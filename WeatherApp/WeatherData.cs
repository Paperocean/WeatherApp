using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApi
{
    public class WeatherData
    {
        public string Name { get; set; }
        public MainData Main { get; set; }
        public WeatherDescription[] Weather { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Weather in {Name}:");
            sb.AppendLine($"Temperature: {Main.Temp} °C");
            sb.AppendLine($"Feels like: {Main.Feels_like} °C");
            sb.AppendLine($"Min temperature: {Main.Temp_min} °C");
            sb.AppendLine($"Max temperature: {Main.Temp_max} °C");
            sb.AppendLine($"Pressure: {Main.Pressure} hPa");
            sb.AppendLine($"Humidity: {Main.Humidity}%");

            if (Weather != null && Weather.Length > 0)
            {
                sb.AppendLine($"Weather: {Weather[0].Main}");
                sb.AppendLine($"Description: {Weather[0].Description}");
            }

            return sb.ToString();
        }
    }

    public class MainData
    {
        public double Temp { get; set; }
        public double Feels_like { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
    }

    public class WeatherDescription
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }
}
