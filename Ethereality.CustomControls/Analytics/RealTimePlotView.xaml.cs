using System.Windows;
using OxyPlot;
using System.Collections.Generic;
using System.Windows.Controls;


namespace Isis.CustomControls.Analytics
{
    /// <summary>
    /// Description for RealTimePlotView.
    /// </summary>
    public partial class RealTimePlotView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the RealTimePlotView class.
        /// </summary>
        public RealTimePlotView()
        {
            InitializeComponent();
        }



        public IList<DataPoint> PlotDataPoints
        {
            get { return (IList<DataPoint>)GetValue(PlotDataPointsProperty); }
            set { SetValue(PlotDataPointsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlotDataPoints.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlotDataPointsProperty =
            DependencyProperty.Register("PlotDataPoints", typeof(IList<DataPoint>), typeof(RealTimePlotView), new PropertyMetadata(new List<DataPoint>(),OnPlotDataChanged));

        private static void OnPlotDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IList<DataPoint> datapoints = (IList<DataPoint>)e.NewValue;
            RealTimePlotView realTimePlotView = (RealTimePlotView)d;

        }

        
    }
}