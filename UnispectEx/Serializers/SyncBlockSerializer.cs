using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using dnlib.DotNet;

using UnispectEx.Core.Inspector;
using UnispectEx.Core.Util;

namespace UnispectEx.Serializers; 

public class SyncBlockSerializer : IDumpSerializer {
    private string BaseTypes(TypeDef typeDef) {
        var builder = new StringBuilder();

        ITypeDefOrRef baseType = typeDef.BaseType;
        while (baseType is not null) {
            builder.Append($"{baseType.Name}");

            baseType = baseType.GetBaseType();

            if (baseType is not null)
                builder.Append(" : ");
        }

        return builder.ToString();
    }

    private TypeDef? TypeDefFromSig(TypeSig sig) {
        return sig.ScopeType switch {
            TypeDef fieldTypeTypeDef => fieldTypeTypeDef,
            TypeRef typeRef => sig.Module.Context.Resolver.Resolve(typeRef),
            _ => null
        };
    }

    private string? ToPrimitiveTypeName(TypeSig fieldSig) {
        if (fieldSig.FullName.EndsWith("[]")) // array
            return "uint64_t";

        switch (fieldSig.FullName) {
            case "System.SByte":
                return "int8_t";

            case "System.Int16":
                return "int16_t";

            case "System.Int32":
                return "int32_t";

            case "System.Int64":
                return "int64_t";

            case "System.Byte":
                return "uint8_t";

            case "System.UInt16":
                return "uint16_t";

            case "System.UInt32":
                return "uint32_t";

            case "System.UInt64":
                return "uint64_t";

            case "System.Char":
                return "char";

            case "System.Boolean":
                return "bool";

            case "System.Single":
                return "float";

            case "System.Double":
                return "double";

            case "UnityEngine.Vector2":
                return "glm::vec2";

            case "UnityEngine.Vector3":
                return "glm::vec3";

            case "UnityEngine.Quaternion":
                return "glm::vec4";

            case "UnityEngine.Color":
                return "glm::vec4";

            case "UnityEngine.LayerMask":
                return "int32_t";
        }

        var typeDef = TypeDefFromSig(fieldSig);

        if (typeDef is null)
            return null;

        if ((typeDef.IsClass || typeDef.IsInterface) && !typeDef.IsValueType)
            return "uint64_t";

        if (typeDef.IsClass && typeDef.IsValueType) {
            if (typeDef.IsEnum)
                return Helpers.ToSnakeCase(typeDef.Name);
            
            // TODO: handle obfuscated symbols lol
            if (Helpers.IsObfuscatedSymbolName(typeDef.Name))
                return "<OBFUSCATED_SYMBOL_NAME>";

            // :skull: we need to reference in-game struct types here
            if (fieldSig.Module == typeDef.Module)
                return Helpers.ToSnakeCase(typeDef.Name);
            
            // we need to resolve third party structs :pensive:
            return "<ILLEGAL_STRUCT>";
        }

        return null;
    }

    private uint? ToPrimitiveTypeSize(TypeSig fieldSig) {
        if (fieldSig.FullName.EndsWith("[]")) // array
            return 8;

        switch (fieldSig.FullName) {
            case "System.SByte":
                return 1;

            case "System.Int16":
                return 2;

            case "System.Int32":
                return 4;

            case "System.Int64":
                return 8;

            case "System.Byte":
                return 1;

            case "System.UInt16":
                return 2;

            case "System.UInt32":
                return 4;

            case "System.UInt64":
                return 8;

            case "System.Char":
                return 1;

            case "System.Boolean":
                return 1;

            case "System.Single":
                return 4;

            case "System.Double":
                return 8;

            case "UnityEngine.Vector2":
                return 8;

            case "UnityEngine.Vector3":
                return 12;

            case "UnityEngine.Quaternion":
                return 16;

            case "UnityEngine.Color":
                return 16;

            case "UnityEngine.LayerMask":
                return 4;
        }

        var typeDef = TypeDefFromSig(fieldSig);

        if (typeDef is null)
            return null;

        if ((typeDef.IsClass || typeDef.IsInterface) && !typeDef.IsValueType)
            return 8;

        if (typeDef.IsClass && typeDef.IsValueType) {
            if (typeDef.IsEnum) {
                switch (typeDef.GetEnumUnderlyingType().ElementType) {
                    case ElementType.Void:
                        return 0;

                    case ElementType.Boolean:
                        return 1;

                    case ElementType.I1:
                    case ElementType.U1:
                        return 1;
                    
                    case ElementType.Char:
                    case ElementType.I2:
                    case ElementType.U2:
                        return 2;

                    case ElementType.I4:
                    case ElementType.U4:
                    case ElementType.R4:
                        return 4;

                    case ElementType.I8:
                    case ElementType.U8:
                    case ElementType.R8:
                        return 8;

                    case ElementType.I:
                    case ElementType.U:
                        return 8; // TODO: detect architecture
                }
            }

            // we need to resolve third party struct sizes :pensive:
            return null;
        }
        
        return null;
    }

    private bool IsIllegalSymbol(string name) {
        switch (name) {
            case "auto":
            case "char":
            case "short":
            case "int":
            case "long":
            case "float":
            case "double":
                return true;

            default:
                return false;
        }
    }

    private void WriteClass(StreamWriter writer, MetadataContainer container) {
        uint classSize = 0;
        
        var lastField = container.Fields.LastOrDefault(x => x.MonoClassField.Offset != -1 && !x.FieldDef.IsStatic && !x.FieldDef.IsLiteral && x.Export);
        if (lastField is not null) {
            uint size = lastField.FieldDef.GetFieldSize();
            if (size != 0)
                classSize = (uint) lastField.MonoClassField.Offset + size;
            else {
                var typeSize = ToPrimitiveTypeSize(lastField.FieldDef.FieldType);

                if (typeSize is null)
                    classSize = (uint) lastField.MonoClassField.Offset + 0x8; // TODO: GUESS SIZE
                else
                    classSize = (uint) lastField.MonoClassField.Offset + typeSize.Value;
            }
        }

        var typeName = Helpers.ToSnakeCase(container.Name);

        var headerName = $"SDK_{typeName.ToUpper()}_HPP";

        writer.WriteLine($"#ifndef {headerName}");
        writer.WriteLine($"#define {headerName}");
        
        writer.WriteLine("namespace sdk {");
        
        writer.WriteLine("// forwarded");

        var forwardedTypes = new List<string>();

        // forward declare dependencies
        foreach (var metadataFieldContainer in container.Fields) {
            if (!metadataFieldContainer.Export)
                continue;

            var fieldDef = metadataFieldContainer.FieldDef;

            var typeDef = TypeDefFromSig(fieldDef.FieldType);

            if (typeDef is null)
                continue;

            if (typeDef.Module != fieldDef.Module)
                continue;

            if (!(typeDef.IsClass && typeDef.IsValueType) && !typeDef.IsEnum)
                continue;

            var name = Helpers.ToSnakeCase(typeDef.Name);

            if (forwardedTypes.Contains(name))
                continue;
            
            writer.WriteLine(typeDef.IsEnum ? $"enum class {name};" : $"struct {name};");

            forwardedTypes.Add(name);
        }

        writer.WriteLine($"// class: {container.Name}");
        writer.WriteLine($"// parents: {BaseTypes(container.TypeDef)}");

        writer.WriteLine($"struct {typeName} : sync_block<0x{classSize:X}> {{");
        writer.WriteLine($"    {typeName}() = default;");
        writer.WriteLine($"    explicit {typeName}(uint64_t address) : sync_block(address) {{ }}");

        bool insideStaticBlock = false;

        // write field
        foreach (var metadataFieldContainer in container.Fields) {
            if (!metadataFieldContainer.Export)
                continue;

            var fieldDef = metadataFieldContainer.FieldDef;
            var offset = metadataFieldContainer.MonoClassField.Offset;

            var type = ToPrimitiveTypeName(fieldDef.FieldType) ?? "<ERROR_READING_PRIMITIVE_NAME>";

            if (fieldDef.IsLiteral)
                continue;

            bool alreadyInsideStaticBlock = insideStaticBlock;
            insideStaticBlock = fieldDef.IsStatic;
            
            if (!alreadyInsideStaticBlock && insideStaticBlock)
                writer.WriteLine("\n// begin static block");
            
            if (alreadyInsideStaticBlock && !insideStaticBlock)
                writer.WriteLine("// end static block\n");

            var name = new StringBuilder(Helpers.ToSnakeCase(fieldDef.Name));
            
            if (fieldDef.IsStatic)
                name.Insert(0, name[0] == '_' ? "get" : "get_");
            else
                name.Insert(0, name[0] == '_' ? "get" : "get_");

            if (IsIllegalSymbol(name.ToString()))
                name.Append('_');

            if (type is "<ERROR_READING_PRIMITIVE_NAME>" or "<ILLEGAL_STRUCT>" or "<OBFUSCATED_SYMBOL_NAME>")
                writer.Write("//");

            writer.WriteLine($"    SYNC_FIELD({type}, {name}, 0x{offset:X}) // {fieldDef.FieldType.FullName}:0x{fieldDef.MDToken}");
        }
        
        if (insideStaticBlock)
            writer.WriteLine("// end static block\n");

        writer.WriteLine($"}};");

        writer.WriteLine("}");

        writer.WriteLine($"#endif // {headerName}");
    }

    private void WriteEnum(StreamWriter writer, MetadataContainer container) {
        var headerName = $"SDK_{Helpers.ToSnakeCase(container.Name).ToUpper()}_HPP";

        writer.WriteLine($"#ifndef {headerName}");
        writer.WriteLine($"#define {headerName}");
        
        writer.WriteLine($"enum class {Helpers.ToSnakeCase(container.Name)} {{");
        
        foreach (var metadataFieldContainer in container.Fields) {
            if (!metadataFieldContainer.Export)
                continue;

            var fieldDef = metadataFieldContainer.FieldDef;

            var name = new StringBuilder(Helpers.ToSnakeCase(fieldDef.Name));
            
            if (IsIllegalSymbol(name.ToString()))
                name.Append('_');

            if (fieldDef.HasConstant) 
                writer.WriteLine($"    {name} = {fieldDef.Constant.Value},");
        }
        
        writer.WriteLine("};");
        
        writer.WriteLine($"#endif // {headerName}");
    }

    public bool Serialize(StreamWriter writer, MetadataContainer metadataContainer) {
        writer.WriteLine("// ---");

        if (metadataContainer.TypeDef.IsEnum)
            WriteEnum(writer, metadataContainer);
        else if (metadataContainer.TypeDef.IsClass)
            WriteClass(writer, metadataContainer);

        writer.Flush();

        return true;
    }
}