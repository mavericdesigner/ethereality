using ForecastIOPortable.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ethereality.Meteorology
{
    public interface IWeatherForecast
    {
        Task<Forecast> GetWeatherHistoryService(double latitude, double longitude, DateTimeOffset date);

        Task<Forecast> GetWeatherService(double latitude, double longitude);

        Task<List<Forecast>> GetWeatherServiceCollection(ICollection<WeatherForecast.LatLongTime> latLongTimeCollection);

        Task<List<Forecast>> GetWeatherServiceCollection(ICollection<WeatherForecast.LatLong> latLongCollection);
    }
}