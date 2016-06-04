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
        SPA_ZA,           //calculate zenith and azimuth
        SPA_ZA_INC,       //calculate zenith, azimuth, and incidence
        SPA_ZA_RTS,       //calculate zenith, azimuth, and sun rise/transit/set values
        SPA_ALL,          //calculate all SPA output values
    };

    //Calculate SPA output values (in structure) based on input values passed in structure
    public struct Spa_Data
    {
        //----------------------INPUT VALUES------------------------

        private double year;            // 4-digit year,    valid range: -2000 to 6000, error code: 1

        public double Year
        {
            get { return year; }
            set { year = value; }
        }

        private int month;          // 2-digit month,         valid range: 1 to 12, error code: 2

        public int Month
        {
            get { return month; }
            set { month = value; }
        }

        private int day;             // 2-digit day,           valid range: 1 to 31, error code: 3

        public int Day
        {
            get { return day; }
            set { day = value; }
        }

        private int hour;            // Observer local hour,   valid range: 0 to 24, error code: 4

        public int Hour
        {
            get { return hour; }
            set { hour = value; }
        }

        private int minute;          // Observer local minute, valid range: 0 to 59, error code: 5

        public int Minute
        {
            get { return minute; }
            set { minute = value; }
        }

        private int second;          // Observer local second, valid range: 0 to 59, error code: 6

        public int Second
        {
            get { return second; }
            set { second = value; }
        }

        private double delta_ut1;    // Fractional second difference between UTC and UT which is used

        public double Delta_ut1
        {
            get { return delta_ut1; }
            set { delta_ut1 = value; }
        }

        // to adjust UTC for earth's irregular rotation rate and is derived
        // from observation only and is reported in this bulletin:
        // http://maia.usno.navy.mil/ser7/ser7.dat,
        // where delta_ut1 = DUT1
        // valid range: -1 to 1 second (exclusive), error code 17

        private double delta_t;      // Difference between earth rotation time and terrestrial time

        public double Delta_t
        {
            get { return delta_t; }
            set { delta_t = value; }
        }

        // It is derived from observation only and is reported in this
        // bulletin: http://maia.usno.navy.mil/ser7/ser7.dat,
        // where delta_t = 32.184 + (TAI-UTC) + DUT1
        // valid range: -8000 to 8000 seconds, error code: 7

        private double timezone;     // Observer time zone (negative west of Greenwich)

        public double Timezone
        {
            get { return timezone; }
            set { timezone = value; }
        }

        // valid range: -18   to   18 hours,   error code: 8

        private double longitude;    // Observer longitude (negative west of Greenwich)

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        // valid range: -180  to  180 degrees, error code: 9

        private double latitude;     // Observer latitude (negative south of equator)

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        // valid range: -90   to   90 degrees, error code: 10

        private double elevation;    // Observer elevation [meters]

        public double Elevation
        {
            get { return elevation; }
            set { elevation = value; }
        }

        // valid range: -6500000 or higher meters,    error code: 11

        private double pressure;     // Annual average local pressure [millibars]

        public double Pressure
        {
            get { return pressure; }
            set { pressure = value; }
        }

        // valid range:    0 to 5000 millibars,       error code: 12

        private double temperature;  // Annual average local temperature [degrees Celsius]

        public double Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }

        // valid range: -273 to 6000 degrees Celsius, error code; 13

        private double slope;        // Surface slope (measured from the horizontal plane)

        public double Slope
        {
            get { return slope; }
            set { slope = value; }
        }

        // valid range: -360 to 360 degrees, error code: 14

        private double azm_rotation; // Surface azimuth rotation (measured from south to projection of

        public double Azm_rotation
        {
            get { return azm_rotation; }
            set { azm_rotation = value; }
        }

        //     surface normal on horizontal plane, negative west)
        // valid range: -360 to 360 degrees, error code: 15

        private double atmos_refract;// Atmospheric refraction at sunrise and sunset (0.5667 deg is typical)

        public double Atmos_refract
        {
            get { return atmos_refract; }
            set { atmos_refract = value; }
        }

        // valid range: -5   to   5 degrees, error code: 16

        private int function;        // Switch to choose functions for desired output (from enumeration)

        public int Function
        {
            get { return function; }
            set { function = value; }
        }

        //-----------------Intermediate OUTPUT VALUES--------------------

        private double jd;          //Julian day

        public double Jd
        {
            get { return jd; }
            set { jd = value; }
        }

        private double jc;          //Julian century

        public double Jc
        {
            get { return jc; }
            set { jc = value; }
        }

        private double jde;         //Julian ephemeris day

        public double Jde
        {
            get { return jde; }
            set { jde = value; }
        }

        private double jce;         //Julian ephemeris century

        public double Jce
        {
            get { return jce; }
            set { jce = value; }
        }

        public double jme;         //Julian ephemeris millennium

        private double l;           //earth heliocentric longitude [degrees]

        public double L
        {
            get { return l; }
            set { l = value; }
        }

        private double b;           //earth heliocentric latitude [degrees]

        public double B
        {
            get { return b; }
            set { b = value; }
        }

        private double r;           //earth radius vector [Astronomical Units, AU]

        public double R
        {
            get { return r; }
            set { r = value; }
        }

        private double theta;       //geocentric longitude [degrees]

        public double Theta
        {
            get { return theta; }
            set { theta = value; }
        }

        private double beta;        //geocentric latitude [degrees]

        public double Beta
        {
            get { return beta; }
            set { beta = value; }
        }

        private double x0;          //mean elongation (moon-sun) [degrees]

        public double X0
        {
            get { return x0; }
            set { x0 = value; }
        }

        private double x1;          //mean anomaly (sun) [degrees]

        public double X1
        {
            get { return x1; }
            set { x1 = value; }
        }

        private double x2;          //mean anomaly (moon) [degrees]

        public double X2
        {
            get { return x2; }
            set { x2 = value; }
        }

        private double x3;          //argument latitude (moon) [degrees]

        public double X3
        {
            get { return x3; }
            set { x3 = value; }
        }

        private double x4;          //ascending longitude (moon) [degrees]

        public double X4
        {
            get { return x4; }
            set { x4 = value; }
        }

        public double del_psi;     //nutation longitude [degrees]
        public double del_epsilon; //nutation obliquity [degrees]
        public double epsilon0;    //ecliptic mean obliquity [arc seconds]
        public double epsilon;     //ecliptic true obliquity  [degrees]
        public double del_tau;     //aberration correction [degrees]
        public double lamda;       //apparent sun longitude [degrees]
        public double nu0;         //Greenwich mean sidereal time [degrees]
        public double nu;          //Greenwich sidereal time [degrees]
        public double alpha;       //geocentric sun right ascension [degrees]
        public double delta;       //geocentric sun declination [degrees]
        public double h;           //observer hour angle [degrees]
        public double xi;          //sun equatorial horizontal parallax [degrees]
        public double del_alpha;   //sun right ascension parallax [degrees]
        public double delta_prime; //topocentric sun declination [degrees]
        public double alpha_prime; //topocentric sun right ascension [degrees]
        public double h_prime;     //topocentric local hour angle [degrees]
        public double e0;          //topocentric elevation angle (uncorrected) [degrees]
        public double del_e;       //atmospheric refraction correction [degrees]
        public double e;           //topocentric elevation angle (corrected) [degrees]
        public double eot;         //equation of time [minutes]
        public double srha;        //sunrise hour angle [degrees]
        public double ssha;        //sunset hour angle [degrees]
        public double sta;         //sun transit altitude [degrees]

        //---------------------Final OUTPUT VALUES------------------------

        private double zenith;      //topocentric zenith angle [degrees]

        public double Zenith
        {
            get { return zenith; }
            set { zenith = value; }
        }

        private double azimuth180;  //topocentric azimuth angle (westward from south) [-180 to 180 degrees]

        public double Azimuth180
        {
            get { return azimuth180; }
            set { azimuth180 = value; }
        }

        private double azimuth;     //topocentric azimuth angle (eastward from north) [   0 to 360 degrees]

        public double Azimuth
        {
            get { return azimuth; }
            set { azimuth = value; }
        }

        private double incidence;   //surface incidence angle [degrees]

        public double Incidence
        {
            get { return incidence; }
            set { incidence = value; }
        }

        private double suntransit;  //local sun transit time (or solar noon) [fractional hour]

        public double Suntransit
        {
            get { return suntransit; }
            set { suntransit = value; }
        }

        private double sunrise;     //local sunrise time (+/- 30 seconds) [fractional hour]

        public double Sunrise
        {
            get { return sunrise; }
            set { sunrise = value; }
        }

        private double sunset;      //local sunset time (+/- 30 seconds) [fractional hour]

        public double Sunset
        {
            get { return sunset; }
            set { sunset = value; }
        }
    }


}