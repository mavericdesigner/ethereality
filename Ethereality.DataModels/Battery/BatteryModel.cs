


namespace Ethereality.DataModels.Battery
{
 
    public class BatteryModel:INotifyPropertyChanged
    {
        #region BmuHeartBeatSerialNumber
        /// <summary>
        /// The <see cref="BmuHeartSerialProp" /> property's name.
        /// </summary>
        public const string BmuHeartSerialPropPropertyName = "BmuHeartSerialProp";

        private BmuHeartBeatSerialNumber _bmuHeartSerialProp = new BmuHeartBeatSerialNumber();

        /// <summary>
        /// Sets and gets the BmuHeartSerialProp property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public BmuHeartBeatSerialNumber BmuHeartSerialProp
        {
            get
            {
                return _bmuHeartSerialProp;
            }

            set
            {
                if (_bmuHeartSerialProp.Equals(value))
                {
                    return;
                }

                NotifyPropertyChanged(BmuHeartSerialPropPropertyName);
                _bmuHeartSerialProp = value;
                NotifyPropertyChanged(BmuHeartSerialPropPropertyName);
            }
        } 
        #endregion

        #region PackStateOfCharge
        /// <summary>
        /// The <see cref="PackSocProp" /> property's name.
        /// </summary>
        public const string PackSocPropPropertyName = "PackSocProp";

        private PackStateOfCharge _packSocProp = new PackStateOfCharge();

        /// <summary>
        /// Sets and gets the PackSocProp property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PackStateOfCharge PackSocProp
        {
            get
            {
                return _packSocProp;
            }

            set
            {
                if (_packSocProp.Equals(value))
                {
                    return;
                }

                NotifyPropertyChanged(PackSocPropPropertyName);
                _packSocProp = value;
                NotifyPropertyChanged(PackSocPropPropertyName);
            }
        } 
        #endregion

        #region PackBalanceStateOfCharge
        /// <summary>
        /// The <see cref="PackBalanceSoc" /> property's name.
        /// </summary>
        public const string PackBalanceSocPropertyName = "PackBalanceSoc";

        private PackBalanceStateOfCharge _packBalanceSoc = new PackBalanceStateOfCharge();

        /// <summary>
        /// Sets and gets the PackBalanceSoc property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PackBalanceStateOfCharge PackBalanceSoc
        {
            get
            {
                return _packBalanceSoc;
            }

            set
            {
                if (_packBalanceSoc.Equals(value))
                {
                    return;
                }

                NotifyPropertyChanged(PackBalanceSocPropertyName);
                _packBalanceSoc = value;
                NotifyPropertyChanged(PackBalanceSocPropertyName);
            }
        }
        
        #endregion

        #region ChargerControlInformation
        /// <summary>
        /// The <see cref="ChargerControlInfo" /> property's name.
        /// </summary>
        public const string ChargerControlInfoPropertyName = "ChargerControlInfo";

        private ChargerControlInformation _chargerControlInfo = new ChargerControlInformation();

        /// <summary>
        /// Sets and gets the ChargerControlInfo property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ChargerControlInformation ChargerControlInfo
        {
            get
            {
                return _chargerControlInfo;
            }

            set
            {
                if (_chargerControlInfo.Equals(value))
                {
                    return;
                }

                NotifyPropertyChanged(ChargerControlInfoPropertyName);
                _chargerControlInfo = value;
                NotifyPropertyChanged(ChargerControlInfoPropertyName);
            }
        }
        
        #endregion

        #region PreChargeStatus
        /// <summary>
        /// The <see cref="PrechargeStatusProp" /> property's name.
        /// </summary>
        public const string PrechargeStatusPropPropertyName = "PrechargeStatusProp";

        private PrechargeStatus _prechargeStatusProp = new PrechargeStatus();

        /// <summary>
        /// Sets and gets the PrechargeStatusProp property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PrechargeStatus PrechargeStatusProp
        {
            get
            {
                return _prechargeStatusProp;
            }

            set
            {
                if (_prechargeStatusProp.Equals(value))
                {
                    return;
                }

                NotifyPropertyChanged(PrechargeStatusPropPropertyName);
                _prechargeStatusProp = value;
                NotifyPropertyChanged(PrechargeStatusPropPropertyName);
            }
        } 
        #endregion

        #region MinimumMaximumCellVoltage
        /// <summary>
        /// The <see cref="MinMaxCellVolt" /> property's name.
        /// </summary>
        public const string MinMaxCellVoltPropertyName = "MinMaxCellVolt";

        private MinimumMaximumCellVoltage _minMaxCellVolt = new MinimumMaximumCellVoltage();

        /// <summary>
        /// Sets and gets the MinMaxCellVolt property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public MinimumMaximumCellVoltage MinMaxCellVolt
        {
            get
            {
                return _minMaxCellVolt;
            }

            set
            {
                if (_minMaxCellVolt.Equals(value))
                {
                    return;
                }

                NotifyPropertyChanged(MinMaxCellVoltPropertyName);
                _minMaxCellVolt = value;
                NotifyPropertyChanged(MinMaxCellVoltPropertyName);
            }
        }
        
        #endregion

        #region MinimumMaximumCellTemperature
        /// <summary>
        /// The <see cref="MinMaxCellTemp" /> property's name.
        /// </summary>
        public const string MinMaxCellTempPropertyName = "MinMaxCellTemp";

        private MinimumMaximumCellTemperature _minMaxCellTemp = new MinimumMaximumCellTemperature();

        /// <summary>
        /// Sets and gets the MinMaxCellTemp property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public MinimumMaximumCellTemperature MinMaxCellTemp
        {
            get
            {
                return _minMaxCellTemp;
            }

            set
            {
                if (_minMaxCellTemp.Equals(value))
                {
                    return;
                }

                NotifyPropertyChanged(MinMaxCellTempPropertyName);
                _minMaxCellTemp = value;
                NotifyPropertyChanged(MinMaxCellTempPropertyName);
            }
        }
        #endregion

        #region BatteryPackVoltageCurrent
        /// <summary>
        /// The <see cref="BatteryPackVA" /> property's name.
        /// </summary>
        public const string BatteryPackVAPropertyName = "BatteryPackVA";

        private BatteryPackVoltageCurrent _batteryPackVA = new BatteryPackVoltageCurrent();

        /// <summary>
        /// Sets and gets the BatteryPackVA property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public BatteryPackVoltageCurrent BatteryPackVA
        {
            get
            {
                return _batteryPackVA;
            }

            set
            {
                if (_batteryPackVA.Equals(value))
                {
                    return;
                }

                NotifyPropertyChanged(BatteryPackVAPropertyName);
                _batteryPackVA = value;
                NotifyPropertyChanged(BatteryPackVAPropertyName);
            }
        }
        
        #endregion

        #region BatteryPackStatus
        /// <summary>
        /// The <see cref="BatteryPackStatusProp" /> property's name.
        /// </summary>
        public const string BatteryPackStatusPropPropertyName = "BatteryPackStatusProp";

        private ExtendedBatteryPackStatus _batteryPackStatusProp = new ExtendedBatteryPackStatus();

        /// <summary>
        /// Sets and gets the BatteryPackStatusProp property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ExtendedBatteryPackStatus BatteryPackStatusProp
        {
            get
            {
                return _batteryPackStatusProp;
            }

            set
            {
                if (_batteryPackStatusProp.Equals(value))
                {
                    return;
                }

                NotifyPropertyChanged(BatteryPackStatusPropPropertyName);
                _batteryPackStatusProp = value;
                NotifyPropertyChanged(BatteryPackStatusPropPropertyName);
            }
        }
        #endregion

        #region BatteryPackFanStatus
        /// <summary>
        /// The <see cref="BatteryPackFanStatusProp" /> property's name.
        /// </summary>
        public const string BatteryPackFanStatusPropPropertyName = "BatteryPackFanStatusProp";

        private BatteryPackFanStatus _batteryPackFanStatusProp = new BatteryPackFanStatus();

        /// <summary>
        /// Sets and gets the BatteryPackFanStatusProp property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public BatteryPackFanStatus BatteryPackFanStatusProp
        {
            get
            {
                return _batteryPackFanStatusProp;
            }

            set
            {
                if (_batteryPackFanStatusProp.Equals(value))
                {
                    return;
                }

                NotifyPropertyChanged(BatteryPackFanStatusPropPropertyName);
                _batteryPackFanStatusProp = value;
                NotifyPropertyChanged(BatteryPackFanStatusPropPropertyName);
            }
        }
        #endregion

        #region BatteryPackStatusExtended
        /// <summary>
        /// The <see cref="BatteryPackStatusExt" /> property's name.
        /// </summary>
        public const string BatteryPackStatusExtPropertyName = "BatteryPackStatusExt";

        private ExtendedBatteryPackStatus _batteryPackStatusExt = new ExtendedBatteryPackStatus();

        


        /// <summary>
        /// Sets and gets the BatteryPackStatusExt property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ExtendedBatteryPackStatus BatteryPackStatusExt
        {
            get
            {
                return _batteryPackStatusExt;
            }

            set
            {
                if (_batteryPackStatusExt.Equals(value))
                {
                    return;
                }

                NotifyPropertyChanged(BatteryPackStatusExtPropertyName);
                _batteryPackStatusExt = value;
                NotifyPropertyChanged(BatteryPackStatusExtPropertyName);
            }
        }
        #endregion



        public event PropertyChangedEventHandler PropertyChanged;


        public void BatteryDataConnect()
        {
            PackSocProp = new PackStateOfCharge();
            _packSocProp.SocAmpHours = 17;
            _packSocProp.SocPercentage = 50;
            PackSocProp = _packSocProp;


        }
        private async void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                await
                   Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () => PropertyChanged(this, new PropertyChangedEventArgs(propertyName)));
            }
        }


    }

}
