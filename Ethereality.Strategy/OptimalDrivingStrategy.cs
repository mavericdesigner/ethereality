using Ethereality.CustomTypes;
using Etheriality.Constants;
using SPA;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Core;

namespace Ethereality.Model.EnergyManagement.LocalModels
{
    public struct SampleInit
    {
        #region Properties

        public double Binit { get; set; }

        public double Vinit { get; set; }

        #endregion Properties
    }

    public struct SampleStats
    {
        #region Properties

        public double Kurtosis { get; set; }

        public double Mean { get; set; }

        public double Median { get; set; }

        public double Skewness { get; set; }

        public double Variance { get; set; }

        #endregion Properties
    }

    public struct States
    {
        #region Properties

        public double BatteryEnergy { get; set; }

        public double BatteryPower { get; set; }

        public double Cost { get; set; }

        public double CoulombCount { get; set; }

        public double Current { get; set; }

        public double DayTime { get; set; }

        public double Elevation { get; set; }

        public int Index { get; set; }

        public double Position { get; set; }

        public double SegmentLength { get; set; }

        public double Slope { get; set; }

        public double SolarEnergy { get; set; }

        public double Speed { get; set; }

        public double Time { get; set; }

        #endregion Properties
    }

    public class OptimalDrivingStrategy : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Fields

        /// <summary>
        /// The <see cref="BrightnessFactor" /> property's name.
        /// </summary>
        public const string BrightnessFactorPropertyName = "BrightnessFactor";

        /// <summary>
        /// The <see cref="CombinedTrack" /> property's name.
        /// </summary>
        public const string CombinedTrackPropertyName = "CombinedTrack";

        /// <summary>
        /// The <see cref="FinishTime" /> property's name.
        /// </summary>
        public const string FinishTimePropertyName = "FinishTime";

        /// <summary>
        /// The <see cref="InitialArraySet" /> property's name.
        /// </summary>
        public const string InitialArraySetPropertyName = "InitialArraySet";

        /// <summary>
        /// The <see cref="NumberOfLoops" /> property's name.
        /// </summary>
        public const string NumberOfLoopsPropertyName = "NumberOfLoops";

        /// <summary>
        /// The <see cref="SampleStats" /> property's name.
        /// </summary>
        public const string SampleStatsPropertyName = "SampleStats";

        /// <summary>
        /// The <see cref="SolarModelCollection" /> property's name.
        /// </summary>
        public const string SolarModelCollectionPropertyName = "SolarModelCollection";

        /// <summary>
        /// The <see cref="SolarModel" /> property's name.
        /// </summary>
        public const string SolarModelPropertyName = "SolarModel";

        /// <summary>
        /// The <see cref="SolarModelService" /> property's name.
        /// </summary>
        public const string SolarModelServicePropertyName = "SolarModelService";

        /// <summary>
        /// The <see cref="TrackLoop" /> property's name.
        /// </summary>
        public const string TrackLoopPropertyName = "TrackLoop";

        /// <summary>
        /// The <see cref="Track" /> property's name.
        /// </summary>
        public const string TrackPropertyName = "Track";

        /// <summary>
        /// The <see cref="VehicleState" /> property's name.
        /// </summary>
        public const string VehicleStatePropertyName = "VehicleStates";

        /// <summary>
        /// The <see cref="VehicleStates" /> property's name.
        /// </summary>
        public const string VehicleStatesPropertyName = "VehicleStates";

        /// <summary>
        /// The <see cref="VelocityReference" /> property's name.
        /// </summary>
        public const string VelocityReferencePropertyName = "VelocityReference";

        // All States Are in Reverse
        private double _brightnessFactor;

        private ObservableCollection<RouteSegmentVector> _combinedTrack;
        private DateTime _finishTime;
        private ObservableCollection<SampleInit> _initialArraySet = new ObservableCollection<SampleInit>();
        private int _numberOfLoops;
        private SampleStats _sampleStats = new SampleStats();
        private SpaData _solarModel;
        private ObservableCollection<SpaData> _solarModelCollection;
        private SpaService _solarModelService = new SpaService();
        private ObservableCollection<RouteSegmentVector> _track = new ObservableCollection<RouteSegmentVector>();
        private ObservableCollection<RouteSegmentVector> _trackLoop = new ObservableCollection<RouteSegmentVector>();
        private States _vehicleState;

        private ObservableCollection<States> _vehicleStates;

        private double _velocityReference;

        #endregion Fields

        #region Properties

        public double BrightnessFactor
        {
            get
            {
                return _brightnessFactor;
            }

            set
            {
                if (_brightnessFactor == value)
                {
                    return;
                }

                _brightnessFactor = value;
                NotifyPropertyChanged(BrightnessFactorPropertyName);
            }
        }

        /// <summary>
        /// Sets and gets the CombinedTrack property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<RouteSegmentVector> CombinedTrack
        {
            get
            {
                return _combinedTrack;
            }

            set
            {
                if (_combinedTrack == value)
                {
                    return;
                }

                _combinedTrack = value;
                NotifyPropertyChanged(CombinedTrackPropertyName);
            }
        }

        /// <summary>
        /// Sets and gets the BrightnessFactor property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        /// <summary>
        /// Sets and gets the FinishTime property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public DateTime FinishTime
        {
            get
            {
                return _finishTime;
            }

            set
            {
                if (_finishTime == value)
                {
                    return;
                }

                _finishTime = value;
                NotifyPropertyChanged(FinishTimePropertyName);
            }
        }

        /// <summary>
        /// Sets and gets the InitialArraySet property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<SampleInit> InitialArraySet
        {
            get
            {
                return _initialArraySet;
            }

            set
            {
                if (_initialArraySet.Equals(value))
                {
                    return;
                }

                _initialArraySet = value;
                NotifyPropertyChanged(InitialArraySetPropertyName);
            }
        }

        /// <summary>
        /// Sets and gets the NumberOfLoops property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int NumberOfLoops
        {
            get
            {
                return _numberOfLoops;
            }

            set
            {
                if (_numberOfLoops == value)
                {
                    return;
                }

                _numberOfLoops = value;
                NotifyPropertyChanged(NumberOfLoopsPropertyName);
            }
        }

        /// <summary>
        /// Sets and gets the SampleStats property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public SampleStats SampleStats
        {
            get
            {
                return _sampleStats;
            }

            set
            {
                if (_sampleStats.Equals(value))
                {
                    return;
                }

                _sampleStats = value;
                NotifyPropertyChanged(SampleStatsPropertyName);
            }
        }

        /// <summary>
        /// Sets and gets the SolarModel property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public SpaData SolarModel
        {
            get
            {
                return _solarModel;
            }

            set
            {
                if (_solarModel.Equals(value))
                {
                    return;
                }

                _solarModel = value;
                NotifyPropertyChanged(SolarModelPropertyName);
            }
        }

        /// <summary>
        /// Sets and gets the SolarModelCollection property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<SpaData> SolarModelCollection
        {
            get
            {
                return _solarModelCollection;
            }

            set
            {
                if (_solarModelCollection == value)
                {
                    return;
                }

                _solarModelCollection = value;
                NotifyPropertyChanged(SolarModelCollectionPropertyName);
            }
        }

        /// <summary>
        /// Sets and gets the SolarModelService property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public SpaService SolarModelService
        {
            get
            {
                return _solarModelService;
            }

            set
            {
                if (_solarModelService == value)
                {
                    return;
                }

                _solarModelService = value;
                NotifyPropertyChanged(SolarModelServicePropertyName);
            }
        }

        /// <summary>
        /// Sets and gets the Track property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<RouteSegmentVector> Track
        {
            get
            {
                return _track;
            }

            set
            {
                if (_track == value)
                {
                    return;
                }

                _track = value;
                NotifyPropertyChanged(TrackPropertyName);
            }
        }

        /// <summary>
        /// Sets and gets the TrackLoop property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<RouteSegmentVector> TrackLoop
        {
            get
            {
                return _trackLoop;
            }

            set
            {
                if (_trackLoop == value)
                {
                    return;
                }

                _trackLoop = value;
                NotifyPropertyChanged(TrackLoopPropertyName);
            }
        }

        /// <summary>
        /// Sets and gets the VehicleStates property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public States VehicleState
        {
            get
            {
                return _vehicleState;
            }

            set
            {
                if (_vehicleState.Equals(value))
                {
                    return;
                }

                _vehicleState = value;
                NotifyPropertyChanged(VehicleStatePropertyName);
            }
        }

        /// <summary>
        /// Sets and gets the VehicleStates property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<States> VehicleStates
        {
            get
            {
                return _vehicleStates;
            }

            set
            {
                if (_vehicleStates == value)
                {
                    return;
                }

                _vehicleStates = value;
                NotifyPropertyChanged(VehicleStatesPropertyName);
            }
        }

        /// <summary>
        /// Sets and gets the VelocityReference property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public double VelocityReference
        {
            get
            {
                return _velocityReference;
            }

            set
            {
                if (_velocityReference == value)
                {
                    return;
                }

                _velocityReference = value;
                NotifyPropertyChanged(VelocityReferencePropertyName);
            }
        }

        #endregion Properties

        #region Constructors

        public OptimalDrivingStrategy()
        {
            _vehicleState = new States();
            _vehicleStates = new ObservableCollection<States>();
            _solarModel = new SpaData();
            _solarModelCollection = new ObservableCollection<SpaData>();
            _solarModelService = new SpaService();
            _sampleStats = new SampleStats();
            _combinedTrack = new ObservableCollection<RouteSegmentVector>();
            _finishTime = DateTime.Now.ToLocalTime();
            _velocityReference = 19.0;
            _brightnessFactor = 0.8;
            _numberOfLoops = 0;
        }

        #endregion Constructors

        #region Methods

        public static void CoulombCount(double b, double tau, out double qs, ref double qf)
        {
            qs = qf + Current(b) * tau;
            qf = qs;
        }

        public void BfgsAlgorithm()
        {
            double[] randB = new double[30] { -10000, -4000.0, -3000.0, -200.0, -100.0, -90.0, -80.0, -70.0, -60, -50, -40, -30, -20, -10, -9, 0.0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 3000, 4000, 10000 };
            double[] randV = new double[30] { 0.0, 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0, 11.0, 12.0, 13.0, 14.0, 15.0, 16.0, 17.0, 18.0, 19.0, 20.0, 21.0, 22.0, 23.0, 24.0, 25.0, 26.0, 27.0, 28.0, 30 };

            SampleInit sampinit = new SampleInit();

            for (int k = 0; k < 20; k++)
            {
                alglib.hqrndstate vnewstate;
                alglib.hqrndstate bnewstate;
                alglib.hqrndrandomize(out vnewstate);
                alglib.hqrndrandomize(out bnewstate);
                double bitem = alglib.hqrndcontinuous(bnewstate, randB, 30);
                double vitem = alglib.hqrndcontinuous(bnewstate, randV, 30);

                double[] x = new double[] { vitem, bitem };

                double epsg = 1.0e-6;
                double epsf = 0;
                double epsx = 0;
                int maxits = 3000;

                alglib.minlbfgsstate state;
                alglib.minlbfgsreport rep;

                alglib.minlbfgscreate(1, x, out state);
                alglib.minlbfgssetcond(state, epsg, epsf, epsx, maxits);
                alglib.minlbfgsoptimize(state, s1_grad, null, null);
                alglib.minlbfgsresults(state, out x, out rep);
                sampinit.Vinit = x[0];
                sampinit.Binit = x[1];
                _initialArraySet.Add(sampinit);
            }
            double speedMedian = 1.0;
            double batteryMedian = 0.0;
            speedMedian = _initialArraySet.Where(x => !Double.IsNaN(x.Vinit)).Average(x => x.Vinit);
            batteryMedian = _initialArraySet.Where(x => !Double.IsNaN(x.Binit)).Average(x => x.Binit);
            _vehicleState.Speed = speedMedian;
            _vehicleState.BatteryPower = batteryMedian;
            _initialArraySet = new ObservableCollection<SampleInit>();
        }

        public void RouteJoin()
        {
            try
            {
                foreach (var point in Track)
                {
                    for (int i = 0; i < _numberOfLoops; i++)
                    {
                        if ((Math.Abs(point.Point1.Latitude - TrackLoop[0].Point1.Latitude) < 0.0001) && Math.Abs(point.Point1.Longitude - TrackLoop[0].Point1.Longitude) < 0.0001)
                        {
                            _combinedTrack.Remove(point);
                            foreach (var item in TrackLoop)
                            {
                                _combinedTrack.Add(item);
                            }
                        }
                    }
                    _combinedTrack.Add(point);
                }
            }
            catch (Exception e)
            {
                // MessageBox.Show(e.Message);
            }
        }

        public void s1_grad(double[] x, ref double func, double[] grad, object obj)
        {
            //
            // this callback calculates f(x) = (1+x)^(-0.2) + (1-x)^(-0.3) + 1000*x and its gradient.
            //
            // function is trimmed when we calculate it near the singular points or outside of the [-1,+1].
            // Note that we do NOT calculate gradient in this case.
            //

            double eta = -0.000000002;
            double beta = -500;
            double zeta = -500;
            double theta = _vehicleState.Slope;
            double r0 = 16.1091;
            double R1 = 0.027793396534818913;
            double R2 = 0.5387755102040851;
            double mass = 260;
            double m = 1 / (Math.Sin((_solarModel.Incidence + 90) * ConstantValue.ToRad));
            double s = 1353 * Math.Pow(0.687, Math.Pow(m, 0.687)) * 0.35 * 3.0 * _brightnessFactor;
            double dE = 0;
            _vehicleState.SolarEnergy = s;
            double vref = _velocityReference;
            const double degree = 0.017453292519943295769;
            double v = x[0];
            double b = x[1];
            //if ((x[0] <= 0.9999999) || (x[0] >= 30.9999999))
            //{
            //    func = 1.0E+300;
            //    return;
            //}

            func = Math.Pow(v - vref, 2) * beta - ((b * ConstantValue.C1) / 38.0 + (Math.Pow(b, 2) * ConstantValue.C2) / 38.0) * zeta + eta * Math.Pow(b + dE + s - v * (r0 + R1 * v + R2 * Math.Pow(v, 2) + 9.18 * mass * Math.Sin(degree * theta)), 2);

            grad[1] = 2 * (v - vref) * beta + 2 * eta * (-r0 - R1 * v - R2 * Math.Pow(v, 2) - v * (R1 + 2 * R2 * v) - 9.18 * mass * Math.Sin(degree * theta)) * (b + dE + s - v * (r0 + R1 * v + R2 * Math.Pow(v, 2) + 9.18 * mass * Math.Sin(degree * theta)));

            grad[0] = (-ConstantValue.C1 / 38.0 - (b * ConstantValue.C2) / 19.0) * zeta + 2 * eta * (b + dE + s - v * (r0 + R1 * v + R2 * Math.Pow(v, 2) + 9.18 * mass * Math.Sin(degree * theta)));
        }

        public void Solver()
        {
            RouteJoin();
            _track = _combinedTrack;
            int n = _track.Count();
            int k = n - 1;
            _vehicleState.BatteryEnergy = 0;

            ObservableCollection<States> localStates = new ObservableCollection<States>();
            ObservableCollection<SpaData> localSolar = new ObservableCollection<SpaData>();
            TimeSpan time = new TimeSpan(0, 15, 41, 0);
            DateTime timer = new DateTime(2014, 9, 27, 0, 0, 0);
            _vehicleState.Time = time.TotalSeconds;

            timer = timer.Add(time);
            while (k >= 0)
            {
                SolarCalculation(k, timer);
                _vehicleState.Slope = GradientFilter(_track, k);
                _vehicleState.SegmentLength = _track[k].Gcd;
                _vehicleState.DayTime = _vehicleState.Time;
                _vehicleState.Position -= _vehicleState.SegmentLength;
                _vehicleState.Index = k;
                BfgsAlgorithm();
                _vehicleState.Time -= VehicleState.SegmentLength / _vehicleState.Speed;
                double hour = 0;
                hour = (VehicleState.SegmentLength / _vehicleState.Speed) / (60 * 60);
                _vehicleState.BatteryEnergy += (_vehicleState.BatteryPower) * hour;
                _vehicleState.CoulombCount += (VehicleState.SegmentLength / _vehicleState.Speed) * Current(_vehicleState.BatteryPower) / 3600;
                _vehicleState.Current = Current(_vehicleState.BatteryPower);
                _vehicleState.Elevation = _track[k].Elevation;
                _vehicleStates.Add(_vehicleState);

                timer = timer.Subtract(TimeSpan.FromSeconds(_vehicleState.SegmentLength / _vehicleState.Speed));
                _solarModelCollection.Add(SolarModel);
                --k;
            }

            Statistics();

            //System.Console.WriteLine("{0}", alglib.ap.format(x, 7)); // EXPECTED: [-0.99917305]

            //System.Console.ReadLine();
            //return 0;
        }

        private static double Acceleration(double s, double b, double vs, double theta)
        {
            double a;
            a = s + b - R(vs) + G(theta);
            return a;
        }

        private static double Current(double b)
        {
            return ConstantValue.C2 * Math.Pow(b, 2) + ConstantValue.C1 * b;
        }

        private static void Distance(double a, double vs, double tau, ref double xf, out double xs)
        {
            xs = xf - vs - 0.5 * a * Math.Pow(tau, 2);
            xf = xs;
        }

        private static double Dr(double v)
        {
            double drForce;
            drForce = ConstantValue.R1 + 2 * ConstantValue.R2 * v;
            return drForce;
        }

        private static double G(double theta)
        {
            double gForce;
            gForce = -1 * ConstantValue.Mass * ConstantValue.G * Math.Sin(theta * ConstantValue.ToRad);
            return gForce;
        }

        private static double GradientFilter(ObservableCollection<RouteSegmentVector> routeSegmentVectors, int k)
        {
            double theta;
            if (Math.Abs(routeSegmentVectors[k].Slope) > 8)
            {
                theta = 0;
            }
            else
            {
                theta = routeSegmentVectors[k].Slope;
            }
            return theta;
        }

        private static double R(double v)
        {
            double rForce;
            rForce = ConstantValue.R2 * Math.Pow(v, 2) + ConstantValue.R1 * v + ConstantValue.R2;
            return rForce;
        }

        private static void Speed(double a, double tau, out double vs, ref double vf)
        {
            vs = vf - a * tau;
            vf = vs;
        }

        private void SolarCalculation(int k, DateTime timer)
        {
            _solarModel.Year = timer.Year;
            _solarModel.Month = timer.Month;
            _solarModel.Day = timer.Day;
            _solarModel.Hour = timer.Hour;
            _solarModel.Minute = timer.Minute;
            _solarModel.Second = timer.Second;
            //      _solarModel.Delta_ut1 = -0.3;
            _solarModel.DeltaT = 67;
            _solarModel.Timezone = +2;
            _solarModel.Longitude = _track[k].Point1.Longitude;
            _solarModel.Latitude = _track[k].Point1.Latitude;
            _solarModel.Elevation = _track[k].Point1.Elevation;
            _solarModel.Pressure = 1025;
            _solarModel.Temperature = 30;
            _solarModel.Slope = GradientFilter(_track, k);
            _solarModel.AzmRotation = _track[k].Azimuth;
            _solarModel.AtmosRefract = 0.5667;
            _solarModel.Function = (int)SpaSelect.SpaAll;

            _solarModelService.SpaValues = _solarModel;
            _solarModelService.SpaDataCalculate(_solarModelService.SpaValues);
            _solarModel = _solarModelService.SpaValues;
        }

        private void Statistics()
        {
            double[] samples = _vehicleStates.Select(state => state.Speed).ToList().ToArray();
            double mean = 0.0;
            double variance = 0.0;
            double skewness = 0.0;
            double kurtosis = 0.0;
            double median = 0.0;
            alglib.samplemoments(samples, out mean, out variance, out skewness, out kurtosis);
            alglib.samplemedian(samples, out median);
            _sampleStats.Mean = mean;
            _sampleStats.Variance = variance;
            _sampleStats.Skewness = skewness;
            _sampleStats.Kurtosis = kurtosis;
            _sampleStats.Median = median;
        }

        #endregion Methods

        private async void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                await
                   Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () => PropertyChanged(this, new PropertyChangedEventArgs(propertyName)));
            }
        }
    }
}