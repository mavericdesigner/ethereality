using Ethereality.DataModels.MPPT;
using Ethereality.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Threading.Tasks;

namespace Ethereality.ViewModel
{
    public class MpptViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;

        /// <summary>
        /// The <see cref="MpptTelemetry" /> property's name.
        /// </summary>
        public const string MpptTelemetryPropertyName = "MpptTelemetry";

        private MpptModel _mpptTelemetry;

        /// <summary>
        /// Sets and gets the MpptTelemetry property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public MpptModel MpptTelemetry
        {
            get
            {
                return _mpptTelemetry;
            }

            set
            {
                if (_mpptTelemetry == value)
                {
                    return;
                }

                _mpptTelemetry = value;
                RaisePropertyChanged(() => MpptTelemetry);
            }
        }

        public MpptViewModel(IDataService dataService, INavigationService navigationService)
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