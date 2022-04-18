using System.IO;

namespace UnispectEx.Inspector {
    internal interface IDumpSerializer {
        bool Serialize(StreamWriter writer, MetadataContainer metadataContainer);
    }
}