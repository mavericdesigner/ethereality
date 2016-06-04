using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Xml;

namespace Isis.Model.Meteorology.WebWeather
{
    public class WebWeatherModelOpenXML : ObservableObject
    {
        public WebWeatherModelOpenXML()
        {
            _conditionsCC = new OpenWeatherCC();
        }

        /// <summary>
        /// The <see cref="ConditionsCC" /> property's name.
        /// </summary>
        public const string ConditionsCCPropertyName = "ConditionsCC";

        private OpenWeatherCC _conditionsCC;

        /// <summary>
        /// Sets and gets the ConditionsCC property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public OpenWeatherCC ConditionsCC
        {
            get
            {
                return _conditionsCC;
            }

            set
            {
                RaisePropertyChanging(ConditionsCCPropertyName);
                _conditionsCC = value;
                RaisePropertyChanged(ConditionsCCPropertyName);
            }
        }

        /// <summary>
        /// The function that returns the current conditions for the specified location.
        /// </summary>
        /// <param name="location">City or ZIP code</param>
        /// <returns></returns>
        public void GetCurrentConditions()
        {
            OpenWeatherCC LocalConditionsCC = _conditionsCC;

            XmlDocument xmlConditions = new XmlDocument();
            xmlConditions.Load(string.Format("http://api.openweathermap.org/data/2.5/weather?q=Johannesburg&mode=xml"));

            if (xmlConditions.SelectSingleNode("xml_api_reply/weather/problem_cause") != null)
            {
            }
            else
            {
                LocalConditionsCC.CityID = xmlConditions.SelectSingleNode("current/city").Attributes["id"].InnerText;
                LocalConditionsCC.CityName = xmlConditions.SelectSingleNode("current/city").Attributes["name"].InnerText;
                LocalConditionsCC.CityLatitude = xmlConditions.SelectSingleNode("current/city/coord").Attributes["lat"].InnerText;
                LocalConditionsCC.CityLongitude = xmlConditions.SelectSingleNode("current/city/coord").Attributes["lon"].InnerText;
                LocalConditionsCC.TempValue = xmlConditions.SelectSingleNode("current/temperature").Attributes["value"].InnerText;
                LocalConditionsCC.TempMin = xmlConditions.SelectSingleNode("current/temperature").Attributes["min"].InnerText;
                LocalConditionsCC.TempMax = xmlConditions.SelectSingleNode("current/temperature").Attributes["max"].InnerText;
                LocalConditionsCC.TempUnit = xmlConditions.SelectSingleNode("current/temperature").Attributes["unit"].InnerText;
                LocalConditionsCC.HumValue = xmlConditions.SelectSingleNode("current/humidity").Attributes["value"].InnerText;
                LocalConditionsCC.HumUnit = xmlConditions.SelectSingleNode("current/humidity").Attributes["unit"].InnerText;
                LocalConditionsCC.PressValue = xmlConditions.SelectSingleNode("current/pressure").Attributes["value"].InnerText;
                LocalConditionsCC.PressUnit = xmlConditions.SelectSingleNode("current/pressure").Attributes["unit"].InnerText;
                LocalConditionsCC.WindSpeedValue = xmlConditions.SelectSingleNode("current/wind/speed").Attributes["value"].InnerText;
                LocalConditionsCC.WindSpeedName = xmlConditions.SelectSingleNode("current/wind/speed").Attributes["name"].InnerText;
                LocalConditionsCC.WindDirectValue = xmlConditions.SelectSingleNode("current/wind/direction").Attributes["value"].InnerText;
                LocalConditionsCC.WindDirectCode = xmlConditions.SelectSingleNode("current/wind/direction").Attributes["code"].InnerText;
                LocalConditionsCC.WindDirectName = xmlConditions.SelectSingleNode("current/wind/direction").Attributes["name"].InnerText;
                LocalConditionsCC.PrecipitMode = xmlConditions.SelectSingleNode("current/precipitation").Attributes["mode"].InnerText;
                LocalConditionsCC.WeatherNumber = xmlConditions.SelectSingleNode("current/weather").Attributes["number"].InnerText;
                LocalConditionsCC.WeatherValue = xmlConditions.SelectSingleNode("current/weather").Attributes["value"].InnerText;
                LocalConditionsCC.WeatherIcon = xmlConditions.SelectSingleNode("current/weather").Attributes["icon"].InnerText;
                LocalConditionsCC.LastUpdateValue = xmlConditions.SelectSingleNode("current/lastupdate").Attributes["value"].InnerText;
            }

            ConditionsCC = LocalConditionsCC;
        }

        /// <summary>
        /// The function that gets the forecast for the next four days.
        /// </summary>
        /// <param name="location">City or ZIP code</param>
        /// <returns></returns>
        public static List<ConditionsFC> GetForecast(string location)
        {
            List<ConditionsFC> conditionsFC = new List<ConditionsFC>();

            XmlDocument xmlConditions = new XmlDocument();
            xmlConditions.Load(string.Format("http://www.google.com/ig/api?weather={0}", location));

            if (xmlConditions.SelectSingleNode("xml_api_reply/weather/problem_cause") != null)
            {
                conditionsFC = null;
            }
            else
            {
                foreach (XmlNode node in xmlConditions.SelectNodes("/xml_api_reply/weather/forecast_conditions"))
                {
                    ConditionsFC conditionFC = new ConditionsFC();
                    conditionFC.City = xmlConditions.SelectSingleNode("/xml_api_reply/weather/forecast_information/city").Attributes["data"].InnerText;
                    conditionFC.Condition = node.SelectSingleNode("condition").Attributes["data"].InnerText;
                    conditionFC.High = node.SelectSingleNode("high").Attributes["data"].InnerText;
                    conditionFC.Low = node.SelectSingleNode("low").Attributes["data"].InnerText;
                    conditionFC.DayOfWeek = node.SelectSingleNode("day_of_week").Attributes["data"].InnerText;
                    conditionsFC.Add(conditionFC);
                }
            }

            return conditionsFC;
        }
    }
}