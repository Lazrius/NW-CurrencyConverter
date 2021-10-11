using System;
using Newtonsoft.Json;

namespace CurrencyConverter.Models
{
    public class ApiResult<T>
    {
        // Error string, null if successful
        public string Error { get; set; }

        // The object our API is returning
        public T Result { get; set; }

        /// <summary>
        /// Override ToString method to return a json representation of the api result.
        /// </summary>
        /// <returns>A json representation of the api response</returns>
        public override string ToString() => JsonConvert.SerializeObject(this);

        public ApiResult() => this.Error = null;
        public ApiResult(string err)
        {
            this.Error = err;
        }
    }
}