using System.IO;

namespace UnispectEx.Core.Inspector {
    public interface IDumpSerializer {
        bool Serialize(StreamWriter writer, MetadataContainer metadataContainer);
    }
}