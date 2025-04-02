# WeatherApp

WeatherApp to wieloplatformowa aplikacja demonstrująca dostęp do informacji pogodowych z API OpenWeather. Zawiera projekt .NET MAUI dla klientów mobilnych i desktopowych, aplikację konsolową do testowania przez wiersz poleceń oraz projekt bazy danych wykorzystujący EF Core z SQLite do przechowywania danych pogodowych.

## Przegląd

- **MauiApp**  
  Wieloplatformowa aplikacja MAUI (MauiApp12.csproj) zapewniająca interfejs użytkownika do sprawdzania pogody i przeglądania zapisanych danych.

- **WeatherApi.Database**  
  Biblioteka klas (WeatherApi.Database.csproj) zawierająca kontekst bazy danych EF Core i modele.

- **WeatherApi**  
  Aplikacja konsolowa (WeatherApi.csproj) wykorzystująca API pogodowe w trybie interakcji konsolowej.

## Struktura projektu

```
.
├── MauiApp/                   # Aplikacja mobilna/desktopowa .NET MAUI
│   ├── App.xaml               # Definicja aplikacji (UI)
│   ├── MainPage.xaml          # Główny interfejs strony
│   ├── AppShell.xaml          # Definicja powłoki
│   ├── MauiProgram.cs         # Konfiguracja aplikacji MAUI
│   └── Platforms/             # Implementacje specyficzne dla platform
│
├── WeatherApi.Database/       # Projekt bazy danych EF Core
│   ├── Database.cs            # Model danych i DbContext
│   ├── WeatherRepository.cs   # Repozytorium do dostępu/modyfikacji danych
│   └── Migrations/            # Migracje bazy danych i snapshot
│
└── WeatherApp/                # Aplikacja konsolowa do sprawdzania pogody
    ├── Program.cs             # Punkt wejścia dla aplikacji konsolowej
    ├── WeatherApp.cs          # Klasa opakowująca API pogodowe
    ├── WeatherData.cs         # DTO reprezentujące dane pogodowe z API
    └── WeatherService.cs      # Logika biznesowa i usługa
```

## Schemat relacji encji (ERD)

W mojej aplikacji używam prostej struktury bazy danych do przechowywania danych pogodowych. Główną encją jest `WeatherData` z następującymi właściwościami:

```
WeatherData
+-------------+--------------+--------------------------+
| Właściwość  | Typ          | Opis                    |
+-------------+--------------+--------------------------+
| Id          | int          | Klucz główny            |
| Name        | string       | Nazwa miasta            |
| Temp        | double       | Temperatura             |
| Feels_like  | double       | Odczuwalna temperatura  |
| Temp_min    | double       | Minimalna temperatura   |
| Temp_max    | double       | Maksymalna temperatura  |
| Pressure    | int          | Ciśnienie powietrza     |
| Humidity    | int          | Wilgotność %            |
+-------------+--------------+--------------------------+
```

Encja `WeatherData` reprezentuje informacje pogodowe dla konkretnego miasta, z `Id` jako kluczem głównym i `Name` przechowującym nazwę miasta. Wartości temperatury są przechowywane w stopniach Celsjusza jako liczby zmiennoprzecinkowe, podczas gdy ciśnienie i wilgotność są przechowywane jako liczby całkowite.

## Operacje na bazie danych

W mojej aplikacji zaimplementowałem kilka operacji na bazie danych do zarządzania danymi pogodowymi:

### Wyświetlanie danych pogodowych

```csharp
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
```

Ta metoda pobiera wszystkie rekordy danych pogodowych z bazy danych i wyświetla je w konsoli. Jeśli nie znaleziono żadnych rekordów, pokazuje odpowiedni komunikat.

Dodatkowo na bazie danych zaimplementowałem operację:

- Zapisywania nowych danych pogodowych

Ta operacja jest zaimplementowane w klasie `WeatherRepository` przy użyciu Entity Framework Core.

## Funkcje

- **Wieloplatformowy interfejs użytkownika**: Aplikacja MAUI działa na Androidzie, iOS, Windows i Mac.
- **Integracja z API pogodowym**: Pobiera dane pogodowe z API OpenWeather przy użyciu `HttpClient`.
- **Lokalna baza danych**: Przechowuje dane pogodowe lokalnie przy użyciu SQLite i EF Core.
- **Wzorzec repozytorium**: Umożliwia zapytania, zapisywanie, wyświetlanie i usuwanie rekordów danych pogodowych.

### Migracje bazy danych

W projekcie `WeatherApi.Database` używam EF Core z SQLite.

## Sposób użycia

### W aplikacji MAUI

- Wprowadź nazwę miasta i stuknij **Check Weather**, aby pobrać i wyświetlić aktualne dane pogodowe.
- Użyj opcji **List Saved Cities**, aby wyświetlić wszystkie lokalnie przechowywane rekordy pogodowe.

### W aplikacji konsolowej

Uruchom aplikację konsolową, aby uzyskać interfejs oparty na menu, w którym możesz:

1. Pobrać pogodę, wprowadzając nazwę miasta.
2. Wyświetlić wszystkie dane pogodowe przechowywane w bazie danych.
3. Wyjść z aplikacji.

## Najważniejsze elementy kodu

- **Integracja z API pogodowym**:  
  Klasa `WeatherApp` buduje adres URL API (używając mojego klucza API) i deserializuje dane JSON do obiektu `WeatherData`.

- **Przechowywanie danych**:  
  `WeatherRepository` obsługuje wszystkie operacje bazodanowe, a `WeatherDbContext` konfiguruje lokalną bazę danych SQLite.

- **Warstwa usług**:  
  `WeatherService` sprawdza, czy dane dla miasta są już przechowywane lokalnie i pobiera nowe dane, jeśli to konieczne.
