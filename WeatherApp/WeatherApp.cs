using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WeatherApi
{
    public class WeatherApp
    {
        private readonly HttpClient _httpClient;
        private const string WeatherApiBaseUrl = "https://api.openweathermap.org/data/2.5/weather";
        private readonly string _apiKey;
        private List<WeatherData> _weatherDataList = new List<WeatherData>();

        public WeatherApp(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public async Task<WeatherData> GetWeatherDataAsync(string lat, string lon)
        {
            string url = $"{WeatherApiBaseUrl}?lat={lat}&lon={lon}&appid={_apiKey}&units=metric";
            Console.WriteLine($"Requesting Weather URL: {url}\n");

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: Received status code {response.StatusCode} when calling weather API.");
                return null;
            }

            string content = await response.Content.ReadAsStringAsync();

            try
            {
                var weatherData = JsonSerializer.Deserialize<WeatherData>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (weatherData != null)
                {
                    _weatherDataList.Add(weatherData);
                }
                return weatherData;
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine("Deserialization error for weather data: " + jsonEx.Message);
                Console.WriteLine("Raw JSON: " + content);
                return null;
            }
        }

        public async Task<WeatherData> GetWeatherDataAsync(string cityName = "Wrocław")
        {
            string url = $"{WeatherApiBaseUrl}?q={cityName}&appid={_apiKey}&units=metric";
            Console.WriteLine($"Requesting Weather URL: {url}\n");

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: Received status code {response.StatusCode} when calling weather API.");
                return null;
            }

            string content = await response.Content.ReadAsStringAsync();
            try
            {
                var weatherData = JsonSerializer.Deserialize<WeatherData>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (weatherData != null)
                {
                    _weatherDataList.Add(weatherData);
                }
                return weatherData;
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine("Deserialization error for weather data: " + jsonEx.Message);
                Console.WriteLine("Raw JSON: " + content);
                return null;
            }
        }

        public string BuildWeatherSummary()
        {
            var sb = new StringBuilder();
            foreach (var weatherData in _weatherDataList)
            {
                sb.AppendLine(weatherData.ToString());
                sb.AppendLine(new string('-', 40));
            }
            return sb.ToString();
        }

    }
}

