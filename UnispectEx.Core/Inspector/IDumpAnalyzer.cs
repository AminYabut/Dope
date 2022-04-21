using System.Collections.Immutable;

namespace UnispectEx.Core.Inspector; 

public interface IDumpAnalyzer {
    bool Analyze(ImmutableList<MetadataContainer> containers);
}