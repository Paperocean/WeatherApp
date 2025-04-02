using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WeatherApi.Database
{
    public class WeatherRepository
    {
        public async Task<WeatherData?> GetWeatherDataFromDbAsync(string cityName)
        {
            using (var dbContext = new WeatherDbContext())
            {
                return await dbContext.WeatherDatas.FirstOrDefaultAsync(w => w.Name == cityName);
            }
        }

        public async Task SaveWeatherDataAsync(WeatherData weatherData)
        {
            using (var dbContext = new WeatherDbContext())
            {
                dbContext.WeatherDatas.Add(weatherData);
                await dbContext.SaveChangesAsync();
            }
        }

        public static async Task DeleteWeatherDataAsync(string cityName)
        {
            using (var dbContext = new WeatherDbContext())
            {
                var weatherData = await dbContext.WeatherDatas.FirstOrDefaultAsync(w => w.Name == cityName);
                if (weatherData != null)
                {
                    dbContext.WeatherDatas.Remove(weatherData);
                    await dbContext.SaveChangesAsync();
                    Console.WriteLine($"Deleted weather data for {cityName}.");
                }
                else
                {
                    Console.WriteLine($"No data found for {cityName}.");
                }
            }
        }

        public static async Task ListWeatherDataAsync()
        {
            using (var dbContext = new WeatherDbContext())
            {
                var allData = await dbContext.WeatherDatas.ToListAsync();
                if (allData.Any())
                {
                    Console.WriteLine("\nStored Weather Data:");
                    foreach (var data in allData)
                    {
                        Console.WriteLine(data);
                    }
                }
                else
                {
                    Console.WriteLine("No weather data stored.");
                }
            }
        }
        public async Task<List<WeatherData>> GetWeatherDataListAsync()
        {
            using (var dbContext = new WeatherDbContext())
            {
                return await dbContext.WeatherDatas
                     .OrderByDescending(w => w.Id)
                     .ToListAsync();
            }
        }

    }
}
