using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Ethereality.UserControls
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
                    statusLamp.ErrorBlock.Fill =new SolidColorBrush(Colors.Red);
                    break;

                case false:
                    statusLamp.ErrorBlock.Fill = new SolidColorBrush(Colors.Lime);
                    break;

                default:
                    statusLamp.ErrorBlock.Fill = new SolidColorBrush(Colors.Transparent);
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