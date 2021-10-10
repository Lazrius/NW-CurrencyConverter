using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CurrencyConverter.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CurrencyId
    {
        // ReSharper disable InconsistentNaming
        USD,
        GBP,
        EUR,
        JYP,
        CAD,
        AUD
        // ReSharper restore InconsistentNaming
    }
}