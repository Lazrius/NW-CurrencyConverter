using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CurrencyConverter.Api.Contracts;
using CurrencyConverter.Api.Services;
using CurrencyConverter.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private ICurrencyService CurrencyService { get; }
        
        // Dependency inject our currency service
        public CurrencyController(CurrencyService currencyService)
        {
            this.CurrencyService = currencyService;
        }

        [HttpGet]
        public ActionResult<ApiResult<List<CurrencyId>>> GetCurrencies()
        {
            var items = this.CurrencyService.GetCurrencyList().ToList();
            
            if (items.Count is 0)
            {
                var empty = new ApiResult<List<CurrencyId>>("Collection had no items.");
                this.Ok(empty);
            }

            var result = new ApiResult<List<CurrencyId>>
            {
                Result = items
            };
            return this.Ok(result);
        }

        [HttpGet("id")]
        public ActionResult<ApiResult<Currency>> GetCurrencyById([FromQuery][Required] string id)
        {
            var cur = this.CurrencyService.GetCurrencyData(id);
            if (cur is null)
            {
                var notFound = new ApiResult<Currency>("Currency ID was not found.");
                return this.NotFound(notFound);
            }
            
            var result = new ApiResult<Currency>
            {
                Result = cur
            };
            return this.Ok(result);
        }

        [HttpGet("convert")]
        public ActionResult<ApiResult<decimal>> GetCurrencyConversion([FromQuery] [Required] string selected, 
            [FromQuery] [Required] string target, [FromQuery] [Required] decimal amount)
        {
            var selectedCurrency = this.CurrencyService.GetCurrencyData(selected);
            if (selectedCurrency is null)
            {
                return this.NotFound(new ApiResult<decimal>("Selected currency ID was not valid."));
            }
            
            var targetedCurrency = this.CurrencyService.GetCurrencyData(target);
            if (targetedCurrency is null)
            {
                return this.NotFound(new ApiResult<decimal>("Target currency ID was not valid."));
            }

            // While we handle this within our service, we shouldn't accept what we know is a bad result.
            if (amount < 0)
            {
                return this.BadRequest(new ApiResult<decimal>("Amount was a negative number."));
            }
            
            return this.Ok(new ApiResult<decimal>() 
            { 
                Result = this.CurrencyService.ConvertCurrency(selectedCurrency, 
                targetedCurrency, amount) });
        }
    }
}