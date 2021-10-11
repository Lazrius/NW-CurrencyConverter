using System.Linq;
using CurrencyConverter.Api.Contracts;
using CurrencyConverter.Api.Services;
using CurrencyConverter.Models;
using NUnit.Framework;

namespace CurrencyConverter.Tests
{
    public sealed class CurrencyServiceTests
    {
        private ICurrencyService _currencyService;
        [SetUp]
        public void Setup()
        {
            this._currencyService = new CurrencyService();
            
            // Normally we would import our mock data here rather than what is hardcoded.
        }

        [Test]
        public void TestValidCurrency()
        {
            var usd = this._currencyService.GetCurrencyData(CurrencyId.USD.ToString());
            Assert.NotNull(usd);
        }

        [Test]
        public void TestCurrencyCaseInsensitivity()
        {
            var usd = this._currencyService.GetCurrencyData("usd");
            Assert.NotNull(usd);
        }
        
        [Test]
        public void TestInvalidCurrency()
        {
            var eee = this._currencyService.GetCurrencyData("EEE");
            Assert.IsNull(eee);
        }
        
        [Test]
        // Test should show that we have at least two currencies to compare
        public void TestValidCurrencies()
        {
            var list = _currencyService.GetCurrencyList();
            Assert.GreaterOrEqual(list.Count(), 2);
        }

        [Test]
        public void TestCurrencyConversion()
        {
            var cur1 = _currencyService.GetCurrencyData(CurrencyId.USD.ToString());
            Assert.IsNotNull(cur1);
            var cur2 = _currencyService.GetCurrencyData(CurrencyId.EUR.ToString());
            Assert.IsNotNull(cur2);
            
            Assert.NotZero(this._currencyService.ConvertCurrency(cur1, cur2, 100));
        }
        
        [Test]
        public void TestCurrencyConversionWithNegativeAmount()
        {
            var cur1 = _currencyService.GetCurrencyData(CurrencyId.USD.ToString());
            Assert.IsNotNull(cur1);
            var cur2 = _currencyService.GetCurrencyData(CurrencyId.EUR.ToString());
            Assert.IsNotNull(cur2);
            
            Assert.Zero(this._currencyService.ConvertCurrency(cur1, cur2, -100));
        }
    }
}