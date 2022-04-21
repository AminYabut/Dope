using System.Collections.Immutable;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers; 

internal interface IMarker {
    bool Mark(ImmutableList<MetadataContainer> containers);
}