using System.Collections.Immutable;
using System.IO;
using UnispectEx.Core.Inspector;

namespace UnispectEx {
    public class DefaultDumpAnalyzer : IDumpAnalyzer {
        public bool Analyze(ImmutableList<MetadataContainer> containers) {
            foreach (var container in containers) {
                container.Export = true;

                foreach (var fieldContainer in container.Fields)
                    fieldContainer.Export = true;
            }

            return true;
        }
    }
}