namespace SPA
{
  

    public class SpaService
    {

        private Spa_Data SpaValues;
    
        public int SpaErrorCode { get; set; }
  

        #region Constructors

        public SpaService()
        {
            SpaValues= new Spa_Data();
        }

        #endregion Constructors

        #region Methods

        public int SpaDataCalculate(Spa_Data SpaValues)
        {
            //declare the SPA structure
            int result;
            double min, sec;
          /*  //_spaValues = new Spa_Data();

            ////enter required input values into SPA structure
            //_spaValues.Function = (int)SpaSelect.SPA_ALL;
            //_spaValues.Year = _spaDataLink.SpaDataProp.Year;
            //_spaValues.Month = _spaDataLink.SpaDataProp.Month;
            //_spaValues.Day = _spaDataLink.SpaDataProp.Day;
            //_spaValues.Hour = _spaDataLink.SpaDataProp.Hour;
            //_spaValues.Minute = _spaDataLink.SpaDataProp.Minute;
            //_spaValues.Second = _spaDataLink.SpaDataProp.Second;
            //_spaValues.Timezone = _spaDataLink.SpaDataProp.Timezone;
            //_spaValues.Delta_ut1 = _spaDataLink.SpaDataProp.Delta_ut1;
            //_spaValues.Delta_t = _spaDataLink.SpaDataProp.Delta_t;
            //_spaValues.Longitude = _spaDataLink.SpaDataProp.Longitude;
            //_spaValues.Latitude = _spaDataLink.SpaDataProp.Latitude;
            //_spaValues.Elevation = _spaDataLink.SpaDataProp.Elevation;
            //_spaValues.Pressure = _spaDataLink.SpaDataProp.Pressure;
            //_spaValues.Temperature = _spaDataLink.SpaDataProp.Temperature;
            //_spaValues.Slope = _spaDataLink.SpaDataProp.Slope;
            //_spaValues.Azm_rotation = _spaDataLink.SpaDataProp.Azm_rotation;
            //_spaValues.Atmos_refract = _spaDataLink.SpaDataProp.Atmos_refract;
            //_spaValues.Function = _spaDataLink.SpaDataProp.Function;
            //call the _spaValues calculate function and pass the _spaValues structure*/
            Spa spawork = new Spa();
            result = spawork.spa_calculate(ref SpaValues);

            if (result == 0)  //check for SPA errors
            {
                //display the results inside the SPA structure
                //Console.WriteLine("Julian Day:    {0}", _spaValues.jd);
                //Console.WriteLine("L:             {0} degrees", _spaValues.l);
                //Console.WriteLine("B:             {0} degrees", _spaValues.b);
                //Console.WriteLine("R:             {0} AU", _spaValues.r);
                //Console.WriteLine("H:             {0} degrees", _spaValues.h);
                //Console.WriteLine("Delta Psi:     {0} degrees", _spaValues.del_psi);
                //Console.WriteLine("Delta Epsilon: {0} degrees", _spaValues.del_epsilon);
                //Console.WriteLine("Epsilon:       {0} degrees", _spaValues.epsilon);
                //Console.WriteLine("Zenith:        {0} degrees", _spaValues.zenith);
                //Console.WriteLine("Azimuth:       {0} degrees", _spaValues.azimuth);
                //Console.WriteLine("Incidence:     {0} degrees", _spaValues.incidence);

                min = 60.0 * (SpaValues.Sunrise - (int)(SpaValues.Sunrise));
                sec = 60.0 * (min - (int)min);
                //Console.Write("Sunrise:       {0}:{1}:{2} Local Time\n", (int)(_spaValues.sunrise), (int)min, (int)sec);

                min = 60.0 * (SpaValues.Sunset - (int)(SpaValues.Sunset));
                sec = 60.0 * (min - (int)min);
                //Console.Write("Sunset:        {0}:{1}:{2} Local Time\n", (int)(_spaValues.sunset), (int)min, (int)sec);
                //_spaDataLink.SpaDataProp = _spaValues;
            }
            else
             return result; 

            return 0;
        }

        //public int SpaDataCalculate()
        //{
        //    Spa_Data spa = _spaValues;

        //    //declare the SPA structure
        //    int result;
        //    double min, sec;

        //    //enter required input values into SPA structure

        //    spa.year = 2003;
        //    spa.month = 10;
        //    spa.day = 17;
        //    spa.hour = 12;
        //    spa.minute = 30;
        //    spa.second = 30;
        //    spa.timezone = -7.0;
        //    spa.delta_ut1 = 0;
        //    spa.delta_t = 67;
        //    spa.longitude = -105.1786;
        //    spa.latitude = 39.742476;
        //    spa.elevation = 1830.14;
        //    spa.pressure = 820;
        //    spa.temperature = 11;
        //    spa.slope = 30;
        //    spa.azm_rotation = -10;
        //    spa.atmos_refract = 0.5667;
        //    spa.function = (int)SpaSelect.SPA_ALL;

        //    //call the SPA calculate function and pass the SPA structure
        //    Spa spawork = new Spa();
        //    result = spawork.spa_calculate(ref spa);
        //    _spaValues = spa;
        //    if (result == 0)  //check for SPA errors
        //    {
        //        //display the results inside the SPA structure
        //        //Console.WriteLine("Julian Day:    {0}", spa.jd);
        //        //Console.WriteLine("L:             {0} degrees", spa.l);
        //        //Console.WriteLine("B:             {0} degrees", spa.b);
        //        //Console.WriteLine("R:             {0} AU", spa.r);
        //        //Console.WriteLine("H:             {0} degrees", spa.h);
        //        //Console.WriteLine("Delta Psi:     {0} degrees", spa.del_psi);
        //        //Console.WriteLine("Delta Epsilon: {0} degrees", spa.del_epsilon);
        //        //Console.WriteLine("Epsilon:       {0} degrees", spa.epsilon);
        //        //Console.WriteLine("Zenith:        {0} degrees", spa.zenith);
        //        //Console.WriteLine("Azimuth:       {0} degrees", spa.azimuth);
        //        //Console.WriteLine("Incidence:     {0} degrees", spa.incidence);

        //        min = 60.0 * (spa.sunrise - (int)(spa.sunrise));
        //        sec = 60.0 * (min - (int)min);
        //        //Console.Write("Sunrise:       {0}:{1}:{2} Local Time\n", (int)(spa.sunrise), (int)min, (int)sec);

        //        min = 60.0 * (spa.sunset - (int)(spa.sunset));
        //        sec = 60.0 * (min - (int)min);
        //        //Console.Write("Sunset:        {0}:{1}:{2} Local Time\n", (int)(spa.sunset), (int)min, (int)sec);
        //    }
        //    else
        //    { return result; }

        //    return 0;
        //}

        #endregion Methods
    }
}