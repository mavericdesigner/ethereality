using Ethereality.DataManagement;
using Ethereality.DataParserMachine;
using Ethereality.ViewModels.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using SerialCommunicationUWP;
using System;
using System.Threading.Tasks;

namespace Ethereality.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public const string ClockPropertyName = "Clock";
        public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private readonly IDataService _dataService;
        private readonly IDataManager _dataManager;
        private readonly INavigationService _navigationService;
        private string _clock = "Starting...";
        private int _counter;
        private RelayCommand _incrementCommand;
        private RelayCommand<string> _navigateCommand;
        private RelayCommand<string> _navigateToMotorCommand;
        private RelayCommand<string> _navigateToBatteryCommand;
        private string _originalTitle;
        private bool _runClock;
        private RelayCommand _sendMessageCommand;
        private RelayCommand _showDialogCommand;
        private string _welcomeTitle = string.Empty;
        private bool init = false;

        /// <summary>
        /// The <see cref="SerialDevice" /> property's name.
        /// </summary>
        public const string SerialDevicePropertyName = "SerialDevice";

        private SerialDeviceItem _serialDevice;

        /// <summary>
        /// Sets and gets the SerialDevice property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public SerialDeviceItem SerialDevice
        {
            get
            {
                return _serialDevice;
            }

            set
            {
                if (_serialDevice == value)
                {
                    return;
                }

                _serialDevice = value;
                RaisePropertyChanged(() => SerialDevice);
            }
        }

        private RelayCommand _readDevice;

        /// <summary>
        /// Gets the ReadDevice.
        /// </summary>
        public RelayCommand ReadDevice
        {
            get
            {
                return _readDevice
                    ?? (_readDevice = new RelayCommand(
                    async () =>
                    {
                        if (!init)
                        {
                            SerialDevice.ReadWriteHandle.Initialize();
                            init = true;
                        }
                        await _dataManager.SerialPolling();
                    },
                    () => true));
            }
        }

        private RelayCommand _connectDevice;

        /// <summary>
        /// Gets the ConnectDevice.
        /// </summary>
        public RelayCommand ConnectDevice
        {
            get
            {
                return _connectDevice
                    ?? (_connectDevice = new RelayCommand(
                    () =>
                    {
                        SerialDevice.ConnectDisconnectHandle.ConnectToDevice_Click();
                    },
                    () => true));
            }
        }

        private RelayCommand _disconnectDevice;

        /// <summary>
        /// Gets the DisconnectDevice.
        /// </summary>
        public RelayCommand DisconnectDevice
        {
            get
            {
                return _disconnectDevice
                    ?? (_disconnectDevice = new RelayCommand(
                    () =>
                    {
                        SerialDevice.ConnectDisconnectHandle.DisconnectFromDevice_Click();
                    },
                    () => true));
            }
        }

        public string Clock
        {
            get
            {
                return _clock;
            }
            set
            {
                Set(ClockPropertyName, ref _clock, value);
            }
        }

        public RelayCommand IncrementCommand
        {
            get
            {
                return _incrementCommand
                    ?? (_incrementCommand = new RelayCommand(
                    () =>
                    {
                        WelcomeTitle = string.Format("Counter clicked {0} times", ++_counter);
                    }));
            }
        }

        public RelayCommand<string> NavigateCommand
        {
            get
            {
                return _navigateCommand
                       ?? (_navigateCommand = new RelayCommand<string>(
                           p => _navigationService.NavigateTo("SecondPage", p),
                           p => !string.IsNullOrEmpty(p)));
            }
        }

        public RelayCommand<string> NavigateToMotorCommand
        {
            get
            {
                return _navigateToMotorCommand
                       ?? (_navigateToMotorCommand = new RelayCommand<string>(
                           p => _navigationService.NavigateTo("MotorPage", p),
                           p => !string.IsNullOrEmpty(p)));
            }
        }
        public RelayCommand<string> NavigateToBatteryCommand
        {
            get
            {
                return _navigateCommand
                       ?? (_navigateCommand = new RelayCommand<string>(
                           p => _navigationService.NavigateTo("BatteryPage", p),
                           p => !string.IsNullOrEmpty(p)));
            }
        }

        public RelayCommand SendMessageCommand
        {
            get
            {
                return _sendMessageCommand
                    ?? (_sendMessageCommand = new RelayCommand(
                    () =>
                    {
                        Messenger.Default.Send(
                            new NotificationMessageAction<string>(
                                "Testing",
                                reply =>
                                {
                                    WelcomeTitle = reply;
                                }));
                    }));
            }
        }

        public RelayCommand ShowDialogCommand
        {
            get
            {
                return _showDialogCommand
                       ?? (_showDialogCommand = new RelayCommand(
                           async () =>
                           {
                               var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
                               await dialog.ShowMessage("Hello Universal Application", "it works...");
                           }));
            }
        }

        /// <summary>
        /// The <see cref="DataParserMachine" /> property's name.
        /// </summary>
        public const string DataParserMachinePropertyName = "DataParserMachine";

        private MainDataParsingService _dataParserMachine;

        /// <summary>
        /// Sets and gets the DataParserMachine property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public MainDataParsingService DataParserMachine
        {
            get
            {
                return _dataParserMachine;
            }

            set
            {
                if (_dataParserMachine == value)
                {
                    return;
                }

                _dataParserMachine = value;
                RaisePropertyChanged(() => DataParserMachine);
            }
        }

        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }

            set
            {
                Set(ref _welcomeTitle, value);
            }
        }

        public MainViewModel(
            IDataService dataService,
            INavigationService navigationService, IDataManager
             dataManager)
        {
            _dataManager = dataManager;
            _dataService = dataService;
            _navigationService = navigationService;
            Initialize().Wait();
        }

        public void RunClock()
        {
            _runClock = true;

            Task.Run(async () =>
            {
                while (_runClock)
                {
                    try
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            Clock = DateTime.Now.ToString("HH:mm:ss");
                        });

                        await Task.Delay(1000);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            });
        }

        public void StopClock()
        {
            _runClock = false;
        }

        private async Task Initialize()
        {
            try
            {
                //  var item = await _dataService.GetData();
                SerialDevice = await _dataManager.GetSerial();
                DataParserMachine = await _dataManager.GetTelemetryData();
                //     _originalTitle = item.Title;
                //  WelcomeTitle = item.Title;
            }
            catch (Exception ex)
            {
                // Report error here
                WelcomeTitle = ex.Message;
            }
        }
    }
}