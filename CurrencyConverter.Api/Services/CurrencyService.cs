using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyConverter.Api.Contracts;
using CurrencyConverter.Models;

namespace CurrencyConverter.Api.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly Dictionary<CurrencyId, Currency> _currencies = new Dictionary<CurrencyId, Currency>();
        
        // Normally you would want to extract data from an online API, local data-store, or database
        // However, as this is a mere prototype, we will just 'simulate' doing that.

        private void LoadData()
        {
            // In hindsight, I could have done one currency then iterated over their values and divided to get the value
            
            // Create our currencies
            var usd = new Currency("United States Dollar", CurrencyId.USD, '$');
            var gbp = new Currency("Great British Pound", CurrencyId.GBP, '£');
            var jpy = new Currency("Japanese Yen", CurrencyId.JPY, '¥');
            var cad = new Currency("Canadian Dollar", CurrencyId.CAD, '$');
            var eur = new Currency("Euro", CurrencyId.EUR, '€');
            var aud = new Currency("Australian Dollar", CurrencyId.AUD, '$');

            usd.Rates[eur.Id] = 0.863905m;
            usd.Rates[gbp.Id] = 0.734564m;
            usd.Rates[jpy.Id] = 112.21308m;
            usd.Rates[aud.Id] = 1.368055m;
            usd.Rates[cad.Id] = 1.247145m;

            gbp.Rates[usd.Id] = 1.36152m;
            gbp.Rates[eur.Id] = 1.17607m;
            gbp.Rates[jpy.Id] = 152.7614m;
            gbp.Rates[aud.Id] = 1.862404m;
            gbp.Rates[cad.Id] = 1.697803m;
            
            eur.Rates[usd.Id] = 1.1569m;
            eur.Rates[gbp.Id] = 0.84890m;
            eur.Rates[aud.Id] = 1.5837m;
            eur.Rates[cad.Id] = 1.4499m;
            eur.Rates[jpy.Id] = 129.32m;
            
            jpy.Rates[usd.Id] = 0.008912m;
            jpy.Rates[eur.Id] = 0.007699m;
            jpy.Rates[gbp.Id] = 0.006546m;
            jpy.Rates[aud.Id] = 0.012192m;
            jpy.Rates[cad.Id] = 0.011114m;
            
            aud.Rates[usd.Id] = 0.730965m;
            aud.Rates[eur.Id] = 0.631484m;
            aud.Rates[gbp.Id] = 0.53694m;
            aud.Rates[cad.Id] = 0.911619m;
            aud.Rates[jpy.Id] = 82.02337m;
            
            cad.Rates[usd.Id] = 0.801831m;
            cad.Rates[eur.Id] = 0.692706m;
            cad.Rates[gbp.Id] = 0.588997m;
            cad.Rates[aud.Id] = 1.098650m;
            cad.Rates[jpy.Id] = 89.97596m;
            
            this._currencies.Clear();
            this._currencies.Add(CurrencyId.USD, usd);
            this._currencies.Add(CurrencyId.EUR, eur);
            this._currencies.Add(CurrencyId.GBP, gbp);
            this._currencies.Add(CurrencyId.JPY, jpy);
            this._currencies.Add(CurrencyId.AUD, aud);
            this._currencies.Add(CurrencyId.CAD, cad);
        }
        
        public Currency GetCurrencyData(string id)
        {
            if (_currencies.Count is 0)
                this.LoadData();
            
            // If we are passed in a invalid id or our dataset doesn't contain said ID, we cannot continue
            if (!Enum.TryParse(id, true, out CurrencyId cur) || !_currencies.ContainsKey(cur))
            {
                return null;
            }

            return _currencies[cur];
        }

        public IEnumerable<CurrencyId> GetCurrencyList()
        {
            if (_currencies.Count is 0)
                this.LoadData();
            
            foreach (var currency in _currencies)
            {
                yield return currency.Key;
            } 
        }

        public decimal ConvertCurrency(Currency current, Currency target, decimal amount)
        {
            if (!current.Rates.ContainsKey(target.Id))
                return 0;

            if (amount < 0)
                amount = 0;
            
            decimal rate = current.Rates[target.Id];
            return amount * rate;
        }
    }
}