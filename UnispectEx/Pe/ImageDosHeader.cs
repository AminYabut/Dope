using System;
using UnispectEx.Pe.Constants;
using UnispectEx.Util;

namespace UnispectEx.Pe {
    internal class ImageDosHeader {
        private ImageDosHeader() { }
        
        internal ushort Magic { private get; init; }
        internal uint NtHeadersOffset { get; private init; }

        internal static ImageDosHeader Create(Memory memory, ulong address) {
            var reader = new MemoryReader(memory, address);

            var magic = reader.U16();

            if (magic != ImageConstants.DosMagic)
                throw new InvalidOperationException("dos header invalid!");

            reader.Seek(0x3C);
            
            return new() {
                Magic = magic,
                NtHeadersOffset = reader.U32()
            };
        }
    }
}