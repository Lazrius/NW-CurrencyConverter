using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class Currency
    {
        // Full official name of the currency
        public string CurrencyName { get; set; }
        
        // The ID of the currency as defined by ISO-4217
        public CurrencyId Id { get; set; }
        
        // Map of currencies to their relative rates
        public Dictionary<CurrencyId, decimal> Rates { get; } = new Dictionary<CurrencyId, decimal>();

        public char CurrencyCharacter { get; }
        
        public DateTime LastUpdate { get; }

        public Currency(string currencyName, CurrencyId id, char currencyCharacter)
        {
            this.CurrencyName = currencyName;
            this.Id = id;
            this.CurrencyCharacter = currencyCharacter;
            
            // Debug value for prototype, normally you'd pull this from your datasource
            this.LastUpdate = DateTime.Parse("2021-10-10");
        }

        public override string ToString() => this.Id.ToString();
    }
}