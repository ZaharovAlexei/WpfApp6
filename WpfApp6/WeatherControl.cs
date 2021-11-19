using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp6
{
    //Разработать в WPF приложении класс WeatherControl, моделирующий погодную сводку:
    //температуру (целое число в диапазоне от -50 до +50),
    //направление ветра (строка), скорость ветра (целое число),
    //наличие осадков (возможные значения: 0 – солнечно, 1 – облачно, 2 – дождь, 3 – снег.
    //Можно использовать целочисленное значение, либо создать перечисление enum).
    //Свойство «температура» преобразовать в свойство зависимости.
    class WeatherControl : DependencyObject
    {
        public static readonly DependencyProperty TemperatureProperty;
        private string windDirection;
        private int windSpeed;
        private int precipitationIndex;
        public string WindDirection
        {
            get => windDirection;
            set => windDirection = value;
        }
        public int WindSpeed
        {
            get => windSpeed;
            set => windSpeed = value;
        }
        public int Precipitation
        {
            get
            {
                return precipitationIndex;
            }
            set
            {
                if (value >= 0 && value <= 3)
                    precipitationIndex = value;
                else
                    precipitationIndex = 0;
            }
        }
        public int Temperature
        {
            get => (int)GetValue(TemperatureProperty);
            set => SetValue(TemperatureProperty, value);
        }
        public WeatherControl(int temperature, string windDirection, int windSpeed, int precipitationIndex)
        {
            Temperature = temperature;
            WindDirection = windDirection;
            WindSpeed = windSpeed;
            Precipitation = precipitationIndex;
        }
        static WeatherControl()
        {
            TemperatureProperty = DependencyProperty.Register(
                nameof(Temperature),
                typeof(int),
                typeof(WeatherControl),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.Journal,
                    null,
                    new CoerceValueCallback(CoerceAge)),
                    new ValidateValueCallback(ValidateAge));
        }
        private static bool ValidateAge(object value)
        {
            int v = (int)value;
            if (v >= -50 && v <= 50)
                return true;
            else
                return false;
        }
        private static object CoerceAge(DependencyObject d, object baseValue)
        {
            int v = (int)baseValue;
            if (v >= -50)
                return v;
            else
                return 0;
        }
        public string Print()
        {
            string precipitation;
            switch (precipitationIndex)
            {
                case 0:
                    {
                        precipitation = "Cолнечно";
                        break;
                    }
                case 1:
                    {
                        precipitation = "Облачно";
                        break;
                    }
                case 2:
                    {
                        precipitation = "Дождь";
                        break;
                    }
                case 3:
                    {
                        precipitation = "Снег";
                        break;
                    }
                default:
                    {
                        precipitation = "нет данных";
                        break;
                    }
            }
            return $"{Temperature}, {WindDirection}, {WindSpeed}, {precipitation}";
        }
    }
}

