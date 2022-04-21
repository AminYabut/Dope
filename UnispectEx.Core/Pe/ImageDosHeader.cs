using System;

using UnispectEx.Core.Pe.Constants;
using UnispectEx.Core.Util;

namespace UnispectEx.Core.Pe; 

internal class ImageDosHeader {
    private ImageDosHeader(ushort magic, uint ntHeadersOffset) {
        Magic = magic;
        NtHeadersOffset = ntHeadersOffset;
    }

    internal ushort Magic { get; }
    internal uint NtHeadersOffset { get; }

    internal static ImageDosHeader Create(MemoryConnector memory, ulong address) {
        var reader = new MemoryReader(memory, address);

        var magic = reader.U16();

        if (magic != ImageConstants.DosMagic)
            throw new InvalidOperationException("dos header invalid!");

        reader.Seek(0x3C);

        return new(magic, reader.U32());
    }
}