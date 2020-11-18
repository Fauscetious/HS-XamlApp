using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Lab6.Models;
using Newtonsoft.Json;
using static Lab6.Models.Observations;
using Lab6.Models.AutoComplete;
using Lab6.Models.Forecast;

namespace Lab6
{
    class WeatherRetriever
    {
        private string apiKey = "ZNEzufZlMd4HGPZQjEwi3";
        private string secret = "ZASHB46coCkJ4WFp9mFwKmlBzmSuJTbh0w8eCPfD";

        public async Task<ObservationsRootObject> GetObservations(string cityLink)
        {
            HttpClient httpClient = new HttpClient();
            string apiUrl = $"https://api.aerisapi.com/observations/{cityLink}?client_id={apiKey}&client_secret={secret}";

            string responseString = await httpClient.GetStringAsync(apiUrl);

            ObservationsRootObject observations = JsonConvert.DeserializeObject<ObservationsRootObject>(responseString);

            return observations;
        }

        public async Task<AutoCompleteRootObject> GetSuggestions(string enteredStr)
        {
            HttpClient httpClient = new HttpClient();
            string apiUrl = $"https://api.aerisapi.com/places/search?query=name:^{enteredStr}&limit=10&client_id={apiKey}&client_secret={secret}";

            string responseString = await httpClient.GetStringAsync(apiUrl);

            AutoCompleteRootObject suggestions = JsonConvert.DeserializeObject<AutoCompleteRootObject>(responseString);

            return suggestions;
        }

        public async Task<ForecastRootObject> GetForecast(string cityLink)
        {
            HttpClient httpClient = new HttpClient();
            string apiUrl = $"https://api.aerisapi.com/forecasts/{cityLink}?limit=4&filter=day&client_id={apiKey}&client_secret={secret}";

            string responseString = await httpClient.GetStringAsync(apiUrl);

            ForecastRootObject forecast = JsonConvert.DeserializeObject<ForecastRootObject>(responseString);

            return forecast;
        }
    }
}
