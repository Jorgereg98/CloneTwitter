using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Models;

namespace Twitter.Design_patterns.Proxy
{
    interface IProxyWeather
    {
        WeatherObject weather(string city);
        ForecastObject forecast(string city);
    }
}
