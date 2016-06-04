namespace Isis.Model.Meteorology.WebWeather
{
    public struct ConditionsFC
    {
        public string City;
        public string Condition;
        public string TempF;
        public string TempC;
        public string Humidity;
        public string Wind;
        public string DayOfWeek;
        public string High;
        public string Low;
        public string errorMsg;
    }

    public struct OpenWeatherCC
    {
        public string CityID { get; set; }

        public string CityName { get; set; }

        public string CityLatitude { get; set; }

        public string CityLongitude { get; set; }

        public string TempValue { get; set; }

        public string TempMin { get; set; }

        public string TempMax { get; set; }

        public string TempUnit { get; set; }

        public string HumValue { get; set; }

        public string HumUnit { get; set; }

        public string PressValue { get; set; }

        public string PressUnit { get; set; }

        public string WindSpeedValue { get; set; }

        public string WindSpeedName { get; set; }

        public string WindDirectValue { get; set; }

        public string WindDirectCode { get; set; }

        public string WindDirectName { get; set; }

        public string PrecipitMode { get; set; }

        public string WeatherNumber { get; set; }

        public string WeatherValue { get; set; }

        public string WeatherIcon { get; set; }

        public string LastUpdateValue { get; set; }
    }
}