using UnispectEx.Core.Util;

namespace UnispectEx.Core.Pe.DataDirectories; 

internal class ImageExportDirectory : IDataDirectory {
    private ImageExportDirectory(
        uint functionCount,
        uint namesCount,
        uint addressOfFunctions,
        uint addressOfNames,
        uint addressOfNameOrdinals) {
        FunctionCount = functionCount;
        NamesCount = namesCount;

        AddressOfFunctions = addressOfFunctions;
        AddressOfNames = addressOfNames;
        AddressOfNameOrdinals = addressOfNameOrdinals;
    }

    internal uint FunctionCount { get; }
    internal uint NamesCount { get; }

    internal uint AddressOfFunctions { get; }
    internal uint AddressOfNames { get; }
    internal uint AddressOfNameOrdinals { get; }

    internal static ImageExportDirectory Create(MemoryConnector memory, ulong address) {
        var reader = new MemoryReader(memory, address);

        var characteristics = reader.U32();

        reader.Seek(0x14);

        var functionCount = reader.U32();
        var namesCount = reader.U32();

        var addressOfFunctions = reader.U32();
        var addressOfNames = reader.U32();
        var addressOfNameOrdinals = reader.U32();

        return new(functionCount, namesCount, addressOfFunctions, addressOfNames, addressOfNameOrdinals);
    }
}