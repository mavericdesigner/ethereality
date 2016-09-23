/*
  In App.xaml:
  <Application.Resources>
      <vm:PlotViewModelLocator xmlns:vm="clr-namespace:Isis.CustomControls.Analytics"
                                   x:Key="PlotLocator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource PlotLocator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Isis.CustomControls.Analytics
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PlotViewModelLocator
    {
        static PlotViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            }
            else
            {
                // SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<PlotViewModel>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public PlotViewModel PlotVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlotViewModel>();
            }
        }
    }
}