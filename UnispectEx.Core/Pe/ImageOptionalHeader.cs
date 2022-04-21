using UnispectEx.Core.Util;

namespace UnispectEx.Core.Pe; 

internal class ImageOptionalHeader {
    private ImageOptionalHeader(ushort magic, ImageDataDirectory[] dataDirectories) {
        Magic = magic;
        DataDirectories = dataDirectories;
    }

    internal ushort Magic { get; } 
    internal ImageDataDirectory[] DataDirectories { get; }

    internal static ImageOptionalHeader Create(MemoryConnector memory, ulong address, bool is32) {
        var reader = new MemoryReader(memory, address);

        var magic = reader.U16();

        reader.Seek((ulong) (is32 ? 0x5C : 0x6C));

        var dataDirectories = new ImageDataDirectory[reader.U32()];

        for (var i = 0; i < dataDirectories.Length; ++i) {
            dataDirectories[i] = new ImageDataDirectory {
                VirtualAddress = reader.U32(),
                Size = reader.U32()
            };
        }

        return new(magic, dataDirectories);
    }
}