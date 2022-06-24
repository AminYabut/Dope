﻿using dnlib.DotNet;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Extensions; 

internal static class MetadataContainerExtensions {
    internal static bool IsObfuscated(this MetadataContainer container) {
        return UnispectEx.Core.Util.Helpers.IsObfuscatedSymbolName(container.FullName);
    }
    
    internal static bool IsObfuscated(this MetadataFieldContainer container) {
        return UnispectEx.Core.Util.Helpers.IsObfuscatedSymbolName(container.Name);
    }
    
    internal static MetadataContainer? FindContainerByFullName(this IEnumerable<MetadataContainer> containers, string fullName) {
        return containers.FirstOrDefault(x => x.FullName == fullName);
    }
    
    internal static MetadataContainer? FindContainerContainingFullName(this IEnumerable<MetadataContainer> containers, string fullName) {
        return containers.FirstOrDefault(x => x.FullName.Contains(fullName));
    }
    
    internal static MetadataContainer? FindContainerContainingFieldName(this IEnumerable<MetadataContainer> containers, string name) {
        return containers.FirstOrDefault(container => container.TypeDef.Fields.Any(field => field.Name == name));
    }
    
    internal static MetadataContainer? FindContainerContainingMethodName(this IEnumerable<MetadataContainer> containers, string name) {
        return containers.FirstOrDefault(container => container.TypeDef.Methods.Any(method => method.Name == name));
    }
    
    internal static MetadataFieldContainer? FindFieldContainerByName(this MetadataContainer container, string name) {
        return container.Fields.FirstOrDefault(x => x.FieldDef.Name == name);
    }
    
    internal static MethodDef? FindMethodDefByName(this MetadataContainer container, string name) {
        return container.TypeDef.Methods.FirstOrDefault(x => x.Name == name);
    }
    
    internal static FieldDef? FindFieldDefByName(this IEnumerable<MetadataContainer> containers, string name) {
        return containers.SelectMany(container => container.TypeDef.Fields).FirstOrDefault(field => field.Name == name);
    }
    
    internal static MethodDef? FindMethodDefByName(this IEnumerable<MetadataContainer> containers, string name) {
        return containers.SelectMany(container => container.TypeDef.Methods).FirstOrDefault(method => method.Name == name);
    }

    internal static void ExportNonObfuscatedSymbols(this MetadataContainer container) {
        container.Export = true;

        foreach (var fieldContainer in container.Fields.Where(x => !UnispectEx.Core.Util.Helpers.IsObfuscatedSymbolName(x.Name)))
            fieldContainer.Export = true;
    }
    
    internal static void CleanPropertyFieldNames(this MetadataContainer container) {
        var type = container.TypeDef;

        foreach (var property in type.Properties) {
            var field = Helpers.GetFieldFromProperty(property);

            if (field is null)
                continue;

            var fieldContainer = container.Fields.FirstOrDefault(x => x.FieldDef == field);

            if (fieldContainer is null)
                continue;

            var name = Helpers.GetFieldNameFromProperty(property, field);

            if (name is null)
                continue;

            fieldContainer.Name = name;
        }
    }
}