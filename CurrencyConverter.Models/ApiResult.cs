using System;
using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class ApiResult
    {
        // Error string, null if successful
        public string Error { get; set; }
        
        // Time of last currency update - Could potentially integrate with currency class
        public DateTime LastUpdate { get; }
        
        // Time of request
        public DateTime Timestamp { get; }
        
        // The actual currency object that contains the rates, id, etc
        public Currency Currency { get; set; }

        /// <summary>
        /// Override ToString method to return a json representation of the api result.
        /// </summary>
        /// <returns>A json representation of the api response</returns>
        public override string ToString() => JsonConvert.SerializeObject(this);
        
        public ApiResult(string err)
        {
            // Current timestamp that cannot be edited.
            this.Timestamp = DateTime.Now;
            
            // Hardcoded update time as this is a POC API. Normally you'd pull this data from the currency data.
            // YYYY-MM-DD format
            this.LastUpdate = DateTime.Parse("2021-10-09");

            this.Error = err;
        }
    }
}