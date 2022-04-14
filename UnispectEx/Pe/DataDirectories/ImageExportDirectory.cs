using UnispectEx.Util;

namespace UnispectEx.Pe.DataDirectories {
    internal class ImageExportDirectory : IDataDirectory {
        private ImageExportDirectory() { }

        internal uint FunctionCount { get; private init; }
        internal uint NamesCount { get; private init; }

        internal uint AddressOfFunctions { get; private init; }
        internal uint AddressOfNames { get; private init; }
        internal uint AddressOfNameOrdinals { get; private init; }

        internal static ImageExportDirectory Create(MemoryConnector memory, ulong address) {
            var reader = new MemoryReader(memory, address);

            var characteristics = reader.U32();

            reader.Seek(20);

            var functionCount = reader.U32();
            var namesCount = reader.U32();

            var addressOfFunctions = reader.U32();
            var addressOfNames = reader.U32();
            var addressOfNameOrdinals = reader.U32();

            return new() {
                FunctionCount = functionCount,
                NamesCount = namesCount,
                AddressOfFunctions = addressOfFunctions,
                AddressOfNames = addressOfNames,
                AddressOfNameOrdinals = addressOfNameOrdinals
            };
        }
    }
}