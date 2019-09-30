using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPp
{
    class Program
    {
        static void Main(string[] args)
        {

            string stillSearching = "y";

            while (stillSearching.ToLower() == "y")
            {
                Console.WriteLine("Where would you like the weather for?");
                string whereFor = Console.ReadLine();

                RestConsumer getLocation = new RestConsumer();
                getLocation.endPoint = $"https://api.mapbox.com/geocoding/v5/mapbox.places/{whereFor}.json?access_token=pk.eyJ1IjoiZ2NhbXVrIiwiYSI6ImNqeG9wbHlyODA4Zmozbm52NDhnZ2s2Ym8ifQ.xuchUhGWJNRW4XEiuIiSvw";

                string strResponse = string.Empty;

                strResponse = getLocation.makeRequest();

                dynamic location = JsonConvert.DeserializeObject(strResponse);

                //Console.WriteLine(location.features[0].center);

                RestConsumer getWeather = new RestConsumer();
                getWeather.endPoint = $"https://api.darksky.net/forecast/e488b23ccfddebc1e29d24d4e0b0721f/{location.features[0].center[1]},{location.features[0].center[0]}";

                string weatherdata = getWeather.makeRequest();
                dynamic weather = JsonConvert.DeserializeObject(weatherdata);

                //Console.WriteLine(weather.currently);

                Console.WriteLine($"The current temperature in {whereFor} is {weather.currently.temperature}°F and the outlook is {weather.currently.summary}th a {weather.currently.precipProbability * 100}% chance if rain!");

                Console.WriteLine($"Are you still searching for the weather? y/n");
                stillSearching = Console.ReadLine();
            }
        
        }
    }
}
