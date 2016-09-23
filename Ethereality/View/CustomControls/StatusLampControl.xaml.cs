using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Isis
{
    /// <summary>
    /// Interaction logic for StatusLampControl.xaml
    /// </summary>
    public partial class StatusLampControl : UserControl
    {
        #region ErrorMessage

        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ErrorMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register("ErrorMessage", typeof(string), typeof(StatusLampControl), new PropertyMetadata("Add Error Message", OnErrorMessageChanged));

        #endregion ErrorMessage

        public bool ErrorState
        {
            get { return (bool)GetValue(ErrorStateProperty); }
            set { SetValue(ErrorStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ErrorState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorStateProperty =
            DependencyProperty.Register("ErrorState", typeof(bool), typeof(StatusLampControl), new PropertyMetadata(false, OnErrorStatusChanged));

        private static void OnErrorStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool newErrorState = (bool)e.NewValue;
            StatusLampControl statusLamp = (StatusLampControl)d;
            switch (newErrorState)
            {
                case true:
                    statusLamp.ErrorBlock.Fill = Brushes.Red;
                    break;

                case false:
                    statusLamp.ErrorBlock.Fill = Brushes.Lime;
                    break;

                default:
                    statusLamp.ErrorBlock.Fill = Brushes.Transparent;
                    break;
            }
        }

        private static void OnErrorMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string newErrorMessage = (string)e.NewValue;
            StatusLampControl statusLamp = (StatusLampControl)d;

            statusLamp.ErrorMessageBox.Text = newErrorMessage.ToString();
        }

        public StatusLampControl()
        {
            this.InitializeComponent();
        }
    }
}