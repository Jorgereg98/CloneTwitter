using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Twitter.Models;

namespace Twitter.Design_patterns.Proxy
{
    public class ProxyWeather : IProxyWeather
    {
        private RestClient _client;
        private string appid = "b1e34d4d55487b41db609a28e5854900";
        private string metrics = "metric";

        public ProxyWeather()
        {
            _client = new RestClient("http://api.openweathermap.org/data/2.5/");
        }


        public WeatherObject weather(string city)
        {
            var request = new RestRequest($"weather?q={city}&APPID={appid}&units={metrics}");
            var response = _client.Get<WeatherObject>(request);
            return response.Data;
        }

        public ForecastObject forecast(string city)
        {
            var request = new RestRequest($"forecast?q={city}&APPID={appid}&units={metrics}");
            var response = _client.Get<ForecastObject>(request);
            return response.Data;
        }
    }
}
