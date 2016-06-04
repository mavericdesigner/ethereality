using ForecastIOPortable;
using ForecastIOPortable.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ethereality.Meteorology
{
    public class WeatherForecast : IWeatherForecast
    {
        public struct LatLong
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        public struct LatLongTime
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public DateTimeOffset Time { get; set; }
        }

        private ForecastApi client { get; set; }

        public WeatherForecast()
        {
            client = new ForecastApi("800ff5cacaa097384f4cadb734d9cb6e");
        }

        public async Task<Forecast> GetWeatherService(double latitude, double longitude)
        {
            Forecast result = await client.GetWeatherDataAsync(latitude, longitude);
            return result;
        }

        public async Task<List<Forecast>> GetWeatherServiceCollection(ICollection<LatLong> LatLongCollection)
        {
            List<Forecast> results = new List<Forecast>();
            Forecast result;
            foreach (var latlong in LatLongCollection)
            {
                result = new Forecast();
                result = await client.GetWeatherDataAsync(latlong.Latitude, latlong.Longitude);
                results.Add(result);
            }

            return results;
        }

        public async Task<List<Forecast>> GetWeatherServiceCollection(ICollection<LatLongTime> LatLongTimeCollection)
        {
            List<Forecast> results = new List<Forecast>();
            Forecast result;
            foreach (var latlongtime in LatLongTimeCollection)
            {
                result = new Forecast();
                result = await client.GetTimeMachineWeatherAsync(latlongtime.Latitude, latlongtime.Longitude, latlongtime.Time);
                results.Add(result);
            }

            return results;
        }

        public async Task<Forecast> GetWeatherHistoryService(double latitude, double longitude, DateTimeOffset date)
        {
            Forecast result = await client.GetTimeMachineWeatherAsync(latitude, longitude, date);
            return result;
        }
    }
}