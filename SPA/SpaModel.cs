/////////////////////////////////////////////
//          HEADER FILE for SPA.C          //
//                                         //
//      Solar Position Algorithm (SPA)     //
//                   for                   //
//        Solar Radiation Application      //
//                                         //
//               May 12, 2003              //
//                                         //
//   Filename: SPA.H                       //
//                                         //
//   Afshin Michael Andreas                //
//   afshin_andreas@nrel.gov (303)384-6383 //
//                                         //
//   Measurement & Instrumentation Team    //
//   Solar Radiation Research Laboratory   //
//   National Renewable Energy Laboratory  //
//   1617 Cole Blvd, Golden, CO 80401      //
/////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////
//                                                                    //
// Usage:                                                             //
//                                                                    //
//   1) In calling program, include this header file,                 //
//      by adding this line to the top of file:                       //
//           #include "spa.h"                                         //
//                                                                    //
//   2) In calling program, declare the SPA structure:                //
//           spa_data spa;                                            //
//                                                                    //
//   3) Enter the required input values into SPA structure            //
//      (input values listed in comments below)                       //
//                                                                    //
//   4) Call the SPA calculate function and pass the SPA structure    //
//      (prototype is declared at the end of this header file):       //
//           spa_calculate(&spa);                                     //
//                                                                    //
//   Selected output values (listed in comments below) will be        //
//   computed and returned in the passed SPA structure.  Output       //
//   will based on function code selected from enumeration below.     //
//                                                                    //
//   Note: A non-zero return code from spa_calculate() indicates that //
//         one of the input values did not pass simple bounds tests.  //
//         The valid input ranges and return error codes are also     //
//         listed below.                                              //
//                                                                    //
////////////////////////////////////////////////////////////////////////
namespace SPA
{
    //enumeration for function codes to select desired final outputs from SPA
    public enum SpaSelect : int
    {
        SpaZa,           //calculate zenith and azimuth
        SpaZaInc,       //calculate zenith, azimuth, and incidence
        SpaZaRts,       //calculate zenith, azimuth, and sun rise/transit/set values
        SpaAll,          //calculate all SPA output values
    };

    //Calculate SPA output values (in structure) based on input values passed in structure
    public struct SpaData
    {
        //----------------------INPUT VALUES------------------------

        private double _year;            // 4-digit year,    valid range: -2000 to 6000, error code: 1

        public double Year
        {
            get { return _year; }
            set { _year = value; }
        }

        private int _month;          // 2-digit month,         valid range: 1 to 12, error code: 2

        public int Month
        {
            get { return _month; }
            set { _month = value; }
        }

        private int _day;             // 2-digit day,           valid range: 1 to 31, error code: 3

        public int Day
        {
            get { return _day; }
            set { _day = value; }
        }

        private int _hour;            // Observer local hour,   valid range: 0 to 24, error code: 4

        public int Hour
        {
            get { return _hour; }
            set { _hour = value; }
        }

        private int _minute;          // Observer local minute, valid range: 0 to 59, error code: 5

        public int Minute
        {
            get { return _minute; }
            set { _minute = value; }
        }

        private int _second;          // Observer local second, valid range: 0 to 59, error code: 6

        public int Second
        {
            get { return _second; }
            set { _second = value; }
        }

        private double _deltaUt1;    // Fractional second difference between UTC and UT which is used

        public double DeltaUt1
        {
            get { return _deltaUt1; }
            set { _deltaUt1 = value; }
        }

        // to adjust UTC for earth's irregular rotation rate and is derived
        // from observation only and is reported in this bulletin:
        // http://maia.usno.navy.mil/ser7/ser7.dat,
        // where delta_ut1 = DUT1
        // valid range: -1 to 1 second (exclusive), error code 17

        private double _deltaT;      // Difference between earth rotation time and terrestrial time

        public double DeltaT
        {
            get { return _deltaT; }
            set { _deltaT = value; }
        }

        // It is derived from observation only and is reported in this
        // bulletin: http://maia.usno.navy.mil/ser7/ser7.dat,
        // where delta_t = 32.184 + (TAI-UTC) + DUT1
        // valid range: -8000 to 8000 seconds, error code: 7

        private double _timezone;     // Observer time zone (negative west of Greenwich)

        public double Timezone
        {
            get { return _timezone; }
            set { _timezone = value; }
        }

        // valid range: -18   to   18 hours,   error code: 8

        private double _longitude;    // Observer longitude (negative west of Greenwich)

        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        // valid range: -180  to  180 degrees, error code: 9

        private double _latitude;     // Observer latitude (negative south of equator)

        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        // valid range: -90   to   90 degrees, error code: 10

        private double _elevation;    // Observer elevation [meters]

        public double Elevation
        {
            get { return _elevation; }
            set { _elevation = value; }
        }

        // valid range: -6500000 or higher meters,    error code: 11

        private double _pressure;     // Annual average local pressure [millibars]

        public double Pressure
        {
            get { return _pressure; }
            set { _pressure = value; }
        }

        // valid range:    0 to 5000 millibars,       error code: 12

        private double _temperature;  // Annual average local temperature [degrees Celsius]

        public double Temperature
        {
            get { return _temperature; }
            set { _temperature = value; }
        }

        // valid range: -273 to 6000 degrees Celsius, error code; 13

        private double _slope;        // Surface slope (measured from the horizontal plane)

        public double Slope
        {
            get { return _slope; }
            set { _slope = value; }
        }

        // valid range: -360 to 360 degrees, error code: 14

        private double _azmRotation; // Surface azimuth rotation (measured from south to projection of

        public double AzmRotation
        {
            get { return _azmRotation; }
            set { _azmRotation = value; }
        }

        //     surface normal on horizontal plane, negative west)
        // valid range: -360 to 360 degrees, error code: 15

        private double _atmosRefract;// Atmospheric refraction at sunrise and sunset (0.5667 deg is typical)

        public double AtmosRefract
        {
            get { return _atmosRefract; }
            set { _atmosRefract = value; }
        }

        // valid range: -5   to   5 degrees, error code: 16

        private int _function;        // Switch to choose functions for desired output (from enumeration)

        public int Function
        {
            get { return _function; }
            set { _function = value; }
        }

        //-----------------Intermediate OUTPUT VALUES--------------------

        private double _jd;          //Julian day

        public double Jd
        {
            get { return _jd; }
            set { _jd = value; }
        }

        private double _jc;          //Julian century

        public double Jc
        {
            get { return _jc; }
            set { _jc = value; }
        }

        private double _jde;         //Julian ephemeris day

        public double Jde
        {
            get { return _jde; }
            set { _jde = value; }
        }

        private double _jce;         //Julian ephemeris century

        public double Jce
        {
            get { return _jce; }
            set { _jce = value; }
        }

        public double Jme;         //Julian ephemeris millennium

        private double _l;           //earth heliocentric longitude [degrees]

        public double L
        {
            get { return _l; }
            set { _l = value; }
        }

        private double _b;           //earth heliocentric latitude [degrees]

        public double B
        {
            get { return _b; }
            set { _b = value; }
        }

        private double _r;           //earth radius vector [Astronomical Units, AU]

        public double R
        {
            get { return _r; }
            set { _r = value; }
        }

        private double _theta;       //geocentric longitude [degrees]

        public double Theta
        {
            get { return _theta; }
            set { _theta = value; }
        }

        private double _beta;        //geocentric latitude [degrees]

        public double Beta
        {
            get { return _beta; }
            set { _beta = value; }
        }

        private double _x0;          //mean elongation (moon-sun) [degrees]

        public double X0
        {
            get { return _x0; }
            set { _x0 = value; }
        }

        private double _x1;          //mean anomaly (sun) [degrees]

        public double X1
        {
            get { return _x1; }
            set { _x1 = value; }
        }

        private double _x2;          //mean anomaly (moon) [degrees]

        public double X2
        {
            get { return _x2; }
            set { _x2 = value; }
        }

        private double _x3;          //argument latitude (moon) [degrees]

        public double X3
        {
            get { return _x3; }
            set { _x3 = value; }
        }

        private double _x4;          //ascending longitude (moon) [degrees]

        public double X4
        {
            get { return _x4; }
            set { _x4 = value; }
        }

        public double DelPsi;     //nutation longitude [degrees]
        public double DelEpsilon; //nutation obliquity [degrees]
        public double Epsilon0;    //ecliptic mean obliquity [arc seconds]
        public double Epsilon;     //ecliptic true obliquity  [degrees]
        public double DelTau;     //aberration correction [degrees]
        public double Lamda;       //apparent sun longitude [degrees]
        public double Nu0;         //Greenwich mean sidereal time [degrees]
        public double Nu;          //Greenwich sidereal time [degrees]
        public double Alpha;       //geocentric sun right ascension [degrees]
        public double Delta;       //geocentric sun declination [degrees]
        public double H;           //observer hour angle [degrees]
        public double Xi;          //sun equatorial horizontal parallax [degrees]
        public double DelAlpha;   //sun right ascension parallax [degrees]
        public double DeltaPrime; //topocentric sun declination [degrees]
        public double AlphaPrime; //topocentric sun right ascension [degrees]
        public double HPrime;     //topocentric local hour angle [degrees]
        public double E0;          //topocentric elevation angle (uncorrected) [degrees]
        public double DelE;       //atmospheric refraction correction [degrees]
        public double E;           //topocentric elevation angle (corrected) [degrees]
        public double Eot;         //equation of time [minutes]
        public double Srha;        //sunrise hour angle [degrees]
        public double Ssha;        //sunset hour angle [degrees]
        public double Sta;         //sun transit altitude [degrees]

        //---------------------Final OUTPUT VALUES------------------------

        private double _zenith;      //topocentric zenith angle [degrees]

        public double Zenith
        {
            get { return _zenith; }
            set { _zenith = value; }
        }

        private double _azimuth180;  //topocentric azimuth angle (westward from south) [-180 to 180 degrees]

        public double Azimuth180
        {
            get { return _azimuth180; }
            set { _azimuth180 = value; }
        }

        private double _azimuth;     //topocentric azimuth angle (eastward from north) [   0 to 360 degrees]

        public double Azimuth
        {
            get { return _azimuth; }
            set { _azimuth = value; }
        }

        private double _incidence;   //surface incidence angle [degrees]

        public double Incidence
        {
            get { return _incidence; }
            set { _incidence = value; }
        }

        private double _suntransit;  //local sun transit time (or solar noon) [fractional hour]

        public double Suntransit
        {
            get { return _suntransit; }
            set { _suntransit = value; }
        }

        private double _sunrise;     //local sunrise time (+/- 30 seconds) [fractional hour]

        public double Sunrise
        {
            get { return _sunrise; }
            set { _sunrise = value; }
        }

        private double _sunset;      //local sunset time (+/- 30 seconds) [fractional hour]

        public double Sunset
        {
            get { return _sunset; }
            set { _sunset = value; }
        }
    }
}