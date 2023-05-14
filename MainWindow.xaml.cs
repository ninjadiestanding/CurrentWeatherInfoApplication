using CurrentWeatherInfoApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrentWeatherInfoApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //Проверяем поле для ввода города на фактическое значение

            if (string.IsNullOrWhiteSpace(InputCityBox.Text))
            {
                dataGrid.ItemsSource = null;
                CitiesCountLabel.Content = "Cities found: " + 0;
                MessageBox.Show("Enter the name of the city in the empty field.");
                return;
            }

            /* Получаем объект CurrentWeatherData, передавая в метод GetWeatherAsync()
            название города и единицу измерения */

            var currentWeatherInfo = await WeatherService.GetWeatherAsync(InputCityBox.Text,
                ((ComboBoxItem)InputTemperatureUnitBox.SelectedValue).Name);

            UpdateUI(currentWeatherInfo, ((ComboBoxItem)InputTemperatureUnitBox.SelectedValue).Name);
        }


        private void UpdateUI(CurrentWeatherData currentWeatherInfo, string systemOfUnits)
        {
            //Проверяем currentWeatherInfo на содержание объектов

            if (currentWeatherInfo == null || currentWeatherInfo.Cities.Count == 0)
            {
                dataGrid.ItemsSource = null;
                CitiesCountLabel.Content = "Cities found: " + 0;
                MessageBox.Show($"City \"{InputCityBox.Text}\" not found.");
                return;
            }

            //Устанавливаем единицы измерения температуры и скорости для полей таблицы

            WeatherService.SetUnitsOfMeasurement(currentWeatherInfo, systemOfUnits);

            //Отображаем количество найденных городов

            CitiesCountLabel.Content = "Cities found: " + currentWeatherInfo.Count;

            //Устанавливаем источник данных для таблицы

            dataGrid.ItemsSource = currentWeatherInfo.Cities;
        }
    }
}
