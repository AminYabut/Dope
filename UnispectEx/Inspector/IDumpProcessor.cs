using System.Collections.Generic;
using System.IO;
using dnlib.DotNet;
using UnispectEx.Inspector;
using UnispectEx.Mono;

namespace UnispectEx.Inspector {
    internal interface IDumpProcessor {
        bool Initialize(IEnumerable<MetadataContainer> metadataContainers);
        bool Mark();
        bool Serialize(IDumpSerializer serializer);
        bool Write(IEnumerable<MetadataContainer> metadataContainers, string outputDirectory);
    }
}