using UnispectEx.Util;

namespace UnispectEx.Pe.DataDirectories {
    internal class ImageExportDirectory : IDataDirectory {
        private ImageExportDirectory() { }
        
        internal uint FunctionCount { get; init; }
        internal uint NamesCount { get; init; }
        
        internal uint AddressOfFunctions { get; init; }
        internal uint AddressOfNames { get; init; }
        internal uint AddressOfNameOrdinals { get; init; }

        internal static ImageExportDirectory Create(Memory memory, ulong address) {
            var reader = new MemoryReader(memory, address);
            
            var characteristics = reader.U32();

            reader.Seek(20);

            var functionCount = reader.U32();
            var namesCount = reader.U32();
            
            var addressOfFunctions = reader.U32();
            var addressOfNames = reader.U32();
            var addressOfNameOrdinals = reader.U32();

            return new ImageExportDirectory {
                FunctionCount = functionCount,
                NamesCount = namesCount,
                AddressOfFunctions = addressOfFunctions,
                AddressOfNames = addressOfNames,
                AddressOfNameOrdinals = addressOfNameOrdinals
            };
        }
    }
}