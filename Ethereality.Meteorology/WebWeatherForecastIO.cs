using ForecastIO;
using ForecastIO.Extensions;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;

namespace Isis.Model.Meteorology.WebWeather
{
    public class WebWeatherForecastIO : ObservableObject
    {
        public WebWeatherForecastIO()
        {
        }

        public Currently WebWeatherCurrent(string api, float latitude, float longitude)
        {
            var request = new ForecastIORequest(api, latitude, longitude, DateTime.Now, Unit.si);
            var response = request.Get();

            // Date/Time is represented by a Unix Timestamp
            var currentTime = response.currently.time;

            // Return a .NET DateTime object (UTC) using an extension (Notice the additional 'using' statement)
            var _currentTime = currentTime.ToDateTime();

            // Return a local .NET DateTime object
            var _localCurrentTime = currentTime.ToDateTime().ToLocalTime();

            return response.currently;
        }

        public List<DailyForecast> WeatherDailyForecast(string api, float latitude, float longitude)
        {
            var request = new ForecastIORequest(api, latitude, longitude, Unit.si);
            var response = request.Get();

            // Date/Time is represented by a Unix Timestamp
            var currentTime = response.currently.time;

            // Return a .NET DateTime object (UTC) using an extension (Notice the additional 'using' statement)
            var _currentTime = currentTime.ToDateTime();

            // Return a local .NET DateTime object
            var _localCurrentTime = currentTime.ToDateTime().ToLocalTime();

            return response.daily.data;
        }

        public List<HourForecast> WeatherHourlyForecast(string api, float latitude, float longitude)
        {
            var request = new ForecastIORequest(api, latitude, longitude, Unit.si);
            var response = request.Get();

            // Date/Time is represented by a Unix Timestamp
            var currentTime = response.currently.time;

            // Return a .NET DateTime object (UTC) using an extension (Notice the additional 'using' statement)
            var _currentTime = currentTime.ToDateTime();

            // Return a local .NET DateTime object
            var _localCurrentTime = currentTime.ToDateTime().ToLocalTime();

            return response.hourly.data;
        }
    }
}