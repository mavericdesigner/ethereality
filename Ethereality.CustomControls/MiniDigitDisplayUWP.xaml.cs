using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class MiniDigitDisplayUwp : UserControl
    {
        public double MiniDisplayValue
        {
            get { return (double)GetValue(MiniDisplayValueProperty); }
            set { SetValue(MiniDisplayValueProperty, value); }
        }
        
        // Using a DependencyProperty as the backing store for MiniDisplayValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MiniDisplayValueProperty =
            DependencyProperty.Register("MiniDisplayValue", typeof(double), typeof(MiniDigitDisplayUwp),new PropertyMetadata(9999.9999,OnDisplayValueChanged));


        public string MiniDisplayUnit
        {
            get { return (string)GetValue(MiniDisplayUnitProperty); }
            set { SetValue(MiniDisplayUnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MiniDisplayUnit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MiniDisplayUnitProperty =
            DependencyProperty.Register("MiniDisplayUnit", typeof(string), typeof(MiniDigitDisplayUwp), new PropertyMetadata("NoUnit",OnDisplayUnitChanged));

        private static void OnDisplayUnitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newMiniDisplayUnit = e.NewValue.ToString();
            var miniDigitDisplay = (MiniDigitDisplayUwp)d;
            miniDigitDisplay.TextBlockDisplayUnit.Text = newMiniDisplayUnit;
        }


        private static void OnDisplayValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
                 var newMiniDisplayValue = (double)e.NewValue;
                 var miniDigitDisplay = (MiniDigitDisplayUwp)d;
                 miniDigitDisplay.TextBlockDisplayValue.Text = newMiniDisplayValue.ToString(CultureInfo.InvariantCulture);    
        }
        public string MiniDisplayName
        {
            get { return (string)GetValue(MiniDisplayNameProperty); }
            set { SetValue(MiniDisplayNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MiniDisplayName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MiniDisplayNameProperty =
            DependencyProperty.Register("MiniDisplayName", typeof(string), typeof(MiniDigitDisplayUwp), new PropertyMetadata("XXXXXXXXXX",OnDisplayNameChanged));

        private static void OnDisplayNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newMiniDisplayName = e.NewValue.ToString();
            var miniDigitDisplay = (MiniDigitDisplayUwp)d;
            miniDigitDisplay.TextBlockDisplayName.Text = newMiniDisplayName;
        }


        public MiniDigitDisplayUwp()
        {
            this.InitializeComponent();
        }
    }
}
