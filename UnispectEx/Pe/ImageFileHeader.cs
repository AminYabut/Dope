using UnispectEx.Util;

namespace UnispectEx.Pe {
    internal class ImageFileHeader {
        private ImageFileHeader() { }
        
        internal ushort Machine { get; private init; }
        internal ushort SectionCount { get; private init; }
        internal ushort OptionalHeaderSize { get; private init; }
        internal ushort Characteristics { get; private init; }

        internal static ImageFileHeader Create(Memory memory, ulong address) {
            var reader = new MemoryReader(memory, address);

            var machine = reader.U16();
            var sectionCount = reader.U16();

            reader.Seek(reader.Tell() + 12);

            var optionalHeaderSize = reader.U16();
            var characteristics = reader.U16();

            return new() {
                Machine = machine,
                SectionCount = sectionCount,
                OptionalHeaderSize = optionalHeaderSize,
                Characteristics = characteristics
            };
        }
    }
}