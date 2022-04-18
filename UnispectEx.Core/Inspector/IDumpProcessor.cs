using System.Collections.Generic;

namespace UnispectEx.Core.Inspector {
    public interface IDumpProcessor {
        bool Initialize(IEnumerable<MetadataContainer> metadataContainers);
        bool Serialize(IDumpSerializer serializer);
        bool Write(string outputDirectory);
    }
}