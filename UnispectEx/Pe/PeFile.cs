using System;
using System.Text;
using UnispectEx.Pe.Constants;
using UnispectEx.Pe.DataDirectories;
using UnispectEx.Pe.Models;

namespace UnispectEx.Pe {
    internal class PeFile {
        private PeFile(Memory memory) {
            _memory = memory;
        }
        
        internal ulong BaseAddress { get; init; }
        internal ImageDosHeader DosHeader { get; init; }
        internal ImageNtHeaders NtHeaders { get; init; }

        internal ulong GetExport(string name) {
            var directoryEntry = GetDataDirectory(DataDirectory.Export);

            if (directoryEntry.VirtualAddress == 0 || directoryEntry.Size == 0)
                return 0;

            var directory = ImageExportDirectory.Create(_memory, BaseAddress + directoryEntry.VirtualAddress);

            for (uint i = 0; i < directory.NamesCount; ++i) {
                var nameAddress = _memory.Read<uint>(BaseAddress + directory.AddressOfNames + i * 0x4);
                
                if (!_memory.ReadBytes(BaseAddress + nameAddress, out var buffer, 0xFF)) {
                    return 0;
                }
                
                // TODO: calculate size dynamically
                var functionName = Encoding.ASCII.GetString(new Span<byte>(buffer,0, Array.IndexOf(buffer, (byte) 0)));

                if (functionName == name) {
                    var offset = _memory.Read<ushort>(BaseAddress + directory.AddressOfNameOrdinals + i * 2);

                    return BaseAddress + _memory.Read<uint>(BaseAddress + directory.AddressOfFunctions + (uint) offset * 4);
                }
            }

            return 0;
        }
        
        internal ImageDataDirectory GetDataDirectory(DataDirectory id) {
            var dataDirectories = NtHeaders.OptionalHeader.DataDirectories;

            if (dataDirectories.Length < (uint) id)
                return null;

            return dataDirectories[(uint) id];
        }

        internal static PeFile Create(Memory memory, ulong address) {
            var dosHeader = ImageDosHeader.Create(memory, address);
            var ntHeaders = ImageNtHeaders.Create(memory, address + dosHeader.NtHeadersOffset);
            
            return new PeFile(memory) {
                BaseAddress = address,
                DosHeader = dosHeader,
                NtHeaders = ntHeaders
            };
        }

        private readonly Memory _memory;
    }
}