using System;
using System.Collections.Generic;
using System.Linq;

using dnlib.DotNet;

using UnispectEx.Mono;

namespace UnispectEx.Util {
    internal static class Helpers {
        internal static IEnumerable<Tuple<TypeDef, MonoClass>> CorrelateClasses(IEnumerable<TypeDef> typeDefs, IEnumerable<MonoClass> monoClasses) {
            var l = typeDefs.OrderBy(x => x.MDToken.ToInt32());
            var r = monoClasses.OrderBy(x => x.Token);

            using var e1 = l.GetEnumerator();
            using var e2 = r.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext())
                yield return new Tuple<TypeDef, MonoClass>(e1.Current, e2.Current);
        }

        internal static IEnumerable<Tuple<FieldDef, MonoClassField>> CorrelateFields(IEnumerable<FieldDef> fieldDefs, IEnumerable<MonoClassField> classFields) {
            var l = fieldDefs.OrderBy(x => x.MDToken.ToInt32());
            var r = classFields.OrderBy(x => x.Token);

            using var e1 = l.GetEnumerator();
            using var e2 = r.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext())
                yield return new Tuple<FieldDef, MonoClassField>(e1.Current, e2.Current);
        }
    }
}