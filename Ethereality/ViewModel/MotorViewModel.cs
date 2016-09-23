using Ethereality.DataManagement;
using Ethereality.DataModels.DriveSystem;
using Ethereality.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;

using System.Threading.Tasks;

namespace Ethereality.ViewModel
{
    public class MotorViewModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="MotorTelemetry" /> property's name.
        /// </summary>
        public const string MotorTelemetryPropertyName = "MotorTelemetry";

        private MotorModel _motorTelemetry = new MotorModel();

        /// <summary>
        /// Sets and gets the MotorTelemetry property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public MotorModel MotorTelemetry
        {
            get
            {
                return _motorTelemetry;
            }

            set
            {
                if (_motorTelemetry == value)
                {
                    return;
                }

                _motorTelemetry = value;
                RaisePropertyChanged(() => MotorTelemetry);
            }
        }

        private readonly IDataManager _dataManager;
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;

        public MotorViewModel(IDataService dataService, INavigationService navigationService, IDataManager dataManager)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            _dataManager = dataManager;
            Initialize().Wait();
        }

        private async Task Initialize()
        {
            try
            {
                var item = await _dataService.GetData();
                var telemetry = await _dataManager.GetTelemetryData();
                MotorTelemetry = (MotorModel)telemetry.MotorWord;
            }
            catch (Exception ex)
            {
                // Report error here
            }
        }
    }
}