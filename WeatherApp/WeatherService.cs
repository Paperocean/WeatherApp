using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApi.Database;

namespace WeatherApi
{
    public class WeatherService
    {
        private readonly WeatherRepository _weatherRepository;
        private readonly WeatherApp _weatherApp;

        public WeatherService(WeatherRepository weatherRepository, WeatherApp weatherApp)
        {
            _weatherRepository = weatherRepository;
            _weatherApp = weatherApp;
        }

        public async Task<WeatherApi.Database.WeatherData> GetOrFetchWeatherDataAsync(string cityName)
        {
            var weatherData = await _weatherRepository.GetWeatherDataFromDbAsync(cityName);
            if (weatherData != null)
            {
                Console.WriteLine("Retrieved data from database.");
                return weatherData;
            }

            var fetchedData = await _weatherApp.GetWeatherDataAsync(cityName);
            if (fetchedData != null)
            {
                var dbWeatherData = new WeatherApi.Database.WeatherData
                {
                    Name = fetchedData.Name,
                    Temp = fetchedData.Main.Temp,
                    Feels_like = fetchedData.Main.Feels_like,
                    Temp_min = fetchedData.Main.Temp_min,
                    Temp_max = fetchedData.Main.Temp_max,
                    Pressure = fetchedData.Main.Pressure,
                    Humidity = fetchedData.Main.Humidity
                };

                await _weatherRepository.SaveWeatherDataAsync(dbWeatherData);
                Console.WriteLine("Saved data to database.");

                return dbWeatherData;
            }

            return null;
        }
    }

}
