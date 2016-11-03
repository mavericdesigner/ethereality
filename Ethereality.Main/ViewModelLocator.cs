using Ethereality.DataManagement;
using Ethereality.ViewModels;
using Ethereality.ViewModels.Models;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace Ethereality.Main
{
    public class ViewModelLocator
    {
        public const string SecondPageKey = "SecondPage";
        public const string MotorPageKey = "MotorPage";

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = CreateNavigationService();
            

            SimpleIoc.Default.Register(() => nav);

            SimpleIoc.Default.Register<IDialogService, DialogService>();

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, ViewModels.Design.DesignDataService>();
                SimpleIoc.Default.Register<IDataManager, DataManager>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
                SimpleIoc.Default.Register<IDataManager, DataManager>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<BatteryViewModel>();
            SimpleIoc.Default.Register<MpptViewModel>();
            SimpleIoc.Default.Register<MotorViewModel>();
        }
        private static INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(SecondPageKey, typeof(SecondPage));
            navigationService.Configure(MotorPageKey, typeof(MotorPage));
            // navigationService.Configure("key1", typeof(OtherPage1));
            // navigationService.Configure("key2", typeof(OtherPage2));

            return navigationService;
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        /// <summary>
        /// Gets the ViewModelPropertyName property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public BatteryViewModel BatteryView => ServiceLocator.Current.GetInstance<BatteryViewModel>();

        /// <summary>
        /// Gets the ViewModelPropertyName property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MotorViewModel MotorView => ServiceLocator.Current.GetInstance<MotorViewModel>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
           "CA1822:MarkMembersAsStatic",
           Justification = "This non-static member is needed for data binding purposes.")]
        public MpptViewModel MpptView => ServiceLocator.Current.GetInstance<MpptViewModel>();
    }
}