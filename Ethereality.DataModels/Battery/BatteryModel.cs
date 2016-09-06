using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Core;

namespace Ethereality.DataModels.Battery
{
    public class BatteryModel : INotifyPropertyChanged
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

                _bmuHeartSerialProp = value;
                NotifyPropertyChanged(BmuHeartSerialPropPropertyName);
            }
        }

        #endregion BmuHeartBeatSerialNumber

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

                _packSocProp = value;
                NotifyPropertyChanged(PackSocPropPropertyName);
            }
        }

        #endregion PackStateOfCharge

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

                _packBalanceSoc = value;
                NotifyPropertyChanged(PackBalanceSocPropertyName);
            }
        }

        #endregion PackBalanceStateOfCharge

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

                _chargerControlInfo = value;
                NotifyPropertyChanged(ChargerControlInfoPropertyName);
            }
        }

        #endregion ChargerControlInformation

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

                _prechargeStatusProp = value;
                NotifyPropertyChanged(PrechargeStatusPropPropertyName);
            }
        }

        #endregion PreChargeStatus

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

                _minMaxCellVolt = value;
                NotifyPropertyChanged(MinMaxCellVoltPropertyName);
            }
        }

        #endregion MinimumMaximumCellVoltage

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

                _minMaxCellTemp = value;
                NotifyPropertyChanged(MinMaxCellTempPropertyName);
            }
        }

        #endregion MinimumMaximumCellTemperature

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

                _batteryPackVA = value;
                NotifyPropertyChanged(BatteryPackVAPropertyName);
            }
        }

        #endregion BatteryPackVoltageCurrent

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

                _batteryPackStatusProp = value;
                NotifyPropertyChanged(BatteryPackStatusPropPropertyName);
            }
        }

        #endregion BatteryPackStatus

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

                _batteryPackFanStatusProp = value;
                NotifyPropertyChanged(BatteryPackFanStatusPropPropertyName);
            }
        }

        #endregion BatteryPackFanStatus

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

                _batteryPackStatusExt = value;
                NotifyPropertyChanged(BatteryPackStatusExtPropertyName);
            }
        }

        #endregion BatteryPackStatusExtended

        public event PropertyChangedEventHandler PropertyChanged;

        public void BatteryDataConnect()
        {
            PackSocProp = new PackStateOfCharge();
            _packSocProp.SocAmpHours = 17;
            _packSocProp.SocPercentage = 50;
            PackSocProp = _packSocProp;
        }

        private async void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
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