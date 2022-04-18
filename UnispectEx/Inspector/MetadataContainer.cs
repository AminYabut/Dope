using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using dnlib.DotNet;
using UnispectEx.Mono;
using UnispectEx.Util;

namespace UnispectEx.Inspector {
    internal class MetadataFieldContainer {
        internal MetadataFieldContainer(FieldDef fieldDef, MonoClassField monoClassField) {
            FieldDef = fieldDef;
            MonoClassField = monoClassField;

            Export = false;
        }

        internal FieldDef FieldDef { get; }
        internal MonoClassField MonoClassField { get; }

        internal bool Export { get; set; }
    }

    internal class MetadataContainer {
        internal MetadataContainer(TypeDef typeDef, MonoClass monoClass) {
            TypeDef = typeDef;
            MonoClass = monoClass;

            var fields = new List<MetadataFieldContainer>();
            foreach (var (fieldDef, monoClassField) in Helpers.CorrelateFields(typeDef.Fields, monoClass.Fields()))
                fields.Add(new MetadataFieldContainer(fieldDef, monoClassField));

            Fields = fields.ToImmutableList();
        }

        internal bool Export { get; set; }

        internal TypeDef TypeDef { get; }
        internal MonoClass MonoClass { get; }

        internal ImmutableList<MetadataFieldContainer> Fields { get; }
    }
}