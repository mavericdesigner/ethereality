using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Ethereality.Model;
using GalaSoft.MvvmLight.Views;

namespace Ethereality.ViewModels
{
    public class BatteryViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
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
