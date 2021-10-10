using System.Collections.Generic;

namespace CurrencyConverter.Models
{
    public class Currency
    {
        // Full official name of the currency
        public string CurrencyName { get; set; }
        
        // The ID of the currency as defined by ISO-4217
        public CurrencyId Id { get; set; }

        // Map of currencies to their relative rates
        public Dictionary<Currency, decimal> Rates { get; } = new Dictionary<Currency, decimal>();

        public char CurrencyCharacter { get; }

        public Currency(string currencyName, CurrencyId id, char currencyCharacter)
        {
            this.CurrencyName = currencyName;
            this.Id = id;
            this.CurrencyCharacter = currencyCharacter;
        }
    }
}