using System.Collections.Immutable;

using dnlib.DotNet;

using UnispectEx.Core.Mono;
using UnispectEx.Core.Util;

namespace UnispectEx.Core.Inspector {
    public class MetadataContainer {
        public MetadataContainer(TypeDef typeDef, MonoClass monoClass) {
            TypeDef = typeDef;
            MonoClass = monoClass;

            var fields = new List<MetadataFieldContainer>();
            foreach (var (fieldDef, monoClassField) in Helpers.CorrelateFields(typeDef.Fields, monoClass.Fields()))
                fields.Add(new MetadataFieldContainer(fieldDef, monoClassField));

            Fields = fields.ToImmutableList();
        }

        public override string ToString() => FullName;

        public string Name {
            get => TypeDef.Name;
            set => TypeDef.Name = value;
        }

        public string Namespace {
            get => TypeDef.Namespace;
            set => TypeDef.Namespace = value;
        }

        public string FullName => TypeDef.FullName;

        public TypeDef TypeDef { get; }
        public MonoClass MonoClass { get; }

        public ImmutableList<MetadataFieldContainer> Fields { get; }

        public bool Export { get; set; }
    }
}