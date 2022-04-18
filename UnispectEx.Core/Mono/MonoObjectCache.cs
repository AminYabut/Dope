using System.Collections.Generic;

namespace UnispectEx.Core.Mono {
    internal class MonoObjectCache {
        internal object MonoAssemblyLockObject { get; } = new();
        internal object MonoAssemblyNameLockObject { get; } = new();
        internal object MonoImageLockObject { get; } = new();
        internal object MonoClassLockObject { get; } = new();
        internal object MonoClassFieldLockObject { get; } = new();
        internal object MonoTypeLockObject { get; } = new();

        internal void Cache(ulong address, MonoAssembly obj) {
            lock (_classes)
                _assemblies[address] = obj;
        }

        internal void Cache(ulong address, MonoAssemblyName obj) {
            lock (_classes)
                _assemblyNames[address] = obj;
        }

        internal void Cache(ulong address, MonoImage obj) {
            lock (_images)
                _images[address] = obj;
        }

        internal void Cache(ulong address, MonoClass obj) {
            lock (_classes)
                _classes[address] = obj;
        }

        internal void Cache(ulong address, MonoClassField obj) {
            lock (_fields)
                _fields[address] = obj;
        }

        internal void Cache(ulong address, MonoType obj) {
            lock (_types)
                _types[address] = obj;
        }

        internal bool TryGetAssembly(ulong address, out MonoAssembly? monoClass) {
            lock (_assemblies)
                return _assemblies.TryGetValue(address, out monoClass);
        }

        internal bool TryGetAssemblyName(ulong address, out MonoAssemblyName? monoClass) {
            lock (_assemblyNames)
                return _assemblyNames.TryGetValue(address, out monoClass);
        }

        internal bool TryGetImage(ulong address, out MonoImage? image) {
            lock (_images)
                return _images.TryGetValue(address, out image);
        }

        internal bool TryGetClass(ulong address, out MonoClass? monoClass) {
            lock (_classes)
                return _classes.TryGetValue(address, out monoClass);
        }

        internal bool TryGetField(ulong address, out MonoClassField? field) {
            lock (_fields)
                return _fields.TryGetValue(address, out field);
        }

        internal bool TryGetType(ulong address, out MonoType? type) {
            lock (_types)
                return _types.TryGetValue(address, out type);
        }

        private readonly Dictionary<ulong, MonoAssembly> _assemblies = new();
        private readonly Dictionary<ulong, MonoAssemblyName> _assemblyNames = new();
        private readonly Dictionary<ulong, MonoImage> _images = new();
        private readonly Dictionary<ulong, MonoClass> _classes = new();
        private readonly Dictionary<ulong, MonoClassField> _fields = new();
        private readonly Dictionary<ulong, MonoType> _types = new();
    }
}