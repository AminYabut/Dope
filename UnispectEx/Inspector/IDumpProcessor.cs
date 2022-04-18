using System.Collections.Generic;

namespace UnispectEx.Inspector {
    internal interface IDumpProcessor {
        bool Initialize(IEnumerable<MetadataContainer> metadataContainers);
        bool Serialize(IDumpSerializer serializer);
        bool Write(string outputDirectory);
    }
}