using WeatherApi;
using WeatherApi.Database;

namespace MauiApp12
{
    public partial class MainPage : ContentPage
    {
        private static WeatherRepository _weatherRepository;
        private static WeatherService _weatherService;


        public MainPage()
        {
            InitializeComponent();
            var apiKey = "477b9cacb970e77d9272509c8fbe64c4";
            var httpClient = new HttpClient();
            var weatherApp = new WeatherApp(httpClient, apiKey);

            _weatherRepository = new WeatherRepository();
            _weatherService = new WeatherService(_weatherRepository, weatherApp);

        }

        private async void CheckWeather(object sender, EventArgs e)
        {
            string cityName = CityNameEntry.Text;

            var weatherData = await _weatherService.GetOrFetchWeatherDataAsync(cityName);
            if (weatherData != null)
            {
                await DisplayAlert("Weather Data", weatherData.ToString(), "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to retrieve weather data.", "OK");
            }
        }

        private async void ListWeatherData(object sender, EventArgs e)
        {
            var weatherDataList = await _weatherRepository.GetWeatherDataListAsync();
            if (weatherDataList != null && weatherDataList.Any())
            {
                WeatherLabel.Text = string.Join("\n", weatherDataList.Select(wd => wd.ToString()));
            }
            else
            {
                WeatherLabel.Text = "No weather data available.";
            }
        }

    }
}
