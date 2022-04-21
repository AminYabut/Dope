using System.IO;
using System.Text;

using dnlib.DotNet;

using UnispectEx.Core.Inspector;

namespace UnispectEx.Serializers {
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

        private void WriteClass(StreamWriter writer, MetadataContainer container) {
            writer.WriteLine($"class: {container.Name}");
            writer.WriteLine($"parents: {BaseTypes(container.TypeDef)}");

            foreach (var metadataFieldContainer in container.Fields) {
                if (!metadataFieldContainer.Export)
                    continue;

                var fieldDef = metadataFieldContainer.FieldDef;
                var offset = metadataFieldContainer.MonoClassField.Offset;
                var token = fieldDef.MDToken.ToInt32();

                string tag;
                if (fieldDef.IsLiteral)
                    tag = "[C]";
                else if (fieldDef.IsStatic)
                    tag = "[S]";
                else
                    tag = "[I]";

                writer.WriteLine($"  - {tag} {fieldDef.Name}:0x{token:X} | 0x{offset:X} | {fieldDef.FieldType.FullName}");
            }
        }

        private void WriteEnum(StreamWriter writer, MetadataContainer container) {
            writer.WriteLine($"enum: {container.Name}");

            foreach (var metadataFieldContainer in container.Fields) {
                if (!metadataFieldContainer.Export)
                    continue;

                var fieldDef = metadataFieldContainer.FieldDef;

                writer.WriteLine($"  - [E] {fieldDef.Name} | = {fieldDef.InitialValue}");
            }
        }

        public bool Serialize(StreamWriter writer, MetadataContainer metadataContainer) {
            writer.WriteLine("---");

            if (metadataContainer.TypeDef.IsEnum)
                WriteEnum(writer, metadataContainer);
            else if (metadataContainer.TypeDef.IsClass)
                WriteClass(writer, metadataContainer);

            writer.Flush();

            return true;
        }
    }
}