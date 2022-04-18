using System;
using System.Text;

using UnispectEx.Core.Pe.Constants;
using UnispectEx.Core.Pe.DataDirectories;
using UnispectEx.Core.Util;

namespace UnispectEx.Core.Pe {
    internal class PeFile {
        private PeFile(MemoryConnector memory, ulong address, ImageDosHeader dosHeader, ImageNtHeaders ntHeaders) {
            Address = address;
            DosHeader = dosHeader;
            NtHeaders = ntHeaders;
            
            _memory = memory;
        }

        internal ulong Address { get; }
        internal ImageDosHeader DosHeader { get; }
        internal ImageNtHeaders NtHeaders { get; }

        internal ulong GetExport(string name) {
            var directoryEntry = GetDataDirectory(DataDirectory.Export);

            if (directoryEntry is null)
                return 0;

            if (directoryEntry.VirtualAddress == 0 || directoryEntry.Size == 0)
                return 0;

            var directory = ImageExportDirectory.Create(_memory, Address + directoryEntry.VirtualAddress);

            for (uint i = 0; i < directory.NamesCount; ++i) {
                var nameAddress = _memory.Read<uint>(Address + directory.AddressOfNames + i * 0x4);

                if (!_memory.ReadBytes(Address + nameAddress, out var buffer, 255))
                    return 0;

                // TODO: calculate size dynamically
                var functionName = Encoding.ASCII.GetString(new Span<byte>(buffer, 0, Array.IndexOf(buffer, (byte) 0)));

                if (functionName == name) {
                    var offset = _memory.Read<ushort>(Address + directory.AddressOfNameOrdinals + i * 2);

                    return Address + _memory.Read<uint>(Address + directory.AddressOfFunctions + (uint) offset * 4);
                }
            }

            return 0;
        }

        internal ImageDataDirectory? GetDataDirectory(DataDirectory id) {
            var dataDirectories = NtHeaders.OptionalHeader.DataDirectories;

            return dataDirectories.Length < (uint) id ? null : dataDirectories[(uint) id];
        }

        internal static PeFile Create(MemoryConnector memory, ulong address) {
            var dosHeader = ImageDosHeader.Create(memory, address);
            var ntHeaders = ImageNtHeaders.Create(memory, address + dosHeader.NtHeadersOffset);

            return new(memory, address, dosHeader, ntHeaders);
        }

        private readonly MemoryConnector _memory;
    }
}