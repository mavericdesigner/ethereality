
using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Wpf;
using System.Windows.Controls;
using System.Windows;
using OxyPlot.Series;
using System;

namespace Isis.CustomControls.Analytics
{
    /// <summary>
    /// Interaction logic for LivePlot.xaml
    /// </summary>
    public partial class LivePlot : UserControl
    {
        public LivePlot()
        {
           InitializeComponent();
          
            
        }



        

        public IList<DataPoint> DataPoints
        {
            get { return (IList<DataPoint>)GetValue(DataPointsProperty); }
            set { SetValue(DataPointsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataPoints.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataPointsProperty =
            DependencyProperty.Register("DataPoints", typeof(IList<DataPoint>), typeof(LivePlot), new PropertyMetadata(OnDataPointsChanged));

        private static void OnDataPointsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            LivePlot plot = (LivePlot)d;
            PlotModel MyModel = new PlotModel();
            MyModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            plot.LivePlotView.Model = MyModel;
        }

        
  
     

        
    }
}
