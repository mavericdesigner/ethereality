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

        private ForecastApi Client { get; set; }

        public WeatherForecast()
        {
            Client = new ForecastApi("800ff5cacaa097384f4cadb734d9cb6e");
        }

        public async Task<Forecast> GetWeatherService(double latitude, double longitude)
        {
            Forecast result = await Client.GetWeatherDataAsync(latitude, longitude);
            return result;
        }

        public async Task<List<Forecast>> GetWeatherServiceCollection(ICollection<LatLong> latLongCollection)
        {
            List<Forecast> results = new List<Forecast>();
            Forecast result;
            foreach (var latlong in latLongCollection)
            {
                result = new Forecast();
                result = await Client.GetWeatherDataAsync(latlong.Latitude, latlong.Longitude);
                results.Add(result);
            }

            return results;
        }

        public async Task<List<Forecast>> GetWeatherServiceCollection(ICollection<LatLongTime> latLongTimeCollection)
        {
            List<Forecast> results = new List<Forecast>();
            Forecast result;
            foreach (var latlongtime in latLongTimeCollection)
            {
                result = new Forecast();
                result = await Client.GetTimeMachineWeatherAsync(latlongtime.Latitude, latlongtime.Longitude, latlongtime.Time);
                results.Add(result);
            }

            return results;
        }

        public async Task<Forecast> GetWeatherHistoryService(double latitude, double longitude, DateTimeOffset date)
        {
            Forecast result = await Client.GetTimeMachineWeatherAsync(latitude, longitude, date);
            return result;
        }
    }
}