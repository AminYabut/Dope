using dnlib.DotNet;

namespace EscapeFromTarkov.Extensions; 

internal static class PropertyDefExtensions {
    internal static FieldDef? GetFieldDef(this PropertyDef propertyDef) {
        return Helpers.GetFieldFromProperty(propertyDef);
    }
}