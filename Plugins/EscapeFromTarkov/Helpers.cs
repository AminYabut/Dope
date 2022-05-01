using System.Text;
using System.Text.RegularExpressions;

using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace EscapeFromTarkov;

internal static class Helpers {
    internal static FieldDef? GetFieldFromProperty(PropertyDef property) {
        var method = property.GetMethod;

        if (method is null || !method.HasBody)
            return null;

        foreach (var instruction in method.Body.Instructions) {
            var field = instruction.Operand as IField;

            // does not immediately return a value, do not bother
            if (instruction.OpCode == OpCodes.Call || instruction.OpCode == OpCodes.Callvirt)
                return null;

            if (instruction.OpCode != OpCodes.Ldfld && instruction.OpCode != OpCodes.Ldsfld)
                continue;

            var fieldDef = field.ResolveFieldDef();

            if (fieldDef is null)
                return null;

            if (fieldDef.FieldType.AssemblyQualifiedName == method.ReturnType.AssemblyQualifiedName)
                return fieldDef;

            var fieldTypeDef = fieldDef.FieldType.ScopeType as TypeDef;
            var returnTypeDef = method.ReturnType.ScopeType as TypeDef;

            if (fieldTypeDef is null || returnTypeDef is null)
                return null;

            if (!TypeImplementsInterface(fieldTypeDef, returnTypeDef))
                continue;

            return fieldDef;
        }

        return null;
    }

    internal static TypeDef? GetFieldDefTypeTypeDef(FieldDef fieldDef) {
        var typeDefOrRef = fieldDef.FieldType.ToTypeDefOrRef();

        return !typeDefOrRef.IsTypeDef ? null : typeDefOrRef.ResolveTypeDef();
    }

    internal static string? GetFieldNameFromProperty(PropertyDef property, FieldDef field) {
        if (!UnispectEx.Core.Util.Helpers.IsObfuscatedSymbolName(field.Name))
            return field.Name;

        if (UnispectEx.Core.Util.Helpers.IsObfuscatedSymbolName(property.Name))
            return null;

        var name = new StringBuilder();

        if (field.IsPrivate) {
            name.Append('_');
            name.Append(property.Name.String!);

            name[1] = char.ToLower(name[1]);
        } else {
            name.Append(property.Name.String!);
        }

        return name.ToString();
    }

    internal static bool TypeImplementsInterface(TypeDef typeDef, TypeDef interfaceTypeDef) {
        foreach (var interfaceImpl in typeDef.Interfaces) {
            var interfaceType = interfaceImpl.Interface as TypeDef;
            if (interfaceType is null)
                continue;

            if (interfaceType == interfaceTypeDef)
                return true;
        }

        return false;
    }
}