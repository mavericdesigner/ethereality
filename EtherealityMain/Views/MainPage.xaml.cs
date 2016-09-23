using System;
using EtherealityMain.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Windows.UI.Core;
using Ethereality.ViewModels;

namespace EtherealityMain.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel Vm => (MainViewModel)DataContext;
        public MainPage()
        {
       
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManagerBackRequested;
            Loaded += (s, e) =>
            {
                Vm.RunClock();
            };
        }

    
          
        

            private void SystemNavigationManagerBackRequested(object sender, BackRequestedEventArgs e)
            {
                if (Frame.CanGoBack)
                {
                    e.Handled = true;
                    Frame.GoBack();
                }
            }

            protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
            {
                Vm.StopClock();
                base.OnNavigatingFrom(e);
            }
        }
    }
}
