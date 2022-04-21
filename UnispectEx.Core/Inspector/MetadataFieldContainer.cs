using dnlib.DotNet;

using UnispectEx.Core.Mono;

namespace UnispectEx.Core.Inspector; 

public class MetadataFieldContainer {
    internal MetadataFieldContainer(FieldDef fieldDef, MonoClassField monoClassField) {
        FieldDef = fieldDef;
        MonoClassField = monoClassField;

        Export = false;
    }

    public override string ToString() => Name;

    public string Name {
        get => FieldDef.Name;
        set => FieldDef.Name = value;
    }

    public FieldDef FieldDef { get; }
    public MonoClassField MonoClassField { get; }

    public bool Export { get; set; }
}