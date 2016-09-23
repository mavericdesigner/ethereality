using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Isis.CustomControls.Analytics
{
    /// <summary>
    /// Interaction logic for RealTimePlotControl.xaml
    /// </summary>
    public partial class RealTimePlotControl : UserControl
    {
        public RealTimePlotControl()
        {
            InitializeComponent();
        }

        public ObservableCollection<DataPoint> ICollectionSeries
        {
            get { return (ObservableCollection<DataPoint>)GetValue(ICollectionSeriesProperty); }
            set { SetValue(ICollectionSeriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlotSeries2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ICollectionSeriesProperty =
            DependencyProperty.Register("ICollectionSeries", typeof(ObservableCollection<DataPoint>), typeof(RealTimePlotControl), new PropertyMetadata(new ObservableCollection<DataPoint>(), OnPlotSeries2Changed));

        private static void OnPlotSeries2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ObservableCollection<DataPoint> newSeriesCollection = (ObservableCollection<DataPoint>)e.NewValue;
            List<DataPoint> newSeriesList = new List<DataPoint>(newSeriesCollection);
            RealTimePlotControl plotControl = (RealTimePlotControl)d;
            var _lineSeries = new LineSeries();
            _lineSeries.ItemsSource = newSeriesList;
            PlotModel newModel = new PlotModel();
            newModel.Series.Add(_lineSeries);
            plotControl.PlotPanel.Model = newModel;
        }

        public static PlotModel MyModel { get; set; }

        public IList<DataPoint> IListSeries
        {
            get { return (IList<DataPoint>)GetValue(IListSeriesProperty); }
            set { SetValue(IListSeriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlotSeries.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IListSeriesProperty =
            DependencyProperty.Register("IListSeries", typeof(IList<DataPoint>), typeof(RealTimePlotControl), new PropertyMetadata(new List<DataPoint>(), OnPlotSeriesChanged));

        private static void OnPlotSeriesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IList<DataPoint> newPlotSeries = (IList<DataPoint>)e.NewValue;
            RealTimePlotControl plotControl = (RealTimePlotControl)d;
            var _lineSeries = new LineSeries();
            _lineSeries.ItemsSource = newPlotSeries;
            PlotModel newModel = new PlotModel();
            newModel.Series.Add(_lineSeries);
            MyModel = newModel;
            plotControl.PlotPanel.Model = MyModel;
        }

        public IList<DataPoint> IListTimeSeries
        {
            get { return (IList<DataPoint>)GetValue(IListTimeSeriesProperty); }
            set { SetValue(IListTimeSeriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IListTimeSeries.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IListTimeSeriesProperty =
            DependencyProperty.Register("IListTimeSeries", typeof(IList<DataPoint>), typeof(RealTimePlotControl), new PropertyMetadata(new List<DataPoint>(), OnPlotSeriesTimeChanged));

        private static void OnPlotSeriesTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IList<DataPoint> TimePlotSeries = (IList<DataPoint>)e.NewValue;
            RealTimePlotControl plotControl = (RealTimePlotControl)d;

            //MyModel.InvalidatePlot(true);
            var plotModel1 = new PlotModel();
            plotModel1.Title = "DateTime axis";
            var dateTimeAxis1 = new DateTimeAxis();
            plotModel1.Axes.Add(dateTimeAxis1);
            var linearAxis1 = new LinearAxis();
            plotModel1.Axes.Add(linearAxis1);

            var lineSeries1 = new LineSeries();

            lineSeries1.ClearSelection();
            lineSeries1.ItemsSource = TimePlotSeries;
            //lineSeries1.Color = OxyColor.FromArgb(255, 78, 154, 6);
            // lineSeries1.MarkerFill = OxyColor.FromArgb(255, 78, 154, 6);
            // lineSeries1.MarkerStroke = OxyColors.ForestGreen;
            //  lineSeries1.MarkerType = MarkerType.Square;
            lineSeries1.StrokeThickness = 1;
            lineSeries1.DataFieldX = "Date";
            lineSeries1.DataFieldY = "Value";

            plotModel1.ResetAllAxes();
            plotModel1.Series.Add(lineSeries1);

            plotControl.PlotPanel.Model = plotModel1;
            plotControl.PlotPanel.DisconnectCanvasWhileUpdating = true;
        }
    }
}