using System.IO;
using UnispectEx.Inspector;

namespace UnispectEx.Inspector {
    internal interface IDumpSerializer {
        bool Serialize(StreamWriter writer, MetadataContainer metadataContainer);
    }
}