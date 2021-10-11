using System.Collections.Generic;
using CurrencyConverter.Models;

namespace CurrencyConverter.Api.Contracts
{
    public interface ICurrencyService
    {
        Currency GetCurrencyData(string id);
        IEnumerable<CurrencyId> GetCurrencyList();
        decimal ConvertCurrency(Currency current, Currency target, decimal amount);
    }
}