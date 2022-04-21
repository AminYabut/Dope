using System.Collections.Generic;
using System.Linq;

using dnlib.DotNet;

using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Extensions; 

internal static class TypeDefExtensions {
    internal static MetadataContainer? ToMetadataContainer(this TypeDef typeDef, IEnumerable<MetadataContainer> containers) {
        return containers.FirstOrDefault(x => x.TypeDef == typeDef);
    }

    internal static MetadataFieldContainer? ToMetadataContainer(this FieldDef fieldDef, MetadataContainer container) {
        return container.Fields.FirstOrDefault(x => x.FieldDef == fieldDef);
    }
}