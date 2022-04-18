using System.IO;
using System.Text;
using dnlib.DotNet;

namespace UnispectEx.Inspector {
    internal class DefaultSerializer : IDumpSerializer {
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
        
        public bool Serialize(StreamWriter writer, MetadataContainer metadataContainer) {
            writer.WriteLine("---");
            writer.WriteLine($"class: {metadataContainer.MonoClass.FullName}");
            writer.WriteLine($"parents: {BaseTypes(metadataContainer.TypeDef)}");

            foreach (var metadataFieldContainer in metadataContainer.Fields) {
                var fieldDef = metadataFieldContainer.FieldDef;
                var monoClassField = metadataFieldContainer.MonoClassField;

                string tag;
                if (fieldDef.IsLiteral)
                    tag = "[C]";
                else if (fieldDef.IsStatic)
                    tag = "[S]";
                else
                    tag = "[I]";

                writer.WriteLine($"  - {tag} {fieldDef.Name}:0x{fieldDef.MDToken.ToInt32():X} | 0x{monoClassField.Offset:X} | {fieldDef.FieldType.FullName}");
            }

            writer.Flush();

            return true;
        }
    }
}