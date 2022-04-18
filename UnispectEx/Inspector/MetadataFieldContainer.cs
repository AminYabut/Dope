using dnlib.DotNet;

using UnispectEx.Mono;

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
}