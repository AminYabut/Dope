using System.IO;
using System.Text;

using dnlib.DotNet;

using UnispectEx.Core.Inspector;

namespace UnispectEx {
    public class DefaultDumpSerializer : IDumpSerializer {
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
            writer.WriteLine($"class: {metadataContainer.Name}");
            writer.WriteLine($"parents: {BaseTypes(metadataContainer.TypeDef)}");

            foreach (var metadataFieldContainer in metadataContainer.Fields) {
                if (!metadataFieldContainer.Export)
                    continue;

                var fieldDef = metadataFieldContainer.FieldDef;
                var offset = metadataFieldContainer.MonoClassField.Offset;

                string tag;
                if (fieldDef.IsLiteral)
                    tag = "[C]";
                else if (fieldDef.IsStatic)
                    tag = "[S]";
                else
                    tag = "[I]";

                writer.WriteLine($"  - {tag} {fieldDef.Name}:0x{fieldDef.MDToken.ToInt32():X} | 0x{offset:X} | {fieldDef.FieldType.FullName}");
            }

            writer.Flush();

            return true;
        }
    }
}