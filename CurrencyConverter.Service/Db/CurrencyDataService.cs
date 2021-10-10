using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyConverter.Models;
using Microsoft.AspNetCore.Identity;

namespace CurrencyConverter.Service.Db
{
    public class CurrencyDataService
    {
        // Normally you would want to extract data from an online API, local data-store, or database
        // However, as this is a mere prototype, we will just 'simulate' doing that.

        private Dictionary<CurrencyId, Currency> Currencies = new Dictionary<CurrencyId, Currency>();
        
        private bool DataLoaded { get; set; } = false;

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

            usd.Rates[eur] = 0.863905m;
            usd.Rates[gbp] = 0.734564m;
            usd.Rates[jpy] = 112.21308m;
            usd.Rates[aud] = 1.368055m;
            usd.Rates[cad] = 1.247145m;

            gbp.Rates[usd] = 1.36152m;
            gbp.Rates[eur] = 1.17607m;
            gbp.Rates[jpy] = 152.7614m;
            gbp.Rates[aud] = 1.862404m;
            gbp.Rates[cad] = 1.697803m;
            
            eur.Rates[usd] = 1.1569m;
            eur.Rates[gbp] = 0.84890m;
            eur.Rates[aud] = 1.5837m;
            eur.Rates[cad] = 1.4499m;
            eur.Rates[jpy] = 129.32m;
            
            jpy.Rates[usd] = 0.008912m;
            jpy.Rates[eur] = 0.007699m;
            jpy.Rates[gbp] = 0.006546m;
            jpy.Rates[aud] = 0.012192m;
            jpy.Rates[cad] = 0.011114m;
            
            aud.Rates[usd] = 0.730965m;
            aud.Rates[eur] = 0.631484m;
            aud.Rates[gbp] = 0.53694m;
            aud.Rates[cad] = 0.911619m;
            aud.Rates[jpy] = 82.02337m;
            
            cad.Rates[usd] = 0.801831m;
            cad.Rates[eur] = 0.692706m;
            cad.Rates[gbp] = 0.588997m;
            cad.Rates[aud] = 1.098650m;
            cad.Rates[jpy] = 89.97596m;
            
            this.Currencies.Clear();
            this.Currencies.Add(CurrencyId.USD, usd);
            this.Currencies.Add(CurrencyId.EUR, eur);
            this.Currencies.Add(CurrencyId.GBP, gbp);
            this.Currencies.Add(CurrencyId.JPY, jpy);
            this.Currencies.Add(CurrencyId.AUD, aud);
            this.Currencies.Add(CurrencyId.CAD, cad);

            this.DataLoaded = true;
        }

        public ApiResult GetCurrencyData(string id)
        {
            var task = this.GetCurrencyDataAsync(id);
            task.Wait();
            return task.Result;
        }

        public Task<ApiResult> GetCurrencyDataAsync(string id)
        {
            if (!DataLoaded)
                this.LoadData();

            if (!Enum.TryParse(id, out CurrencyId cur))
            {
                return Task.FromResult(new ApiResult("Unable to find valid currency."));
            }

            if (!Currencies.ContainsKey(cur))
            {
                return Task.FromResult(new ApiResult("Unable to find valid currency."));
            }

            return Task.FromResult(new ApiResult(null)
            {
                Currency = Currencies[cur],
            });
        }

        public IEnumerable<CurrencyId> GetCurrencyList()
        {
            if (!DataLoaded)
                this.LoadData();

            foreach (var currency in Currencies)
            {
                yield return currency.Key;
            } 
        }
    }
}
