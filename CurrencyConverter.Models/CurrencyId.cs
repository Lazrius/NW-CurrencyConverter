using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace CurrencyConverter.Models
{
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CurrencyId
    {
        // ReSharper disable InconsistentNaming
        USD,
        GBP,
        EUR,
        JPY,
        CAD,
        AUD
        // ReSharper restore InconsistentNaming
    }
}