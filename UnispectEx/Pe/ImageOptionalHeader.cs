using UnispectEx.Util;

namespace UnispectEx.Pe {
    internal class ImageOptionalHeader {
        private ImageOptionalHeader() { }

        internal ushort Magic { get; init; } 
        internal ImageDataDirectory[] DataDirectories { get; init; }
        
        internal static ImageOptionalHeader Create(Memory memory, ulong address, bool is32) {
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

            return new ImageOptionalHeader {
                Magic = magic,
                DataDirectories = dataDirectories
            };
        }
    }
}