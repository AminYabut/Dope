using UnispectEx.Core.Util;

namespace UnispectEx.Core.Pe {
    internal class ImageFileHeader {
        private ImageFileHeader(
            ushort machine,
            ushort sectionCount,
            ushort optionalHeaderSize,
            ushort characteristics) {
            Machine = machine;
            SectionCount = sectionCount;
            OptionalHeaderSize = optionalHeaderSize;
            Characteristics = characteristics;
        }

        internal ushort Machine { get; }
        internal ushort SectionCount { get; }
        internal ushort OptionalHeaderSize { get; }
        internal ushort Characteristics { get; }

        internal static ImageFileHeader Create(MemoryConnector memory, ulong address) {
            var reader = new MemoryReader(memory, address);

            var machine = reader.U16();
            var sectionCount = reader.U16();

            reader.Seek(reader.Tell() + 0xC);

            var optionalHeaderSize = reader.U16();
            var characteristics = reader.U16();

            return new(machine, sectionCount, optionalHeaderSize, characteristics);
        }
    }
}