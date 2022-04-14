using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace UnispectEx.Util {
    internal class LocalMemory : MemoryConnector {
        internal override bool Attach(string name) {
            var processes = Process.GetProcessesByName(name);

            if (processes.Length == 0)
                return false;

            return Attach(processes.First().Id);
        }

        internal override bool Attach(int pid) {
            Process process;
            
            try {
                process = Process.GetProcessById(pid);
            }
            catch (ArgumentException) {
                return false;
            }

            var handle = OpenProcess(
                ProcessAccessFlags.PROCESS_VM_READ | ProcessAccessFlags.PROCESS_VM_WRITE,
                0,
                (uint) pid);

            if (handle == UIntPtr.Zero)
                return false;

            _process = process;
            _handle = handle;
            
            return true;
        }

        internal override ulong GetModule(string name) {
            if (_process == null)
                return 0;

            foreach (ProcessModule module in _process.Modules) {
                if (module.ModuleName?.ToLower() == name) {
                    return (ulong) module.BaseAddress;
                }
            }

            return 0;
        }

        internal override unsafe bool ReadBytes(ulong address, out byte[] buffer, ulong size) {
            var result = new byte[size];

            fixed (byte* pointer = result) {
                if (!ReadProcessMemory(_handle, (nuint) address, pointer, (nuint) size, out _)) {
                    buffer = null;

                    return false;
                }
            }

            buffer = result;
            
            return true;
        }

        internal override unsafe bool WriteBytes(ulong address, byte[] buffer) {
            fixed (byte* pointer = buffer) {
                return WriteProcessMemory(_handle, (nuint) address, pointer, (nuint) buffer.Length, out _);
            }
        }

        [Flags]
        private enum ProcessAccessFlags : uint {
            PROCESS_VM_READ = 0x0010,
            PROCESS_VM_WRITE = 0x0020
        }
        
        [DllImport("kernel32.dll")]
        private static extern UIntPtr OpenProcess(ProcessAccessFlags desiredAccess, uint inheritHandle, uint pid);

        [DllImport("kernel32.dll")]
        private static extern unsafe bool ReadProcessMemory(UIntPtr handle, nuint address, byte* buffer, nuint size, out nuint bytesRead);

        [DllImport("kernel32.dll")]
        private static extern unsafe bool WriteProcessMemory(UIntPtr handle, nuint address, byte* buffer, nuint size, out nuint bytesWritten);

        private Process _process;
        private UIntPtr _handle = UIntPtr.Zero;
    }
}