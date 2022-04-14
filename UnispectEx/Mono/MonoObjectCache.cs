using System.Collections.Generic;

namespace UnispectEx.Mono {
    internal class MonoObjectCache {
        internal MonoClass? LookupClass(ulong address) {
            lock (_classes) {
                if (_classes.TryGetValue(address, out var value))
                    return value;

                return null;
            }
        }
        
        internal MonoClassField? LookupField(ulong address) {
            lock (_fields) {
                if (_fields.TryGetValue(address, out var value))
                    return value;

                return null;
            }
        }
        
        internal MonoType? LookupType(ulong address) {
            lock (_types) {
                if (_types.TryGetValue(address, out var value))
                    return value;

                return null;
            }
        }

        private readonly Dictionary<ulong, MonoClass> _classes = new();
        private readonly Dictionary<ulong, MonoClassField> _fields = new();
        private readonly Dictionary<ulong, MonoType> _types = new();
    }
}