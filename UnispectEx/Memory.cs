using System;
using System.Runtime.InteropServices;
using System.Text;

namespace UnispectEx {
    internal abstract class Memory {
        internal abstract bool Attach(string name);
        internal abstract bool Attach(int pid);

        internal abstract ulong GetModule(string name);

        internal abstract bool ReadBytes(ulong address, out byte[] buffer, ulong size);
        internal abstract bool WriteBytes(ulong address, byte[] buffer);

        internal T Read<T>(ulong address) where T : struct {
            if (!ReadBytes(address, out var buffer, (ulong) Marshal.SizeOf<T>())) {
                return new T();
            }

            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

            try {
                return Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            }
            finally {
                handle.Free();
            }
        }

        internal string ReadString(ulong address, ulong maxLength) {
            if (!ReadBytes(address, out var buffer, maxLength))
                return string.Empty;
            
            var nullByteOffset = Array.IndexOf(buffer, (byte) 0);
            if (nullByteOffset == -1)
                return string.Empty;

            return Encoding.UTF8.GetString(new Span<byte>(buffer, 0, nullByteOffset));
        }

        internal unsafe void Write<T>(ulong address, T value) where T : struct {
            var buffer = new byte[Marshal.SizeOf<T>()];

            fixed (byte* pointer = buffer) {
                Marshal.StructureToPtr(value, (IntPtr) pointer, true);
            }

            WriteBytes(address, buffer);
        }
    }
}                                                 