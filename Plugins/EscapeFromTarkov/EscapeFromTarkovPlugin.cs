using System.Collections.Generic;

using UnispectEx.Core;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov;

public class EscapeFromTarkovPlugin : IPlugin {
    public string Name => "EscapeFromTarkov";

    public IEnumerable<IDumpAnalyzer>? DumpAnalyzers { get; } =
        new List<IDumpAnalyzer> { new EscapeFromTarkovAnalyzer() };

    public IEnumerable<IDumpProcessor>? DumpProcessors => null;
    public IEnumerable<IDumpSerializer>? DumpSerializers => null;
}