using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Core;

namespace Ethereality.DataModels.Battery
{
    public struct BmuHeartBeatSerialNumber
    {
        public UInt32 BmuHeartbeat { get; set; }

        public UInt32 BmuSerialNumber { get; set; }
    }

    //pack state of charge in Ah and %SOC
    public struct PackStateOfCharge
    {
        public float SocAmpHours { get; set; }

        public float SocPercentage { get; set; }
    }

    //pack balance state of charge in Ah and %SOC
    public struct PackBalanceStateOfCharge
    {
        public float BalanceSocAmpHours { get; set; }

        public float BalanceSocPercentage { get; set; }
    }

    //Charging Cell Voltage Error
    //Cell Temperature Margin
    //Discharge Cell Voltage Error
    //Total Pack Capacity
    public struct ChargerControlInformation
    {
        public Int16 ChargingCellVoltageError { get; set; }

        public Int16 CellTemperatureMargin { get; set; }

        public Int16 DischargingCellVoltageError { get; set; }

        public Int16 TotalPackCapacity { get; set; }
    }

    public struct PrechargeStatus
    {
        public byte PrechargeContactorDriverStatus { get; set; }

        public byte PrechargeStates { get; set; }

        public UInt16 ContactorSupplyVoltage12V { get; set; }

        public UInt16 Unused { get; set; }

        public byte PrechargeTimerState { get; set; }

        public byte PrechargeTimerCounter { get; set; }
    }

    public struct MinimumMaximumCellVoltage
    {
        public UInt16 MinimumCellVoltage { get; set; }

        public UInt16 MaximumCellVoltage { get; set; }

        public byte CmuNumberMinCellVoltage { get; set; }

        public byte CellInCmuMinCellVoltage { get; set; }

        public byte CmuNumberMaxCellVoltage { get; set; }

        public byte CellInCmuMaxCellVoltage { get; set; }
    }

    public struct MinimumMaximumCellTemperature
    {
        public UInt16 MinimumCellTemperature { get; set; }

        public UInt16 MaximumCellTemperature { get; set; }

        public byte CmuNumberMinCellTemperature { get; set; }

        public byte Unused0 { get; set; }

        public byte CmuNumberMaxCellTemperature { get; set; }

        public byte Unused1 { get; set; }
    }

    public struct BatteryPackVoltageCurrent
    {
        public UInt32 BatteryVoltage { get; set; }//mV

        public Int32 BatteryCurrent { get; set; }//mA
    }

    public struct BatteryPackStatus
    {
        public UInt16 BalanceVoltageThresholdRising { get; set; } //resistor turns on

        public UInt16 BalanceVoltageThresholdFalling { get; set; }//resistor turns off

        public byte BatteryPackStatusFlags { get; set; }

        public byte BmsCmuCount { get; set; }

        public UInt16 BmuFirmwareBuiltNumber { get; set; }
    }

    public struct BatteryPackFanStatus
    {
        public UInt16 SpeedFan0 { get; set; }//RPM

        public UInt16 SpeedFan1 { get; set; }//RPM

        public UInt16 CurrentConsumption12vFanPlusContactor { get; set; }//mA

        public UInt16 CurrentConsumption12vCmu { get; set; }//mA
    }

    public struct ExtendedBatteryPackStatus
    {
        public UInt32 StatusFlags { get; set; }

        public byte BmuHardwareVersion { get; set; }

        public byte BmuModelID { get; set; }
    }

    #region PrechargeStatusFlags

    [Flags]
    public enum PrechargeContactor
    {
        ErrorStatusContactor1Driver = 0x01,
        ErrorStatusContactor2Driver = 0x02,
        OutputStatusOfContactor1Driver = 0x04,
        OutputStatusOfContactor2Driver = 0x08,
        Contactor12vSupplyVoltageOk = 0x10,
        ErrorStatusOfContactor3Driver = 0x20,
        OutputStatusOfContactor3Driver = 0x40,
        Unused = 0x80
    }

    [Flags]
    public enum PrechargeState
    {
        Error = 0,
        Idle = 1,
        EnablePack = 5,
        Measure = 2,
        Precharge = 3,
        Run = 4
    }

    [Flags]
    public enum PrechargeTimer
    {
        Elapsed = 0x01,
        NotElapsed = 0x00
    }

    #endregion PrechargeStatusFlags

    #region BatteryPackStatusFlags

    [Flags]
    public enum BatteryFlags
    {
        CellOverVoltage = 0x01,
        CellUnderVoltage = 0x02,
        CellOverTemperature = 0x04,
        MeasurementUntrusted = 0x08,
        CmuCommTimeOut = 0x10,
        VehicleCommunicationTimeout = 0x20,
        BmuSetupMode = 0x40,
        CmuCanBusPowerStatus = 0x80,
    }

    #endregion BatteryPackStatusFlags

    #region ExtendedBatteryPacksStatusFlags

    [Flags]
    public enum ExtendedBatteryFlags
    {
        CellOverVoltage = 0x00000001,
        CellUnderVoltage = 0x00000002,
        CellOverTemperature = 0x00000004,
        MeasurementUntrusted = 0x00000008,
        CmuCommTimeOut = 0x00000010,
        VehicleCommTimeOut = 0x00000020,
        BmuInSetupMode = 0x00000040,
        CmuCanBusPowerStatus = 0x00000080,
        PackIsolationTestfailure = 0x00000100,
        SocMeasurementIsNotValid = 0x00000200,
        Can12VSupplyLow = 0x00000400,
        ContactorStuckOrNotEngaged = 0x00000800,
        CmuDetectedExtraCellPresent = 0x00001000
    }

    #endregion ExtendedBatteryPacksStatusFlags

    #region EvDriverControlsSwitchPositionFlags

    [Flags]
    public enum IgnitionStatus
    {
        IgnitionRun = 0x0020,
        IgnitionStart = 0x0040
    }

    #endregion EvDriverControlsSwitchPositionFlags

    public enum BatteryPackMsgAddress
    {
        BmsID = 0x00,
        PackSoc = 0xF4,
        PackBalanceStateofCharge = 0xF5,
        ChargeControlInformation = 0xF6,
        PrechargeStatus = 0xF7,
        MinMaxCellVoltage = 0xF8,
        MinMaxCellTemperature = 0xF9,
        BatteryPackVoltageCurrent = 0xFA,
        BatteryPackStatus = 0xFB,
        BatterPackFanStatus = 0xFC,
        ExtendedBatteryPackStatus = 0xFD,
    }

    public class BmuModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region BmuHeartBeatSerialNumber

        /// <summary>
        /// The <see cref="BmuHeartSerialProp" /> property's name.
        /// </summary>
        public const string BmuHeartSerialPropPropertyName = "BmuHeartSerialProp";

        private BmuHeartBeatSerialNumber _bmuHeartSerialProp;

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

        private PackStateOfCharge _packSocProp;

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

        private BatteryPackStatus _batteryPackStatusProp = new BatteryPackStatus();

        /// <summary>
        /// Sets and gets the BatteryPackStatusProp property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public BatteryPackStatus BatteryPackStatusProp
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

        #region ContactorFlagBits

        /// <summary>
        /// The <see cref="ContactorFlagBits" /> property's name.
        /// </summary>
        public const string ContactorFlagBitsPropertyName = "ContactorFlagBits";

        private BitArray _contactorFlagBits;

        /// <summary>
        /// Sets and gets the ContactorFlagBits property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public BitArray ContactorFlagBits
        {
            get
            {
                return _contactorFlagBits;
            }

            set
            {
                if (_contactorFlagBits == value)
                {
                    return;
                }

                _contactorFlagBits = value;
                NotifyPropertyChanged(ContactorFlagBitsPropertyName);
            }
        }

        #endregion ContactorFlagBits

        public BmuModel()
        {
            _bmuHeartSerialProp = new BmuHeartBeatSerialNumber();
            _packSocProp = new PackStateOfCharge();
            _packBalanceSoc = new PackBalanceStateOfCharge();
            _chargerControlInfo = new ChargerControlInformation();
            _prechargeStatusProp = new PrechargeStatus();
            _minMaxCellVolt = new MinimumMaximumCellVoltage();
            _minMaxCellTemp = new MinimumMaximumCellTemperature();
            _batteryPackVA = new BatteryPackVoltageCurrent();
            _batteryPackStatusProp = new BatteryPackStatus();
            _batteryPackFanStatusProp = new BatteryPackFanStatus();
            _batteryPackStatusExt = new ExtendedBatteryPackStatus();
            _contactorFlagBits = new BitArray(8);
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