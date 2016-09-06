#region License

/////////////////////////////////////////////
//      Solar Position Algorithm (SPA)     //
//                   for                   //
//        Solar Radiation Application      //
//                                         //
//               May 12, 2003              //
//                                         //
//   Filename: SPA.C                       //
//                                         //
//   Afshin Michael Andreas                //
//   Afshin.Andreas@NREL.gov (303)384-6383 //
//                                         //
//   Measurement & Instrumentation Team    //
//   Solar Radiation Research Laboratory   //
//   National Renewable Energy Laboratory  //
//   1617 Cole Blvd, Golden, CO 80401      //
/////////////////////////////////////////////

/////////////////////////////////////////////
//   See the SPA.H header file for usage   //
//                                         //
//   This code is based on the NREL        //
//   technical report "Solar Position      //
//   Algorithm for Solar Radiation         //
//   Application" by I. Reda & A. Andreas  //
/////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////////////////////
//
//   NOTICE
//   Copyright (C) 2008-2011 Alliance for Sustainable Energy, LLC, All Rights Reserved
//
//The Solar Position Algorithm ("Software") is code in development prepared by employees of the
//Alliance for Sustainable Energy, LLC, (hereinafter the "Contractor"), under Contract No.
//DE-AC36-08GO28308 ("Contract") with the U.S. Department of Energy (the "DOE"). The United
//States Government has been granted for itself and others acting on its behalf a paid-up, non-
//exclusive, irrevocable, worldwide license in the Software to reproduce, prepare derivative
//works, and perform publicly and display publicly. Beginning five (5) years after the date
//permission to assert copyright is obtained from the DOE, and subject to any subsequent five
//(5) year renewals, the United States Government is granted for itself and others acting on
//its behalf a paid-up, non-exclusive, irrevocable, worldwide license in the Software to
//reproduce, prepare derivative works, distribute copies to the public, perform publicly and
//display publicly, and to permit others to do so. If the Contractor ceases to make this
//computer software available, it may be obtained from DOE's Office of Scientific and Technical
//Information's Energy Science and Technology Software Center (ESTSC) at P.O. Box 1020, Oak
//Ridge, TN 37831-1020. THIS SOFTWARE IS PROVIDED BY THE CONTRACTOR "AS IS" AND ANY EXPRESS OR
//IMPLIED WARRANTIES, INCLUDING BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
//AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE CONTRACTOR OR THE
//U.S. GOVERNMENT BE LIABLE FOR ANY SPECIAL, INDIRECT OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
//WHATSOEVER, INCLUDING BUT NOT LIMITED TO CLAIMS ASSOCIATED WITH THE LOSS OF DATA OR PROFITS,
//WHICH MAY RESULT FROM AN ACTION IN CONTRACT, NEGLIGENCE OR OTHER TORTIOUS CLAIM THAT ARISES
//OUT OF OR IN CONNECTION WITH THE ACCESS, USE OR PERFORMANCE OF THIS SOFTWARE.
//
//The Software is being provided for internal, noncommercial purposes only and shall not be
//re-distributed. Please contact Anne Miller (Anne.Miller@nrel.gov) in the NREL
//Commercialization and Technology Transfer Office for information concerning a commercial
//license to use the Software.
//
//As a condition of using the Software in an application, the developer of the application
//agrees to reference the use of the Software and make this Notice readily accessible to any
//end-user in a Help|About screen or equivalent manner.
//
///////////////////////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////////////////////
// Revised 27-FEB-2004 Andreas
//         Added bounds check on inputs and return value for spa_calculate().
// Revised 10-MAY-2004 Andreas
//         Changed temperature bound check minimum from -273.15 to -273 degrees C.
// Revised 17-JUN-2004 Andreas
//         Corrected a problem that caused a bogus sunrise/set/transit on the equinox.
// Revised 18-JUN-2004 Andreas
//         Added a "function" input variable that allows the selecting of desired outputs.
// Revised 21-JUN-2004 Andreas
//         Added 3 new intermediate output values to SPA structure (srha, ssha, & sta).
// Revised 23-JUN-2004 Andreas
//         Enumerations for "function" were renamed and 2 were added.
//         Prevented bound checks on inputs that are not used (based on function).
// Revised 01-SEP-2004 Andreas
//         Changed a local variable from integer to double.
// Revised 12-JUL-2005 Andreas
//         Put a limit on the EOT calculation, so that the result is between -20 and 20.
// Revised 26-OCT-2005 Andreas
//         Set the atmos. refraction correction to zero, when sun is below horizon.
//         Made atmos_refract input a requirement for all "functions".
//         Changed atmos_refract bound check from +/- 10 to +/- 5 degrees.
// Revised 07-NOV-2006 Andreas
//         Corrected 3 earth periodic terms in the L_TERMS array.
//         Corrected 2 earth periodic terms in the R_TERMS array.
// Revised 10-NOV-2006 Andreas
//         Corrected a constant used to calculate topocentric sun declination.
//         Put a limit on observer hour angle, so result is between 0 and 360.
// Revised 13-NOV-2006 Andreas
//         Corrected calculation of topocentric sun declination.
//         Converted all floating point inputs in spa structure to doubles.
// Revised 27-FEB-2007 Andreas
//         Minor correction made as to when atmos. refraction correction is set to zero.
// Revised 21-JAN-2008 Andreas
//         Minor change to two variable declarations.
// Revised 12-JAN-2009 Andreas
//         Changed timezone bound check from +/-12 to +/-18 hours.
// Revised 14-JAN-2009 Andreas
//         Corrected a constant used to calculate ecliptic mean obliquity.
///////////////////////////////////////////////////////////////////////////////////////////////

#endregion License

using System;

namespace SPA
{
    public class Spa
    {
        #region Fields

        public SpaData spa { get; set; }

        public const double SunRadius = 0.26667;

        public const int LCount = 6;
        public const int BCount = 2;
        public const int RCount = 5;
        public const int YCount = 63;

        public const int LMaxSubcount = 64;
        public const int BMaxSubcount = 5;
        public const int RMaxSubcount = 40;
        public const double Pi = 3.14159265359;

        public enum Term : int { TermA, TermB, TermC, TermCount };

        public enum TermX : int { TermX0, TermX1, TermX2, TermX3, TermX4, TermXCount };

        public enum Met : int { TermPsiA, TermPsiB, TermEpsC, TermEpsD, TermPeCount };

        public enum Julian : int { JdMinus, JdZero, JdPlus, JdCount };

        public enum SunTerm : int { SunTransit, SunRise, SunSet, SunCount };

        public enum SpaSelect : int
        {
            SpaZa,           //calculate zenith and azimuth
            SpaZaInc,       //calculate zenith, azimuth, and incidence
            SpaZaRts,       //calculate zenith, azimuth, and sun rise/transit/set values
            SpaAll,          //calculate all SPA output values
        };

        public const int TermYCount = (int)TermX.TermXCount;

        private int[] _lSubcount = new int[LCount] { 64, 34, 20, 7, 3, 1 };
        private int[] _bSubcount = new int[BCount] { 5, 2 };
        private int[] _rSubcount = new int[RCount] { 40, 10, 6, 2, 1 };

        #endregion Fields

        #region Constructor

        public Spa()
        {
            spa = new SpaData();
        }

        #endregion Constructor

        ///////////////////////////////////////////////////
        ///  Earth Periodic Terms
        ///////////////////////////////////////////////////

        #region Earth Periodic Terms

        public double[][,] LTerms = new double[LCount][,]
{
   new double[,]{
        {175347046.0,0,0},
        {3341656.0,4.6692568,6283.07585},
        {34894.0,4.6261,12566.1517},
        {3497.0,2.7441,5753.3849},
        {3418.0,2.8289,3.5231},
        {3136.0,3.6277,77713.7715},
        {2676.0,4.4181,7860.4194},
        {2343.0,6.1352,3930.2097},
        {1324.0,0.7425,11506.7698},
        {1273.0,2.0371,529.691},
        {1199.0,1.1096,1577.3435},
        {990,5.233,5884.927},
        {902,2.045,26.298},
        {857,3.508,398.149},
        {780,1.179,5223.694},
        {753,2.533,5507.553},
        {505,4.583,18849.228},
        {492,4.205,775.523},
        {357,2.92,0.067},
        {317,5.849,11790.629},
        {284,1.899,796.298},
        {271,0.315,10977.079},
        {243,0.345,5486.778},
        {206,4.806,2544.314},
        {205,1.869,5573.143},
        {202,2.458,6069.777},
        {156,0.833,213.299},
        {132,3.411,2942.463},
        {126,1.083,20.775},
        {115,0.645,0.98},
        {103,0.636,4694.003},
        {102,0.976,15720.839},
        {102,4.267,7.114},
        {99,6.21,2146.17},
        {98,0.68,155.42},
        {86,5.98,161000.69},
        {85,1.3,6275.96},
        {85,3.67,71430.7},
        {80,1.81,17260.15},
        {79,3.04,12036.46},
        {75,1.76,5088.63},
        {74,3.5,3154.69},
        {74,4.68,801.82},
        {70,0.83,9437.76},
        {62,3.98,8827.39},
        {61,1.82,7084.9},
        {57,2.78,6286.6},
        {56,4.39,14143.5},
        {56,3.47,6279.55},
        {52,0.19,12139.55},
        {52,1.33,1748.02},
        {51,0.28,5856.48},
        {49,0.49,1194.45},
        {41,5.37,8429.24},
        {41,2.4,19651.05},
        {39,6.17,10447.39},
        {37,6.04,10213.29},
        {37,2.57,1059.38},
        {36,1.71,2352.87},
        {36,1.78,6812.77},
        {33,0.59,17789.85},
        {30,0.44,83996.85},
        {30,2.74,1349.87},
        {25,3.16,4690.48}
    },
    new double[,]
    {
        {628331966747.0,0,0},
        {206059.0,2.678235,6283.07585},
        {4303.0,2.6351,12566.1517},
        {425.0,1.59,3.523},
        {119.0,5.796,26.298},
        {109.0,2.966,1577.344},
        {93,2.59,18849.23},
        {72,1.14,529.69},
        {68,1.87,398.15},
        {67,4.41,5507.55},
        {59,2.89,5223.69},
        {56,2.17,155.42},
        {45,0.4,796.3},
        {36,0.47,775.52},
        {29,2.65,7.11},
        {21,5.34,0.98},
        {19,1.85,5486.78},
        {19,4.97,213.3},
        {17,2.99,6275.96},
        {16,0.03,2544.31},
        {16,1.43,2146.17},
        {15,1.21,10977.08},
        {12,2.83,1748.02},
        {12,3.26,5088.63},
        {12,5.27,1194.45},
        {12,2.08,4694},
        {11,0.77,553.57},
        {10,1.3,6286.6},
        {10,4.24,1349.87},
        {9,2.7,242.73},
        {9,5.64,951.72},
        {8,5.3,2352.87},
        {6,2.65,9437.76},
        {6,4.67,4690.48}
    },
    new double[,]
    {
        {52919.0,0,0},
        {8720.0,1.0721,6283.0758},
        {309.0,0.867,12566.152},
        {27,0.05,3.52},
        {16,5.19,26.3},
        {16,3.68,155.42},
        {10,0.76,18849.23},
        {9,2.06,77713.77},
        {7,0.83,775.52},
        {5,4.66,1577.34},
        {4,1.03,7.11},
        {4,3.44,5573.14},
        {3,5.14,796.3},
        {3,6.05,5507.55},
        {3,1.19,242.73},
        {3,6.12,529.69},
        {3,0.31,398.15},
        {3,2.28,553.57},
        {2,4.38,5223.69},
        {2,3.75,0.98}
    },
    new double[,]
    {
        {289.0,5.844,6283.076},
        {35,0,0},
        {17,5.49,12566.15},
        {3,5.2,155.42},
        {1,4.72,3.52},
        {1,5.3,18849.23},
        {1,5.97,242.73}
    },
    new double[,]
    {
        {114.0,3.142,0},
        {8,4.13,6283.08},
        {1,3.84,12566.15}
    },
    new double[,]
    {
        {1,3.14,0}
    }
};

        public double[][,] BTerms = new double[BCount][,]
{
    new double[,]{
        {280.0,3.199,84334.662},
        {102.0,5.422,5507.553},
        {80,3.88,5223.69},
        {44,3.7,2352.87},
        {32,4,1577.34}
    },
    new double[,]
    {
        {9,3.9,5507.55},
        {6,1.73,5223.69}
    }
};

        public double[][,] RTerms = new double[RCount][,]
{
    new double[,]
    {
        {100013989.0,0,0},
        {1670700.0,3.0984635,6283.07585},
        {13956.0,3.05525,12566.1517},
        {3084.0,5.1985,77713.7715},
        {1628.0,1.1739,5753.3849},
        {1576.0,2.8469,7860.4194},
        {925.0,5.453,11506.77},
        {542.0,4.564,3930.21},
        {472.0,3.661,5884.927},
        {346.0,0.964,5507.553},
        {329.0,5.9,5223.694},
        {307.0,0.299,5573.143},
        {243.0,4.273,11790.629},
        {212.0,5.847,1577.344},
        {186.0,5.022,10977.079},
        {175.0,3.012,18849.228},
        {110.0,5.055,5486.778},
        {98,0.89,6069.78},
        {86,5.69,15720.84},
        {86,1.27,161000.69},
        {65,0.27,17260.15},
        {63,0.92,529.69},
        {57,2.01,83996.85},
        {56,5.24,71430.7},
        {49,3.25,2544.31},
        {47,2.58,775.52},
        {45,5.54,9437.76},
        {43,6.01,6275.96},
        {39,5.36,4694},
        {38,2.39,8827.39},
        {37,0.83,19651.05},
        {37,4.9,12139.55},
        {36,1.67,12036.46},
        {35,1.84,2942.46},
        {33,0.24,7084.9},
        {32,0.18,5088.63},
        {32,1.78,398.15},
        {28,1.21,6286.6},
        {28,1.9,6279.55},
        {26,4.59,10447.39}
    },
    new double[,]
    {
        {103019.0,1.10749,6283.07585},
        {1721.0,1.0644,12566.1517},
        {702.0,3.142,0},
        {32,1.02,18849.23},
        {31,2.84,5507.55},
        {25,1.32,5223.69},
        {18,1.42,1577.34},
        {10,5.91,10977.08},
        {9,1.42,6275.96},
        {9,0.27,5486.78}
    },
    new double[,]
    {
        {4359.0,5.7846,6283.0758},
        {124.0,5.579,12566.152},
        {12,3.14,0},
        {9,3.63,77713.77},
        {6,1.87,5573.14},
        {3,5.47,18849.23}
    },
    new double[,]
    {
        {145.0,4.273,6283.076},
        {7,3.92,12566.15}
    },
    new double[,]
    {
        {4,2.56,6283.08}
    }
};

        #endregion Earth Periodic Terms

        ////////////////////////////////////////////////////////////////
        ///  Periodic Terms for the nutation in longitude and obliquity
        ////////////////////////////////////////////////////////////////

        #region Periodic Terms for the nutation in longitude and obliquity

        public int[,] YTerms = new int[YCount, TermYCount]
{
    {0,0,0,0,1},
    {-2,0,0,2,2},
    {0,0,0,2,2},
    {0,0,0,0,2},
    {0,1,0,0,0},
    {0,0,1,0,0},
    {-2,1,0,2,2},
    {0,0,0,2,1},
    {0,0,1,2,2},
    {-2,-1,0,2,2},
    {-2,0,1,0,0},
    {-2,0,0,2,1},
    {0,0,-1,2,2},
    {2,0,0,0,0},
    {0,0,1,0,1},
    {2,0,-1,2,2},
    {0,0,-1,0,1},
    {0,0,1,2,1},
    {-2,0,2,0,0},
    {0,0,-2,2,1},
    {2,0,0,2,2},
    {0,0,2,2,2},
    {0,0,2,0,0},
    {-2,0,1,2,2},
    {0,0,0,2,0},
    {-2,0,0,2,0},
    {0,0,-1,2,1},
    {0,2,0,0,0},
    {2,0,-1,0,1},
    {-2,2,0,2,2},
    {0,1,0,0,1},
    {-2,0,1,0,1},
    {0,-1,0,0,1},
    {0,0,2,-2,0},
    {2,0,-1,2,1},
    {2,0,1,2,2},
    {0,1,0,2,2},
    {-2,1,1,0,0},
    {0,-1,0,2,2},
    {2,0,0,2,1},
    {2,0,1,0,0},
    {-2,0,2,2,2},
    {-2,0,1,2,1},
    {2,0,-2,0,1},
    {2,0,0,0,1},
    {0,-1,1,0,0},
    {-2,-1,0,2,1},
    {-2,0,0,0,1},
    {0,0,2,2,1},
    {-2,0,2,0,1},
    {-2,1,0,2,1},
    {0,0,1,-2,0},
    {-1,0,1,0,0},
    {-2,1,0,0,0},
    {1,0,0,0,0},
    {0,0,1,2,0},
    {0,0,-2,2,2},
    {-1,-1,1,0,0},
    {0,1,1,0,0},
    {0,-1,1,2,2},
    {2,-1,-1,2,2},
    {0,0,3,2,2},
    {2,-1,0,2,2},
};

        private double[,] _peTerms = new double[YCount, (int)Met.TermPeCount]
{
    {-171996,-174.2,92025,8.9},
    {-13187,-1.6,5736,-3.1},
    {-2274,-0.2,977,-0.5},
    {2062,0.2,-895,0.5},
    {1426,-3.4,54,-0.1},
    {712,0.1,-7,0},
    {-517,1.2,224,-0.6},
    {-386,-0.4,200,0},
    {-301,0,129,-0.1},
    {217,-0.5,-95,0.3},
    {-158,0,0,0},
    {129,0.1,-70,0},
    {123,0,-53,0},
    {63,0,0,0},
    {63,0.1,-33,0},
    {-59,0,26,0},
    {-58,-0.1,32,0},
    {-51,0,27,0},
    {48,0,0,0},
    {46,0,-24,0},
    {-38,0,16,0},
    {-31,0,13,0},
    {29,0,0,0},
    {29,0,-12,0},
    {26,0,0,0},
    {-22,0,0,0},
    {21,0,-10,0},
    {17,-0.1,0,0},
    {16,0,-8,0},
    {-16,0.1,7,0},
    {-15,0,9,0},
    {-13,0,7,0},
    {-12,0,6,0},
    {11,0,0,0},
    {-10,0,5,0},
    {-8,0,3,0},
    {7,0,-3,0},
    {-7,0,0,0},
    {-7,0,3,0},
    {-7,0,3,0},
    {6,0,0,0},
    {6,0,-3,0},
    {6,0,-3,0},
    {-6,0,3,0},
    {-6,0,3,0},
    {5,0,0,0},
    {-5,0,3,0},
    {-5,0,3,0},
    {-5,0,3,0},
    {4,0,0,0},
    {4,0,0,0},
    {4,0,0,0},
    {-4,0,0,0},
    {-4,0,0,0},
    {-4,0,0,0},
    {3,0,0,0},
    {-3,0,0,0},
    {-3,0,0,0},
    {-3,0,0,0},
    {-3,0,0,0},
    {-3,0,0,0},
    {-3,0,0,0},
    {-3,0,0,0},
};

        ///////////////////////////////////////////////

        #endregion Periodic Terms for the nutation in longitude and obliquity

        #region Methods

        private double Rad2Deg(double radians)
        {
            return (180.0 / Pi) * radians;
        }

        private double Deg2Rad(double degrees)
        {
            return (Pi / 180.0) * degrees;
        }

        private double limit_degrees(double degrees)
        {
            double limited;

            degrees /= 360.0;
            limited = 360.0 * (degrees - Math.Floor(degrees));
            if (limited < 0) limited += 360.0;

            return limited;
        }

        private double limit_degrees180pm(double degrees)
        {
            double limited;

            degrees /= 360.0;
            limited = 360.0 * (degrees - Math.Floor(degrees));
            if (limited < -180.0) limited += 360.0;
            else if (limited > 180.0) limited -= 360.0;

            return limited;
        }

        private double limit_degrees180(double degrees)
        {
            double limited;

            degrees /= 180.0;
            limited = 180.0 * (degrees - Math.Floor(degrees));
            if (limited < 0) limited += 180.0;

            return limited;
        }

        private double limit_zero2one(double value)
        {
            double limited;

            limited = value - Math.Floor(value);
            if (limited < 0) limited += 1.0;

            return limited;
        }

        private double limit_minutes(double minutes)
        {
            double limited = minutes;

            if (limited < -20.0) limited += 1440.0;
            else if (limited > 20.0) limited -= 1440.0;

            return limited;
        }

        private double dayfrac_to_local_hr(double dayfrac, double timezone)
        {
            return 24.0 * limit_zero2one(dayfrac + timezone / 24.0);
        }

        private double third_order_polynomial(double a, double b, double c, double d, double x)
        {
            return ((a * x + b) * x + c) * x + d;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////
        public int validate_inputs(ref SpaData spa)
        {
            if ((spa.Year < -2000) || (spa.Year > 6000)) return 1;
            if ((spa.Month < 1) || (spa.Month > 12)) return 2;
            if ((spa.Day < 1) || (spa.Day > 31)) return 3;
            if ((spa.Hour < 0) || (spa.Hour > 24)) return 4;
            if ((spa.Minute < 0) || (spa.Minute > 59)) return 5;
            if ((spa.Second < 0) || (spa.Second > 59)) return 6;
            if ((spa.Pressure < 0) || (spa.Pressure > 5000)) return 12;
            if ((spa.Temperature <= -273) || (spa.Temperature > 6000)) return 13;
            if ((spa.Hour == 24) && (spa.Minute > 0)) return 5;
            if ((spa.Hour == 24) && (spa.Second > 0)) return 6;

            if (Math.Abs(spa.DeltaT) > 8000) return 7;
            if (Math.Abs(spa.Timezone) > 18) return 8;
            if (Math.Abs(spa.Longitude) > 180) return 9;
            if (Math.Abs(spa.Latitude) > 90) return 10;
            if (Math.Abs(spa.AtmosRefract) > 5) return 16;
            if (spa.Elevation < -6500000) return 11;

            if ((spa.Function == (int)SpaSelect.SpaZaInc) || (spa.Function == (int)SpaSelect.SpaAll))
            {
                if (Math.Abs(spa.Slope) > 360) return 14;
                if (Math.Abs(spa.AzmRotation) > 360) return 15;
            }

            return 0;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////
        private double julian_day(double year, int month, int day, int hour, int minute, int second, double tz)
        {
            double dayDecimal, julianDay, a;

            dayDecimal = day + (hour - tz + (minute + second / 60.0) / 60.0) / 24.0;

            if (month < 3)
            {
                month += 12;
                year--;
            }

            julianDay = Math.Floor(365.25 * (year + 4716.0)) + Math.Floor(30.6001 * (month + 1)) + dayDecimal - 1524.5;

            if (julianDay > 2299160.0)
            {
                a = Math.Floor(year / 100);
                julianDay += (2 - a + Math.Floor(a / 4));
            }

            return julianDay;
        }

        private double julian_century(double jd)
        {
            return (jd - 2451545.0) / 36525.0;
        }

        private double julian_ephemeris_day(double jd, double deltaT)
        {
            return jd + deltaT / 86400.0;
        }

        private double julian_ephemeris_century(double jde)
        {
            return (jde - 2451545.0) / 36525.0;
        }

        private double julian_ephemeris_millennium(double jce)
        {
            return (jce / 10.0);
        }

        public double earth_periodic_term_summation(double[,] terms, int count, double jme)
        {
            double sum = 0;

            for (int k = 0; k < count; k++)
            {
                sum += terms[k, (int)Term.TermA] * Math.Cos(terms[k, (int)Term.TermB] + terms[k, (int)Term.TermC] * jme);
            }

            return sum;
        }

        public double earth_values(double[] termSum, int count, double jme)
        {
            int i;
            double sum = 0;

            for (i = 0; i < count; i++)
            {
                sum += termSum[i] * Math.Pow(jme, i);
            }

            sum /= 1.0e8;

            return sum;
        }

        public double earth_heliocentric_longitude(double jme)
        {
            double[] sum = new double[LCount];
            int i;

            for (i = 0; i < LCount; i++)
            {
                sum[i] = earth_periodic_term_summation(LTerms[i], _lSubcount[i], jme);
            }

            return limit_degrees(Rad2Deg(earth_values(sum, LCount, jme)));
        }

        private double earth_heliocentric_latitude(double jme)
        {
            double[] sum = new double[BCount];
            int i;

            for (i = 0; i < BCount; i++)
            {
                sum[i] = earth_periodic_term_summation(BTerms[i], _bSubcount[i], jme);
            }

            return Rad2Deg(earth_values(sum, BCount, jme));
        }

        private double earth_radius_vector(double jme)
        {
            double[] sum = new double[RCount];
            int i;

            for (i = 0; i < RCount; i++)
            {
                sum[i] = earth_periodic_term_summation(RTerms[i], _rSubcount[i], jme);
            }

            return earth_values(sum, RCount, jme);
        }

        private double geocentric_longitude(double l)
        {
            double theta = l + 180.0;

            if (theta >= 360.0) theta -= 360.0;

            return theta;
        }

        private double geocentric_latitude(double b)
        {
            return -b;
        }

        private double mean_elongation_moon_sun(double jce)
        {
            return third_order_polynomial(1.0 / 189474.0, -0.0019142, 445267.11148, 297.85036, jce);
        }

        private double mean_anomaly_sun(double jce)
        {
            return third_order_polynomial(-1.0 / 300000.0, -0.0001603, 35999.05034, 357.52772, jce);
        }

        private double mean_anomaly_moon(double jce)
        {
            return third_order_polynomial(1.0 / 56250.0, 0.0086972, 477198.867398, 134.96298, jce);
        }

        private double argument_latitude_moon(double jce)
        {
            return third_order_polynomial(1.0 / 327270.0, -0.0036825, 483202.017538, 93.27191, jce);
        }

        private double ascending_longitude_moon(double jce)
        {
            return third_order_polynomial(1.0 / 450000.0, 0.0020708, -1934.136261, 125.04452, jce);
        }

        private double xy_term_summation(int i, double[] x)
        {
            int j;
            double sum = 0;

            for (j = 0; j < TermYCount; j++)
            {
                sum += x[j] * YTerms[i, j];
            }

            return sum;
        }

        private void nutation_longitude_and_obliquity(double jce, double[] x, ref double delPsi, ref double delEpsilon)
        {
            int i;
            double xyTermSum, sumPsi = 0, sumEpsilon = 0;

            for (i = 0; i < YCount; i++)
            {
                xyTermSum = Deg2Rad(xy_term_summation(i, x));
                sumPsi += (_peTerms[i, (int)Met.TermPsiA] + jce * _peTerms[i, (int)Met.TermPsiB]) * Math.Sin(xyTermSum);
                sumEpsilon += (_peTerms[i, (int)Met.TermEpsC] + jce * _peTerms[i, (int)Met.TermEpsD]) * Math.Cos(xyTermSum);
            }

            delPsi = sumPsi / 36000000.0;
            delEpsilon = sumEpsilon / 36000000.0;
        }

        private double ecliptic_mean_obliquity(double jme)
        {
            double u = jme / 10.0;

            return 84381.448 + u * (-4680.93 + u * (-1.55 + u * (1999.25 + u * (-51.38 + u * (-249.67 +
                               u * (-39.05 + u * (7.12 + u * (27.87 + u * (5.79 + u * 2.45)))))))));
        }

        private double ecliptic_true_obliquity(double deltaEpsilon, double epsilon0)
        {
            return deltaEpsilon + epsilon0 / 3600.0;
        }

        private double aberration_correction(double r)
        {
            return -20.4898 / (3600.0 * r);
        }

        private double apparent_sun_longitude(double theta, double deltaPsi, double deltaTau)
        {
            return theta + deltaPsi + deltaTau;
        }

        private double greenwich_mean_sidereal_time(double jd, double jc)
        {
            return limit_degrees(280.46061837 + 360.98564736629 * (jd - 2451545.0) +
                                               jc * jc * (0.000387933 - jc / 38710000.0));
        }

        private double greenwich_sidereal_time(double nu0, double deltaPsi, double epsilon)
        {
            return nu0 + deltaPsi * Math.Cos(Deg2Rad(epsilon));
        }

        private double geocentric_sun_right_ascension(double lamda, double epsilon, double beta)
        {
            double lamdaRad = Deg2Rad(lamda);
            double epsilonRad = Deg2Rad(epsilon);

            return limit_degrees(Rad2Deg(Math.Atan2(Math.Sin(lamdaRad) * Math.Cos(epsilonRad) -
                                               Math.Tan(Deg2Rad(beta)) * Math.Sin(epsilonRad), Math.Cos(lamdaRad))));
        }

        private double geocentric_sun_declination(double beta, double epsilon, double lamda)
        {
            double betaRad = Deg2Rad(beta);
            double epsilonRad = Deg2Rad(epsilon);

            return Rad2Deg(Math.Asin(Math.Sin(betaRad) * Math.Cos(epsilonRad) +
                                Math.Cos(betaRad) * Math.Sin(epsilonRad) * Math.Sin(Deg2Rad(lamda))));
        }

        private double observer_hour_angle(double nu, double longitude, double alphaDeg)
        {
            return limit_degrees(nu + longitude - alphaDeg);
        }

        private double sun_equatorial_horizontal_parallax(double r)
        {
            return 8.794 / (3600.0 * r);
        }

        public void sun_right_ascension_parallax_and_topocentric_dec(double latitude, double elevation,
               double xi, double h, double delta, ref double deltaAlpha, ref double deltaPrime)
        {
            double deltaAlphaRad;
            double latRad = Deg2Rad(latitude);
            double xiRad = Deg2Rad(xi);
            double hRad = Deg2Rad(h);
            double deltaRad = Deg2Rad(delta);
            double u = Math.Atan(0.99664719 * Math.Tan(latRad));
            double y = 0.99664719 * Math.Sin(u) + elevation * Math.Sin(latRad) / 6378140.0;
            double x = Math.Cos(u) + elevation * Math.Cos(latRad) / 6378140.0;

            deltaAlphaRad = Math.Atan2(-x * Math.Sin(xiRad) * Math.Sin(hRad),
                                          Math.Cos(deltaRad) - x * Math.Sin(xiRad) * Math.Cos(hRad));

            deltaPrime = Rad2Deg(Math.Atan2((Math.Sin(deltaRad) - y * Math.Sin(xiRad)) * Math.Cos(deltaAlphaRad),
                                          Math.Cos(deltaRad) - x * Math.Sin(xiRad) * Math.Cos(hRad)));

            deltaAlpha = Rad2Deg(deltaAlphaRad);
        }

        public double topocentric_sun_right_ascension(double alphaDeg, double deltaAlpha)
        {
            return alphaDeg + deltaAlpha;
        }

        public double topocentric_local_hour_angle(double h, double deltaAlpha)
        {
            return h - deltaAlpha;
        }

        private double topocentric_elevation_angle(double latitude, double deltaPrime, double hPrime)
        {
            double latRad = Deg2Rad(latitude);
            double deltaPrimeRad = Deg2Rad(deltaPrime);

            return Rad2Deg(Math.Asin(Math.Sin(latRad) * Math.Sin(deltaPrimeRad) +
                                Math.Cos(latRad) * Math.Cos(deltaPrimeRad) * Math.Cos(Deg2Rad(hPrime))));
        }

        private double atmospheric_refraction_correction(double pressure, double temperature,
                                                 double atmosRefract, double e0)
        {
            double delE = 0;

            if (e0 >= -1 * (SunRadius + atmosRefract))
                delE = (pressure / 1010.0) * (283.0 / (273.0 + temperature)) *
                         1.02 / (60.0 * Math.Tan(Deg2Rad(e0 + 10.3 / (e0 + 5.11))));

            return delE;
        }

        private double topocentric_elevation_angle_corrected(double e0, double deltaE)
        {
            return e0 + deltaE;
        }

        private double topocentric_zenith_angle(double e)
        {
            return 90.0 - e;
        }

        private double topocentric_azimuth_angle_neg180_180(double hPrime, double latitude, double deltaPrime)
        {
            double hPrimeRad = Deg2Rad(hPrime);
            double latRad = Deg2Rad(latitude);

            return Rad2Deg(Math.Atan2(Math.Sin(hPrimeRad),
                                 Math.Cos(hPrimeRad) * Math.Sin(latRad) - Math.Tan(Deg2Rad(deltaPrime)) * Math.Cos(latRad)));
        }

        private double topocentric_azimuth_angle_zero_360(double azimuth180)
        {
            return azimuth180 + 180.0;
        }

        private double surface_incidence_angle(double zenith, double azimuth180, double azmRotation,
                                                                         double slope)
        {
            double zenithRad = Deg2Rad(zenith);
            double slopeRad = Deg2Rad(slope);

            return Rad2Deg(Math.Acos(Math.Cos(zenithRad) * Math.Cos(slopeRad) +
                                Math.Sin(slopeRad) * Math.Sin(zenithRad) * Math.Cos(Deg2Rad(azimuth180 - azmRotation))));
        }

        private double sun_mean_longitude(double jme)
        {
            return limit_degrees(280.4664567 + jme * (360007.6982779 + jme * (0.03032028 +
                            jme * (1 / 49931.0 + jme * (-1 / 15300.0 + jme * (-1 / 2000000.0))))));
        }

        private double Eot(double m, double alpha, double delPsi, double epsilon)
        {
            return limit_minutes(4.0 * (m - 0.0057183 - alpha + delPsi * Math.Cos(Deg2Rad(epsilon))));
        }

        private double approx_sun_transit_time(double alphaZero, double longitude, double nu)
        {
            return (alphaZero - longitude - nu) / 360.0;
        }

        private double sun_hour_angle_at_rise_set(double latitude, double deltaZero, double h0Prime)
        {
            double h0 = -99999;
            double latitudeRad = Deg2Rad(latitude);
            double deltaZeroRad = Deg2Rad(deltaZero);
            double argument = (Math.Sin(Deg2Rad(h0Prime)) - Math.Sin(latitudeRad) * Math.Sin(deltaZeroRad)) /
                                                             (Math.Cos(latitudeRad) * Math.Cos(deltaZeroRad));

            if (Math.Abs(argument) <= 1) h0 = limit_degrees180(Rad2Deg(Math.Acos(argument)));

            return h0;
        }

        private void approx_sun_rise_and_set(ref double[] mRts, double h0)
        {
            double h0Dfrac = h0 / 360.0;

            mRts[(int)SunTerm.SunRise] = limit_zero2one(mRts[(int)SunTerm.SunTransit] - h0Dfrac);
            mRts[(int)SunTerm.SunSet] = limit_zero2one(mRts[(int)SunTerm.SunTransit] + h0Dfrac);
            mRts[(int)SunTerm.SunTransit] = limit_zero2one(mRts[(int)SunTerm.SunTransit]);
        }

        private double rts_alpha_delta_prime(ref double[] ad, double n)
        {
            double a = ad[(int)Julian.JdZero] - ad[(int)Julian.JdMinus];
            double b = ad[(int)Julian.JdPlus] - ad[(int)Julian.JdZero];

            if (Math.Abs(a) >= 2.0) a = limit_zero2one(a);
            if (Math.Abs(b) >= 2.0) b = limit_zero2one(b);

            return ad[(int)Julian.JdZero] + n * (a + b + (b - a) * n) / 2.0;
        }

        private double rts_sun_altitude(double latitude, double deltaPrime, double hPrime)
        {
            double latitudeRad = Deg2Rad(latitude);
            double deltaPrimeRad = Deg2Rad(deltaPrime);

            return Rad2Deg(Math.Asin(Math.Sin(latitudeRad) * Math.Sin(deltaPrimeRad) +
                                Math.Cos(latitudeRad) * Math.Cos(deltaPrimeRad) * Math.Cos(Deg2Rad(hPrime))));
        }

        private double sun_rise_and_set(ref double[] mRts, ref double[] hRts, ref double[] deltaPrime, double latitude,
                                ref double[] hPrime, double h0Prime, int sun)
        {
            return mRts[sun] + (hRts[sun] - h0Prime) /
                  (360.0 * Math.Cos(Deg2Rad(deltaPrime[sun])) * Math.Cos(Deg2Rad(latitude)) * Math.Sin(Deg2Rad(hPrime[sun])));
        }

        #endregion Methods

        ////////////////////////////////////////////////////////////////////////////////////////////////
        // Calculate required SPA parameters to get the right ascension (alpha) and declination (delta)
        // Note: JD must be already calculated and in structure
        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region SPA

        public void calculate_geocentric_sun_right_ascension_and_declination(ref SpaData spa)
        {
            double[] x = new double[(int)TermX.TermXCount];

            spa.Jc = julian_century(spa.Jd);

            spa.Jde = julian_ephemeris_day(spa.Jd, spa.DeltaT);
            spa.Jce = julian_ephemeris_century(spa.Jde);
            spa.Jme = julian_ephemeris_millennium(spa.Jce);

            spa.L = earth_heliocentric_longitude(spa.Jme);
            spa.B = earth_heliocentric_latitude(spa.Jme);
            spa.R = earth_radius_vector(spa.Jme);

            spa.Theta = geocentric_longitude(spa.L);
            spa.Beta = geocentric_latitude(spa.B);

            x[(int)TermX.TermX0] = spa.X0 = mean_elongation_moon_sun(spa.Jce);
            x[(int)TermX.TermX1] = spa.X1 = mean_anomaly_sun(spa.Jce);
            x[(int)TermX.TermX2] = spa.X2 = mean_anomaly_moon(spa.Jce);
            x[(int)TermX.TermX3] = spa.X3 = argument_latitude_moon(spa.Jce);
            x[(int)TermX.TermX4] = spa.X4 = ascending_longitude_moon(spa.Jce);

            nutation_longitude_and_obliquity(spa.Jce, x, ref spa.DelPsi, ref spa.DelEpsilon);

            spa.Epsilon0 = ecliptic_mean_obliquity(spa.Jme);
            spa.Epsilon = ecliptic_true_obliquity(spa.DelEpsilon, spa.Epsilon0);

            spa.DelTau = aberration_correction(spa.R);
            spa.Lamda = apparent_sun_longitude(spa.Theta, spa.DelPsi, spa.DelTau);
            spa.Nu0 = greenwich_mean_sidereal_time(spa.Jd, spa.Jc);
            spa.Nu = greenwich_sidereal_time(spa.Nu0, spa.DelPsi, spa.Epsilon);

            spa.Alpha = geocentric_sun_right_ascension(spa.Lamda, spa.Epsilon, spa.Beta);
            spa.Delta = geocentric_sun_declination(spa.Beta, spa.Epsilon, spa.Lamda);
        }

        #endregion SPA

        ////////////////////////////////////////////////////////////////////////
        // Calculate Equation of Time (EOT) and Sun Rise, Transit, & Set (RTS)
        ////////////////////////////////////////////////////////////////////////

        #region Calculate Equation of Time (EOT) and Sun Rise, Transit, & Set (RTS)

        private void calculate_eot_and_sun_rise_transit_set(ref SpaData spa)
        {
            SpaData sunRts;
            double nu, m, h0, n;
            double[] alpha = new double[(int)Julian.JdCount];
            double[] delta = new double[(int)Julian.JdCount];
            double[] mRts = new double[(int)SunTerm.SunCount];
            double[] nuRts = new double[(int)SunTerm.SunCount];
            double[] hRts = new double[(int)SunTerm.SunCount];
            double[] alphaPrime = new double[(int)SunTerm.SunCount];
            double[] deltaPrime = new double[(int)SunTerm.SunCount];
            double[] hPrime = new double[(int)SunTerm.SunCount];
            double h0Prime = -1 * (SunRadius + spa.AtmosRefract);
            int i;

            sunRts = spa;
            m = sun_mean_longitude(spa.Jme);
            spa.Eot = Eot(m, spa.Alpha, spa.DelPsi, spa.Epsilon);

            sunRts.Hour = sunRts.Minute = sunRts.Second = 0;
            sunRts.Timezone = 0.0;

            sunRts.Jd = julian_day(sunRts.Year, sunRts.Month, sunRts.Day,
                                     sunRts.Hour, sunRts.Minute, sunRts.Second, sunRts.Timezone);

            calculate_geocentric_sun_right_ascension_and_declination(ref sunRts);
            nu = sunRts.Nu;

            sunRts.DeltaT = 0;
            sunRts.Jd--;
            for (i = 0; i < (int)Julian.JdCount; i++)
            {
                calculate_geocentric_sun_right_ascension_and_declination(ref sunRts);
                alpha[i] = sunRts.Alpha;
                delta[i] = sunRts.Delta;
                sunRts.Jd++;
            }

            mRts[(int)SunTerm.SunTransit] = approx_sun_transit_time(alpha[(int)Julian.JdZero], spa.Longitude, nu);
            h0 = sun_hour_angle_at_rise_set(spa.Latitude, delta[(int)Julian.JdZero], h0Prime);

            if (h0 >= 0)
            {
                approx_sun_rise_and_set(ref mRts, h0);

                for (i = 0; i < (int)SunTerm.SunCount; i++)
                {
                    nuRts[i] = nu + 360.985647 * mRts[i];

                    n = mRts[i] + spa.DeltaT / 86400.0;
                    alphaPrime[i] = rts_alpha_delta_prime(ref alpha, n);
                    deltaPrime[i] = rts_alpha_delta_prime(ref delta, n);

                    hPrime[i] = limit_degrees180pm(nuRts[i] + spa.Longitude - alphaPrime[i]);

                    hRts[i] = rts_sun_altitude(spa.Latitude, deltaPrime[i], hPrime[i]);
                }

                spa.Srha = hPrime[(int)SunTerm.SunRise];
                spa.Ssha = hPrime[(int)SunTerm.SunSet];
                spa.Sta = hRts[(int)SunTerm.SunTransit];

                spa.Suntransit = dayfrac_to_local_hr(mRts[(int)SunTerm.SunTransit] - hPrime[(int)SunTerm.SunTransit] / 360.0,
                                                      spa.Timezone);

                spa.Sunrise = dayfrac_to_local_hr(sun_rise_and_set(ref mRts, ref hRts, ref deltaPrime,
                                  spa.Latitude, ref hPrime, h0Prime, (int)SunTerm.SunRise), spa.Timezone);

                spa.Sunset = dayfrac_to_local_hr(sun_rise_and_set(ref mRts, ref hRts, ref deltaPrime,
                                  spa.Latitude, ref hPrime, h0Prime, (int)SunTerm.SunSet), spa.Timezone);
            }
            else spa.Srha = spa.Ssha = spa.Sta = spa.Suntransit = spa.Sunrise = spa.Sunset = -99999;
        }

        #endregion Calculate Equation of Time (EOT) and Sun Rise, Transit, & Set (RTS)

        ///////////////////////////////////////////////////////////////////////////////////////////
        // Calculate all SPA parameters and put into structure
        // Note: All inputs values (listed in header file) must already be in structure
        ///////////////////////////////////////////////////////////////////////////////////////////

        #region Calculate all SPA parameters and put into structure

        public int spa_calculate(ref SpaData spa)
        {
            int result;

            result = validate_inputs(ref spa);

            if (result == 0)
            {
                spa.Jd = julian_day(spa.Year, spa.Month, spa.Day,
                                      spa.Hour, spa.Minute, spa.Second, spa.Timezone);

                calculate_geocentric_sun_right_ascension_and_declination(ref spa);

                spa.H = observer_hour_angle(spa.Nu, spa.Longitude, spa.Alpha);
                spa.Xi = sun_equatorial_horizontal_parallax(spa.R);

                sun_right_ascension_parallax_and_topocentric_dec(spa.Latitude, spa.Elevation, spa.Xi,
                                            spa.H, spa.Delta, ref spa.DelAlpha, ref spa.DeltaPrime);

                spa.AlphaPrime = topocentric_sun_right_ascension(spa.Alpha, spa.DelAlpha);
                spa.HPrime = topocentric_local_hour_angle(spa.H, spa.DelAlpha);

                spa.E0 = topocentric_elevation_angle(spa.Latitude, spa.DeltaPrime, spa.HPrime);
                spa.DelE = atmospheric_refraction_correction(spa.Pressure, spa.Temperature,
                                                                 spa.AtmosRefract, spa.E0);
                spa.E = topocentric_elevation_angle_corrected(spa.E0, spa.DelE);

                spa.Zenith = topocentric_zenith_angle(spa.E);
                spa.Azimuth180 = topocentric_azimuth_angle_neg180_180(spa.HPrime, spa.Latitude,
                                                                                     spa.DeltaPrime);
                spa.Azimuth = topocentric_azimuth_angle_zero_360(spa.Azimuth180);

                if ((spa.Function == (int)SpaSelect.SpaZaInc) || (spa.Function == (int)SpaSelect.SpaAll))
                    spa.Incidence = surface_incidence_angle(spa.Zenith, spa.Azimuth180,
                                                              spa.AzmRotation, spa.Slope);

                if ((spa.Function == (int)SpaSelect.SpaZaRts) || (spa.Function == (int)SpaSelect.SpaAll))
                    calculate_eot_and_sun_rise_transit_set(ref spa);
            }

            return result;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        #endregion Calculate all SPA parameters and put into structure
    }
}