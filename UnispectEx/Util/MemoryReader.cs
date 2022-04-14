using System.Runtime.InteropServices;

namespace UnispectEx.Util {
    internal class MemoryReader {
        internal MemoryReader(MemoryConnector memory, ulong baseOffset) {
            _memory = memory;
            _baseOffset = baseOffset;
        }

        internal ulong Tell() {
            return _offset;
        }
        
        internal ulong Seek(ulong offset) {
            _offset = offset;
            
            return offset;
        }

        internal sbyte I8() {
            return Read<sbyte>();
        }

        internal short I16() {
            return Read<short>();
        }

        internal int I32() {
            return Read<int>();
        }

        internal long I64() {
            return Read<long>();
        }

        internal byte U8() {
            return Read<byte>();
        }

        internal ushort U16() {
            return Read<ushort>();
        }

        internal uint U32() {
            return Read<uint>();
        }

        internal ulong U64() {
            return Read<ulong>();
        }

        internal float R32() {
            return Read<float>();
        }

        internal double R64() {
            return Read<double>();
        }

        private T Read<T>() where T : struct {
            lock (_lock) {
                var result = _memory.Read<T>(_baseOffset + _offset);
                
                _offset += (ulong) Marshal.SizeOf<T>();
                
                return result;
            }
        }

        private ulong _offset;
        private readonly ulong _baseOffset;

        private readonly MemoryConnector _memory;
        private readonly object _lock = new ();
    }
}