using CurrentWeatherInfoApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Net.WebRequestMethods;

namespace CurrentWeatherInfoApplication
{
    internal class WeatherService
    {
        private const string CELSIUS_SYMBOL = "\u2103";
        private const string FAHRENHEIT_SYMBOL = "\u2109";
        private const string KELVIN_SYMBOL = "K";

        private const string METERS_PER_SEC = "meter/sec";
        private const string MILES_PER_HOUR = "miles/hour";

        //Метод для отправки запроса и обработки ответа

        public static async Task<CurrentWeatherData> GetWeatherAsync(string cityName, string systemOfUnits)
        {
            //Составляем адрес запроса из данных полученных в MainWindow

            string apiUrl = $"https://api.openweathermap.org/data/2.5/find?q={cityName}&type=like&APPID={ConfigurationManager.AppSettings["APPID"]}&units={systemOfUnits}&cnt=15";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //Отправляем HTTP-запрос на указанный адрес и получаем ответ в переменную

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    //Проверяем статус-код, в случае ошибочного ответа генерируется исключение и попадаем в блок catch

                    response.EnsureSuccessStatusCode();

                    //Получаем содержимое ответа

                    string responseBody = await response.Content.ReadAsStringAsync();

                    //Возвращаем десериализованный объект типа CurrentWeatherData

                    return JsonConvert.DeserializeObject<CurrentWeatherData>(responseBody);
                }
                catch
                {
                    return null;
                }
            } 
        }

        //Метод определяет единицы измерения для их дальнейшего отображения в каждой строке таблицы

        public static void SetUnitsOfMeasurement(CurrentWeatherData currentWeatherInfo, string systemOfUnits)
        {
            foreach (var city in currentWeatherInfo.Cities)
            {
                switch (systemOfUnits)
                {
                    case "metric":
                        city.TemperatureUnit = CELSIUS_SYMBOL;
                        city.UnitOfSpeed = METERS_PER_SEC;
                        break;
                    case "imperial":
                        city.TemperatureUnit = FAHRENHEIT_SYMBOL;
                        city.UnitOfSpeed = MILES_PER_HOUR;
                        break;
                    case "standard":
                        city.TemperatureUnit = KELVIN_SYMBOL;
                        city.UnitOfSpeed = METERS_PER_SEC;
                        break;
                }
            }
        }
    }
}
