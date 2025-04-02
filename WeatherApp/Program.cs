using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApi.Database;

namespace WeatherApi
{
    internal partial class Program
    {
        private static WeatherRepository _weatherRepository;
        private static WeatherService _weatherService;

        static async Task Main(string[] args)
        {
            var apiKey = "477b9cacb970e77d9272509c8fbe64c4";
            var httpClient = new HttpClient();
            var weatherApp = new WeatherApp(httpClient, apiKey);

            _weatherRepository = new WeatherRepository();
            _weatherService = new WeatherService(_weatherRepository, weatherApp);

            while (true)
            {
                Console.WriteLine("\n===== Weather App Menu =====");
                Console.WriteLine("1. Get weather by city name");
                Console.WriteLine("2. List stored weather data");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await FetchWeatherDataAsync(weatherApp);
                        break;
                    case "2":
                        await WeatherRepository.ListWeatherDataAsync();
                        break;
                    case "3":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }

            //while (true)
            //{
            //    Console.WriteLine("Enter latitude and longitude (separated by a comma) or a city name:");
            //    string input = Console.ReadLine();

            //    string[] inputParts = input.Split(',');
            //    WeatherData weatherData;

            //    if (inputParts.Length == 1)
            //    {
            //        string cityName = inputParts[0].Trim();
            //        weatherData = await weatherApp.GetWeatherDataAsync(cityName);
            //    }
            //    else
            //    {
            //        string lat = inputParts[0].Trim();
            //        string lon = inputParts.Length > 1 ? inputParts[1].Trim() : null;
            //        weatherData = await weatherApp.GetWeatherDataAsync(lat, lon);
            //    }

            //    if (weatherData == null)
            //    {
            //        Console.WriteLine("Failed to get weather data.");
            //        continue;
            //    }

            //    string weatherSummary = weatherApp.BuildWeatherSummary();
            //    Console.WriteLine(weatherSummary);

            //    Console.WriteLine("Do you want to check another location? (y/n)");
            //    string answer = Console.ReadLine();
            //    if (answer.ToLower() != "y")
            //    {
            //        break;
            //    }
            //}
        }
        private static async Task FetchWeatherDataAsync(WeatherApp weatherApp)
        {
            Console.Write("Enter city name: ");
            string cityName = Console.ReadLine();

            var weatherData = await _weatherService.GetOrFetchWeatherDataAsync(cityName);
            if (weatherData != null)
            {
                Console.WriteLine(weatherData);
            }
            else
            {
                Console.WriteLine("Failed to retrieve weather data.");
            }
        }

    }
}
