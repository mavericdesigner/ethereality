using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Ethereality.Model;
using GalaSoft.MvvmLight.Views;
using Ethereality.DataModels.Battery;

namespace Ethereality.ViewModels
{
    public class BatteryViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;

        /// <summary>
        /// The <see cref="BatteryTelemetry" /> property's name.
        /// </summary>
        public const string BatteryTelemetryPropertyName = "BatteryTelemetry";

        private BmuModel _batteryTelemetry;

        /// <summary>
        /// Sets and gets the BatteryTelemetry property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public BmuModel BatteryTelemetry
        {
            get
            {
                return _batteryTelemetry;
            }

            set
            {
                if (_batteryTelemetry == value)
                {
                    return;
                }

                _batteryTelemetry = value;
                RaisePropertyChanged(() => BatteryTelemetry);
            }
        }
        public BatteryViewModel(IDataService dataService,INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            Initialize().Wait();
        }

        private async Task Initialize()
        {
            try
            {
                var item = await _dataService.GetData();   
            }
            catch (Exception ex)
            {
                // Report error here
              
            }
        }
    }
       
        
}
