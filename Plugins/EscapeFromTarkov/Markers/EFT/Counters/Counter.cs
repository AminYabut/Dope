using System.Collections.Immutable;
using dnlib.DotNet;
using EscapeFromTarkov.Extensions;
using UnispectEx.Core.Inspector;

namespace EscapeFromTarkov.Markers.EFT.Counters;

internal class Counter : IMarker {
    public bool Mark(ImmutableList<MetadataContainer> containers) {
        var counterContainer = containers.FindContainerContainingMethodName("get_ValueType");
        if (counterContainer is null)
            return false;

        
        var baseCounterTypeDef = UnispectEx.Core.Util.Helpers.TypeDefFromSig(counterContainer.TypeDef.BaseType.ToTypeSig());
        if (baseCounterTypeDef is null)
            return false;

        var baseCounterContainer = baseCounterTypeDef.ToMetadataContainer(containers);
        if (baseCounterContainer is null)
            return false;

        baseCounterContainer.Namespace = "EFT.Counters";
        baseCounterContainer.Name = "BaseCounter";
        
        var hashDef = baseCounterContainer.Fields.FirstOrDefault(field => field.FieldDef.FieldType.FullName == "System.Int32");
        if (hashDef is null)
            return false;

        hashDef.Name = "_counterHash";
        
        counterContainer.Namespace = "EFT.Counters";
        counterContainer.Name = "Counter";

        counterContainer.Fields[0].Name = "_CounterValueType";
        
        counterContainer.CleanPropertyFieldNames();
        counterContainer.ExportNonObfuscatedSymbols();
        
        baseCounterContainer.CleanPropertyFieldNames();
        baseCounterContainer.ExportNonObfuscatedSymbols();

        return true;
    }
}