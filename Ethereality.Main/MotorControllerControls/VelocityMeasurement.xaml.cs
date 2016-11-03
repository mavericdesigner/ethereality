using Windows.UI.Xaml.Controls;
using Ethereality.ViewModels;

namespace Ethereality.Main.MotorControllerControls
{
    /// <summary>
    /// Interaction logic for VelocityMeasurement.xaml
    /// </summary>
    public partial class VelocityMeasurement : UserControl
    {
        MotorViewModel vm => (MotorViewModel)DataContext;
        public VelocityMeasurement()
        {
            this.InitializeComponent();
        }
    }
}