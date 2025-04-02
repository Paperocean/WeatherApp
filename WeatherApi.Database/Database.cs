using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace WeatherApi.Database
{
    public class WeatherData
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required double Temp { get; set; }
        public double Feels_like { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Weather in {Name}:");
            sb.AppendLine($"Temperature: {Temp} °C");
            sb.AppendLine($"Feels like: {Feels_like} °C");
            sb.AppendLine($"Min temperature: {Temp_min} °C");
            sb.AppendLine($"Max temperature: {Temp_max} °C");
            sb.AppendLine($"Pressure: {Pressure} hPa");
            sb.AppendLine($"Humidity: {Humidity}%");

            sb.AppendLine(new string('-', 20));

            return sb.ToString();
        }
    }

    public class WeatherDbContext : DbContext
    {
        public DbSet<WeatherData> WeatherDatas { get; set; }

        public WeatherDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@"Data Source=Weather.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherData>().HasData(
                new WeatherData { Id = 1, Name = "Wrocław", Temp = 15.5, Feels_like = 15.0, Temp_min = 10, Temp_max = 18, Pressure = 1013, Humidity = 70 },
                new WeatherData { Id = 2, Name = "Warszawa", Temp = 12.5, Feels_like = 12.0, Temp_min = 8, Temp_max = 15, Pressure = 1015, Humidity = 75 },
                new WeatherData { Id = 3, Name = "Kraków", Temp = 14.5, Feels_like = 14.0, Temp_min = 9, Temp_max = 17, Pressure = 1012, Humidity = 72 }
            );
        }
    }
}
