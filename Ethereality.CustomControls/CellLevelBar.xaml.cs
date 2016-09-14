using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Ethereality.CustomControls
{
    public sealed partial class CellLevelBar : UserControl
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
                cellLevelBar.CellBar.Foreground = new SolidColorBrush(Colors.Green);
            }
            if (newCellVolt < 3800 && newCellVolt > 3000)
            {
                cellLevelBar.CellBar.Value = Convert.ToDouble(newCellVolt) / 4200 * 100;
                cellLevelBar.CellBar.Foreground = new SolidColorBrush(Colors.Yellow);
            }
            if (newCellVolt < 3000)
            {
                cellLevelBar.CellBar.Value = Convert.ToDouble(newCellVolt) / 4200 * 100;
                cellLevelBar.CellBar.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (newCellVolt < 0)
            {
                cellLevelBar.CellBar.Foreground = new SolidColorBrush(Colors.Red);
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
