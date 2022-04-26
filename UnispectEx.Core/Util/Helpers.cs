using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using dnlib.DotNet;

using UnispectEx.Core.Mono;

namespace UnispectEx.Core.Util;

public static class Helpers {
    public static IEnumerable<Tuple<TypeDef, MonoClass>> CorrelateClasses(
        IEnumerable<TypeDef> typeDefs,
        IEnumerable<MonoClass> monoClasses) {
        var l = typeDefs.OrderBy(x => x.MDToken.ToInt32());
        var r = monoClasses.OrderBy(x => x.Token);

        using var e1 = l.GetEnumerator();
        using var e2 = r.GetEnumerator();

        while (e1.MoveNext() && e2.MoveNext())
            yield return new Tuple<TypeDef, MonoClass>(e1.Current, e2.Current);
    }

    public static IEnumerable<Tuple<FieldDef, MonoClassField>> CorrelateFields(
        IEnumerable<FieldDef> fieldDefs,
        IEnumerable<MonoClassField> classFields) {
        var l = fieldDefs.OrderBy(x => x.MDToken.ToInt32());
        var r = classFields.OrderBy(x => x.Token);

        using var e1 = l.GetEnumerator();
        using var e2 = r.GetEnumerator();

        while (e1.MoveNext() && e2.MoveNext())
            yield return new Tuple<FieldDef, MonoClassField>(e1.Current, e2.Current);
    }

    // https://stackoverflow.com/a/67332992/17264463
    public static string ToSnakeCase(string text) {
        if (string.IsNullOrEmpty(text))
            return text;

        var builder = new StringBuilder(text.Length + Math.Min(2, text.Length / 5));
        var lastCategory = default(UnicodeCategory?);

        for (var i = 0; i < text.Length; ++i) {
            var l = text[i];
            if (l == '_') {
                builder.Append('_');
                lastCategory = null;

                continue;
            }

            var category = char.GetUnicodeCategory(l);
            switch (category) {
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.TitlecaseLetter:
                    if (lastCategory == UnicodeCategory.SpaceSeparator ||
                        lastCategory == UnicodeCategory.LowercaseLetter ||
                        lastCategory != UnicodeCategory.DecimalDigitNumber &&
                        lastCategory != null &&
                        i > 0 &&
                        i + 1 < text.Length &&
                        char.IsLower(text[i + 1])) {
                        builder.Append('_');
                    }

                    l = char.ToLower(l, CultureInfo.InvariantCulture);

                    break;

                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.DecimalDigitNumber:
                    if (lastCategory == UnicodeCategory.SpaceSeparator)
                        builder.Append('_');

                    break;

                default:
                    if (lastCategory is not null)
                        lastCategory = UnicodeCategory.SpaceSeparator;

                    continue;
            }

            builder.Append(l);
            lastCategory = category;
        }

        return builder.ToString();
    }

    public static bool IsObfuscatedSymbolName(string name) {
        return Regex.IsMatch(name, "[^a-zA-Z0-9_`]", RegexOptions.None);
    }
}