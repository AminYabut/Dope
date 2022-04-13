using UnispectEx.Util;

namespace UnispectEx.Pe {
    internal class ImageFileHeader {
        private ImageFileHeader() { }
        
        internal ushort Machine { get; init; }
        internal ushort SectionCount { get; init; }
        internal ushort OptionalHeaderSize { get; init; }
        internal ushort Characteristics { get; init; }

        internal static ImageFileHeader Create(Memory memory, ulong address) {
            var reader = new MemoryReader(memory, address);

            var machine = reader.U16();
            var sectionCount = reader.U16();

            reader.Seek(reader.Tell() + 12);

            var optionalHeaderSize = reader.U16();
            var characteristics = reader.U16();

            return new ImageFileHeader {
                Machine = machine,
                SectionCount = sectionCount,
                OptionalHeaderSize = optionalHeaderSize,
                Characteristics = characteristics
            };
        }
    }
}