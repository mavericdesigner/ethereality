using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Isis
{
    /// <summary>
    /// Interaction logic for CellLevelBar.xaml
    /// </summary>
    public partial class CellLevelBar : UserControl
    {
        public Int16 CellVoltage
        {
            get { return (Int16)GetValue(CellVoltageProperty); }
            set { SetValue(CellVoltageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CellVoltage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CellVoltageProperty =
            DependencyProperty.Register("CellVoltage", typeof(Int16), typeof(CellLevelBar), new PropertyMetadata(new Int16(), OnCellVoltageChanged));

        private static void OnCellVoltageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Int16 newCellVolt = (Int16)e.NewValue;
            CellLevelBar cellLevelBar = (CellLevelBar)d;
            cellLevelBar.CellBar.Value = Convert.ToDouble(newCellVolt) / 4200 * 100;
            cellLevelBar.CellVolt.Text = newCellVolt.ToString();
            cellLevelBar.CellVolt.Text = newCellVolt.ToString();

            if (newCellVolt >= 3800)
            {
                cellLevelBar.CellBar.Value = Convert.ToDouble(newCellVolt) / 4200 * 100;
                cellLevelBar.CellBar.Foreground = Brushes.Green;
            }
            if (newCellVolt < 3800 && newCellVolt > 3000)
            {
                cellLevelBar.CellBar.Value = Convert.ToDouble(newCellVolt) / 4200 * 100;
                cellLevelBar.CellBar.Foreground = Brushes.Yellow;
            }
            if (newCellVolt < 3000)
            {
                cellLevelBar.CellBar.Value = Convert.ToDouble(newCellVolt) / 4200 * 100;
                cellLevelBar.CellBar.Foreground = Brushes.Red;
            }

            if (newCellVolt < 0)
            {
                cellLevelBar.CellBar.Foreground = Brushes.Red;
                cellLevelBar.CellBar.Value = 100;
                cellLevelBar.CellVolt.FontSize = 10;
                cellLevelBar.CellVolt.Text = "ERROR";
            }
        }

        public CellLevelBar()
        {
            this.InitializeComponent();
        }
    }
}