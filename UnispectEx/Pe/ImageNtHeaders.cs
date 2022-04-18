using System;

using UnispectEx.Pe.Constants;
using UnispectEx.Util;

namespace UnispectEx.Pe {
    internal class ImageNtHeaders {
        private ImageNtHeaders(uint signature, ImageFileHeader fileHeader, ImageOptionalHeader optionalHeader) {
            Signature = signature;
            FileHeader = fileHeader;
            OptionalHeader = optionalHeader;
        }
        
        internal uint Signature { get; }
        internal ImageFileHeader FileHeader { get; }
        internal ImageOptionalHeader OptionalHeader { get; }

        internal static ImageNtHeaders Create(MemoryConnector memory, ulong address) {
            var reader = new MemoryReader(memory, address);

            var signature = reader.U32();

            if (signature != ImageConstants.NtSignature)
                throw new InvalidOperationException("dos header invalid!");

            var fileHeader = ImageFileHeader.Create(memory, address + 4);
            var optionalHeader = ImageOptionalHeader.Create(
                memory,
                address + sizeof(uint) + ImageConstants.FileHeaderSize,
                fileHeader.Machine == ImageConstants.MachineI386);

            return new(signature, fileHeader, optionalHeader);
        }
    }
}