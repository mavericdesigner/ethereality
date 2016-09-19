using Ethereality.Model;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereality.ViewModel
{
    public class MotorViewModel
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        public MotorViewModel(IDataService dataService, INavigationService navigationService)
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

