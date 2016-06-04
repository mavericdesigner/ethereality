using System;

using ForecastIO;
using ForecastIO.Extensions;
using GalaSoft.MvvmLight;
using Isis.Model.Route;

namespace Isis.Model.Meteorology.WebWeather
{
    public class WebWeatherJson:ObservableObject
    {
        public WebWeatherJson()
        {

        }
        public Currently GetWebWeather(string api,float latitude,float longitude)
        {
            var request = new ForecastIORequest("800ff5cacaa097384f4cadb734d9cb6e", latitude, longitude, DateTime.Now, Unit.si);
            var response = request.Get();

            // Date/Time is represented by a Unix Timestamp
            var currentTime = response.currently.time;

            // Return a .NET DateTime object (UTC) using an extension (Notice the additional 'using' statement)
            var _currentTime = currentTime.ToDateTime();

            // Return a local .NET DateTime object
            var _localCurrentTime = currentTime.ToDateTime().ToLocalTime();

            return response.currently;
        }


    }
}
