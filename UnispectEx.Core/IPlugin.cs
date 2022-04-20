using UnispectEx.Core.Inspector;

namespace UnispectEx.Core {
    public interface IPlugin {
        public string Name { get; }
        public IEnumerable<IDumpAnalyzer> DumpAnalyzers { get; }
        public IEnumerable<IDumpProcessor> DumpProcessors { get; }
        public IEnumerable<IDumpSerializer> DumpSerializers { get; }
    }
}