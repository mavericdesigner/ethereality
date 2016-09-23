using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Ethereality.CustomControls
{
    public sealed partial class CMUUserControl : UserControl
    {
        public string CmuNumber
        {
            get { return (string)GetValue(CmuNumberProperty); }
            set { SetValue(CmuNumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CmuNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CmuNumberProperty =
            DependencyProperty.Register("CmuNumber", typeof(string), typeof(CMUUserControl), new PropertyMetadata("CMU#", OnCmuNumberChanged));

        public double PCBTemp
        {
            get { return (double)GetValue(PCBTempProperty); }
            set { SetValue(PCBTempProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PCBTemp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PCBTempProperty =
            DependencyProperty.Register("PCBTemp", typeof(double), typeof(CMUUserControl), new PropertyMetadata(0.0, OnPCBTempChanged));

        public double CellTemp
        {
            get { return (double)GetValue(CellTempProperty); }
            set { SetValue(CellTempProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CellTemp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CellTempProperty =
            DependencyProperty.Register("CellTemp", typeof(double), typeof(CMUUserControl), new PropertyMetadata(0.0, OnCellTempChanged));

        public Int32 CmuSerial
        {
            get { return (Int32)GetValue(CmuSerialProperty); }
            set { SetValue(CmuSerialProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CmuSerial.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CmuSerialProperty =
            DependencyProperty.Register("CmuSerial", typeof(Int32), typeof(CMUUserControl), new PropertyMetadata(new Int32(), OnCmuSerialChanged));

        private static void OnCmuSerialChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Int32 newCmuSerial = (Int32)e.NewValue;
            CMUUserControl cmucontrol = (CMUUserControl)d;

            cmucontrol.CmuSerialNumber.Text = newCmuSerial.ToString();
        }

        public CMUUserControl()
        {
            this.InitializeComponent();
        }

        private static void OnPCBTempChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            double newTemp = (double)e.NewValue / 10;
            CMUUserControl cmucontrol = (CMUUserControl)d;
            cmucontrol.UIPCBTemp.Text = newTemp.ToString();
        }

        private static void OnCellTempChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            double newTemp = (double)e.NewValue / 10;
            CMUUserControl cmucontrol = (CMUUserControl)d;
            cmucontrol.UICellTemp.Text = newTemp.ToString();
        }

        private static void OnCmuNumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string newCmuNumber = (string)e.NewValue;
            CMUUserControl cmucontrol = (CMUUserControl)d;
            cmucontrol.NameCMU.Text = "CMU" + newCmuNumber;
        }

        #region Cell0

        public Int16 Cell0
        {
            get { return (Int16)GetValue(Cell0Property); }
            set { SetValue(Cell0Property, value); }
        }

        // Using a DependencyProperty as the backing store for Cell0.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Cell0Property =
            DependencyProperty.Register("Cell0", typeof(Int16), typeof(CMUUserControl), new PropertyMetadata(new Int16(), OnCell0PropChanged));

        private static void OnCell0PropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Int16 newCellVoltages = (Int16)e.NewValue;
            CMUUserControl cmucontrol = (CMUUserControl)d;

            cmucontrol.CellBar0.CellVoltage = newCellVoltages;
        }

        #endregion Cell0

        #region Cell1

        public Int16 Cell1
        {
            get { return (Int16)GetValue(Cell1Property); }
            set { SetValue(Cell1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Cell1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Cell1Property =
            DependencyProperty.Register("Cell1", typeof(Int16), typeof(CMUUserControl), new PropertyMetadata(new Int16(), OnCell1PropChanged));

        private static void OnCell1PropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Int16 newCellVoltages = (Int16)e.NewValue;
            CMUUserControl cmucontrol = (CMUUserControl)d;

            cmucontrol.CellBar1.CellVoltage = newCellVoltages;
        }

        #endregion Cell1

        #region Cell2

        public Int16 Cell2
        {
            get { return (Int16)GetValue(Cell2Property); }
            set { SetValue(Cell2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Cell1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Cell2Property =
            DependencyProperty.Register("Cell2", typeof(Int16), typeof(CMUUserControl), new PropertyMetadata(new Int16(), OnCell2PropChanged));

        private static void OnCell2PropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Int16 newCellVoltages = (Int16)e.NewValue;
            CMUUserControl cmucontrol = (CMUUserControl)d;

            cmucontrol.CellBar2.CellVoltage = newCellVoltages;
        }

        #endregion Cell2

        #region Cell3

        public Int16 Cell3
        {
            get { return (Int16)GetValue(Cell3Property); }
            set { SetValue(Cell3Property, value); }
        }

        // Using a DependencyProperty as the backing store for Cell1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Cell3Property =
            DependencyProperty.Register("Cell3", typeof(Int16), typeof(CMUUserControl), new PropertyMetadata(new Int16(), OnCell3PropChanged));

        private static void OnCell3PropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Int16 newCellVoltages = (Int16)e.NewValue;
            CMUUserControl cmucontrol = (CMUUserControl)d;

            cmucontrol.CellBar3.CellVoltage = newCellVoltages;
        }

        #endregion Cell3

        #region Cell4

        public Int16 Cell4
        {
            get { return (Int16)GetValue(Cell4Property); }
            set { SetValue(Cell4Property, value); }
        }

        // Using a DependencyProperty as the backing store for Cell1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Cell4Property =
            DependencyProperty.Register("Cell4", typeof(Int16), typeof(CMUUserControl), new PropertyMetadata(new Int16(), OnCell4PropChanged));

        private static void OnCell4PropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Int16 newCellVoltages = (Int16)e.NewValue;
            CMUUserControl cmucontrol = (CMUUserControl)d;

            cmucontrol.CellBar4.CellVoltage = newCellVoltages;
        }

        #endregion Cell4

        #region Cell5

        public Int16 Cell5
        {
            get { return (Int16)GetValue(Cell5Property); }
            set { SetValue(Cell5Property, value); }
        }

        // Using a DependencyProperty as the backing store for Cell1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Cell5Property =
            DependencyProperty.Register("Cell5", typeof(Int16), typeof(CMUUserControl), new PropertyMetadata(new Int16(), OnCell5PropChanged));

        private static void OnCell5PropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Int16 newCellVoltages = (Int16)e.NewValue;
            CMUUserControl cmucontrol = (CMUUserControl)d;

            cmucontrol.CellBar5.CellVoltage = newCellVoltages;
        }

        #endregion Cell5

        #region Cell6

        public Int16 Cell6
        {
            get { return (Int16)GetValue(Cell6Property); }
            set { SetValue(Cell6Property, value); }
        }

        // Using a DependencyProperty as the backing store for Cell1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Cell6Property =
            DependencyProperty.Register("Cell6", typeof(Int16), typeof(CMUUserControl), new PropertyMetadata(new Int16(), OnCell6PropChanged));

        private static void OnCell6PropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Int16 newCellVoltages = (Int16)e.NewValue;
            CMUUserControl cmucontrol = (CMUUserControl)d;

            cmucontrol.CellBar6.CellVoltage = newCellVoltages;
        }

        #endregion Cell6

        #region Cell7

        public Int16 Cell7
        {
            get { return (Int16)GetValue(Cell7Property); }
            set { SetValue(Cell7Property, value); }
        }

        // Using a DependencyProperty as the backing store for Cell1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Cell7Property =
            DependencyProperty.Register("Cell7", typeof(Int16), typeof(CMUUserControl), new PropertyMetadata(new Int16(), OnCell7PropChanged));

        private static void OnCell7PropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Int16 newCellVoltages = (Int16)e.NewValue;
            CMUUserControl cmucontrol = (CMUUserControl)d;

            cmucontrol.CellBar7.CellVoltage = newCellVoltages;
        }

        #endregion Cell7
    }
}