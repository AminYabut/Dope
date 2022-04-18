using System.Collections.Generic;

namespace UnispectEx.Inspector {
    internal interface IDumpProcessor {
        bool Initialize(IEnumerable<MetadataContainer> metadataContainers);
        bool Mark();
        bool Serialize(IDumpSerializer serializer);
        bool Write(IEnumerable<MetadataContainer> metadataContainers, string outputDirectory);
    }
}