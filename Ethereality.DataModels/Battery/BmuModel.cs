using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Core;

namespace Ethereality.DataModels.Battery
{
    
    public struct CmuStatus
    {
        public UInt32 SerialNumber { get; set; }

        public Int16 PcbTemp { get; set; }

        public Int16 CellTemp { get; set; }
    }

    public struct Cell0to3
    {
        public Int16 Cell0 { get; set; }

        public Int16 Cell1 { get; set; }

        public Int16 Cell2 { get; set; }

        public Int16 Cell3 { get; set; }
    }

    public struct Cell4to7
    {
        public Int16 Cell4 { get; set; }

        public Int16 Cell5 { get; set; }

        public Int16 Cell6 { get; set; }

        public Int16 Cell7 { get; set; }
    }
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
        BmsID = 0x600,
        Cmu1Status = 0x601,
        Cmu1Volt0 = 0x602,
        Cmu1Volt1 = 0x603,

        Cmu2Status = 0x604,
        Cmu2Volt0 = 0x605,
        Cmu2Volt1 = 0x606,

        Cmu3Status = 0x607,
        Cmu3Volt0 = 0x608,
        Cmu3Volt1 = 0x609,

        Cmu4Status = 0x60A,
        Cmu4Volt0 = 0x60B,
        Cmu4Volt1 = 0x60C,

        Cmu5Status = 0x60D,
        Cmu5Volt0 = 0x60E,
        Cmu5Volt1 = 0x60F,

        PackSoc = 0x6F4,
        PackBalanceStateofCharge = 0x6F5,
        ChargeControlInformation = 0x6F6,
        PrechargeStatus = 0x6F7,
        MinMaxCellVoltage = 0x6F8,
        MinMaxCellTemperature = 0x6F9,
        BatteryPackVoltageCurrent = 0x6FA,
        BatteryPackStatus = 0x6FB,
        BatterPackFanStatus = 0x6FC,
        ExtendedBatteryPackStatus = 0x6FD,
    }

    public class BmuModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static BmuModel CurrentBmu;
        #region Cmu1Status

        /// <summary>
        /// The <see cref="Cmu1Status" /> property's name.
        /// </summary>
        public const string Cmu1StatusPropertyName = "Cmu1Status";

        private CmuStatus _cmu1Status;

        /// <summary>
        /// Sets and gets the Cmu1 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public CmuStatus Cmu1Status
        {
            get
            {
                return _cmu1Status;
            }

            set
            {
                if (_cmu1Status.Equals(value))
                {
                    return;
                }

                _cmu1Status = value;
                NotifyPropertyChanged(Cmu1StatusPropertyName);
            }
        }

        #endregion Cmu1Status

        #region Cmu2Status

        /// <summary>
        /// The <see cref="Cmu2" /> property's name.
        /// </summary>
        public const string Cmu2PropertyName = "Cmu2Status";

        private CmuStatus _cmu2Status;

        /// <summary>
        /// Sets and gets the Cmu2 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public CmuStatus Cmu2Status
        {
            get
            {
                return _cmu2Status;
            }

            set
            {
                if (_cmu2Status.Equals(value))
                {
                    return;
                }

                _cmu2Status = value;
                NotifyPropertyChanged(Cmu2PropertyName);
            }
        }

        #endregion Cmu2Status

        #region Cmu3Status

        /// <summary>
        /// The <see cref="Cmu3" /> property's name.
        /// </summary>
        public const string Cmu3PropertyName = "Cmu3Status";

        private CmuStatus _cmu3Status;

        /// <summary>
        /// Sets and gets the Cmu3 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public CmuStatus Cmu3Status
        {
            get
            {
                return _cmu3Status;
            }

            set
            {
                if (_cmu3Status.Equals(value))
                {
                    return;
                }

                _cmu3Status = value;
                NotifyPropertyChanged(Cmu3PropertyName);
            }
        }

        #endregion Cmu3Status

        #region Cmu4Status

        /// <summary>
        /// The <see cref="Cmu4Status" /> property's name.
        /// </summary>
        public const string Cmu4StatusPropertyName = "Cmu4Status";

        private CmuStatus _cmu4Status;

        /// <summary>
        /// Sets and gets the Cmu4 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public CmuStatus Cmu4Status
        {
            get
            {
                return _cmu4Status;
            }

            set
            {
                if (_cmu4Status.Equals(value))
                {
                    return;
                }

                _cmu4Status = value;
                NotifyPropertyChanged(Cmu4StatusPropertyName);
            }
        }

        #endregion Cmu4Status

        #region Cmu5Status

        /// <summary>
        /// The <see cref="Cmu5" /> property's name.
        /// </summary>
        public const string Cmu5PropertyName = "Cmu5Status";

        private CmuStatus _cmu5Status;

        /// <summary>
        /// Sets and gets the Cmu5 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public CmuStatus Cmu5Status
        {
            get
            {
                return _cmu5Status;
            }

            set
            {
                if (_cmu5Status.Equals(value))
                {
                    return;
                }

                _cmu5Status = value;
                NotifyPropertyChanged(Cmu5PropertyName);
            }
        }

        #endregion Cmu5Status

        #region Cmu1_0C3

        /// <summary>
        /// The <see cref="Cmu1_0C3" /> property's name.
        /// </summary>
        public const string Cmu1_0C3PropertyName = "Cmu1_0C3";

        private Cell0to3 _cmu1_0C3;

        /// <summary>
        /// Sets and gets the Cmu1_0C3 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell0to3 Cmu1_0C3
        {
            get
            {
                return _cmu1_0C3;
            }

            set
            {
                if (_cmu1_0C3.Equals(value))
                {
                    return;
                }

                _cmu1_0C3 = value;
                NotifyPropertyChanged(Cmu1_0C3PropertyName);
            }
        }

        #endregion Cmu1_0C3

        #region Cmu2_0C3

        /// <summary>
        /// The <see cref="Cmu2_0C3" /> property's name.
        /// </summary>
        public const string Cmu2_0C3PropertyName = "Cmu2_0C3";

        private Cell0to3 _cmu2_0C3;

        /// <summary>
        /// Sets and gets the Cmu2_0C3 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell0to3 Cmu2_0C3
        {
            get
            {
                return _cmu2_0C3;
            }

            set
            {
                if (_cmu2_0C3.Equals(value))
                {
                    return;
                }

                _cmu2_0C3 = value;
                NotifyPropertyChanged(Cmu2_0C3PropertyName);
            }
        }

        #endregion Cmu2_0C3

        #region Cmu3_0C3

        /// <summary>
        /// The <see cref="Cmu3_0C3" /> property's name.
        /// </summary>
        public const string Cmu3_0C3PropertyName = "Cmu3_0C3";

        private Cell0to3 _cmu3_0C3;

        /// <summary>
        /// Sets and gets the Cmu3_0C3 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell0to3 Cmu3_0C3
        {
            get
            {
                return _cmu3_0C3;
            }

            set
            {
                if (_cmu3_0C3.Equals(value))
                {
                    return;
                }

                _cmu3_0C3 = value;
                NotifyPropertyChanged(Cmu3_0C3PropertyName);
            }
        }

        #endregion Cmu3_0C3

        #region Cmu4_0C3

        /// <summary>
        /// The <see cref="Cmu4_0C3" /> property's name.
        /// </summary>
        public const string Cmu4_0C3PropertyName = "Cmu4_0C3";

        private Cell0to3 _cmu4_0C3;

        /// <summary>
        /// Sets and gets the Cmu4_0C3 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell0to3 Cmu4_0C3
        {
            get
            {
                return _cmu4_0C3;
            }

            set
            {
                if (_cmu4_0C3.Equals(value))
                {
                    return;
                }

                _cmu4_0C3 = value;
                NotifyPropertyChanged(Cmu4_0C3PropertyName);
            }
        }

        #endregion Cmu4_0C3

        #region Cmu5_0C3

        /// <summary>
        /// The <see cref="Cmu5_0C3" /> property's name.
        /// </summary>
        public const string Cmu5_0C3PropertyName = "Cmu5_0C3";

        private Cell0to3 _cmu5_0C3;

        /// <summary>
        /// Sets and gets the Cmu5_0C3 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell0to3 Cmu5_0C3
        {
            get
            {
                return _cmu5_0C3;
            }

            set
            {
                if (_cmu5_0C3.Equals(value))
                {
                    return;
                }

                _cmu5_0C3 = value;
                NotifyPropertyChanged(Cmu5_0C3PropertyName);
            }
        }

        #endregion Cmu5_0C3

        #region Cmu1_4C7

        /// <summary>
        /// The <see cref="Cmu1_4C7" /> property's name.
        /// </summary>
        public const string Cmu1_4C7PropertyName = "Cmu1_4C7";

        private Cell4to7 _cmu1_4C7;

        /// <summary>
        /// Sets and gets the Cmu1_4C7 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell4to7 Cmu1_4C7
        {
            get
            {
                return _cmu1_4C7;
            }

            set
            {
                if (_cmu1_4C7.Equals(value))
                {
                    return;
                }

                _cmu1_4C7 = value;
                NotifyPropertyChanged(Cmu1_4C7PropertyName);
            }
        }

        #endregion Cmu1_4C7

        #region Cmu2_4C7

        /// <summary>
        /// The <see cref="Cmu2_4C7" /> property's name.
        /// </summary>
        public const string Cmu2_4C7PropertyName = "Cmu2_4C7";

        private Cell4to7 _cmu2_4C7;

        /// <summary>
        /// Sets and gets the Cmu2_4C7 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell4to7 Cmu2_4C7
        {
            get
            {
                return _cmu2_4C7;
            }

            set
            {
                if (_cmu2_4C7.Equals(value))
                {
                    return;
                }

                _cmu2_4C7 = value;
                NotifyPropertyChanged(Cmu2_4C7PropertyName);
            }
        }

        #endregion Cmu2_4C7

        #region Cmu3_4C7

        /// <summary>
        /// The <see cref="Cmu3_4C7" /> property's name.
        /// </summary>
        public const string Cmu3_4C7PropertyName = "Cmu3_4C7";

        private Cell4to7 _cmu3_4C7;

        /// <summary>
        /// Sets and gets the Cmu3_4C7 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell4to7 Cmu3_4C7
        {
            get
            {
                return _cmu3_4C7;
            }

            set
            {
                if (_cmu3_4C7.Equals(value))
                {
                    return;
                }

                _cmu3_4C7 = value;
                NotifyPropertyChanged(Cmu3_4C7PropertyName);
            }
        }

        #endregion Cmu3_4C7

        #region Cmu4_4C7

        /// <summary>
        /// The <see cref="Cmu4_4C7" /> property's name.
        /// </summary>
        public const string Cmu4_4C7PropertyName = "Cmu4_4C7";

        private Cell4to7 _cmu4_4C7;

        /// <summary>
        /// Sets and gets the Cmu4_4C7 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell4to7 Cmu4_4C7
        {
            get
            {
                return _cmu4_4C7;
            }

            set
            {
                if (_cmu4_4C7.Equals(value))
                {
                    return;
                }

                _cmu4_4C7 = value;
                NotifyPropertyChanged(Cmu4_4C7PropertyName);
            }
        }

        #endregion Cmu4_4C7

        #region Cmu5_4C7

        /// <summary>
        /// The <see cref="Cmu5_4C7" /> property's name.
        /// </summary>
        public const string Cmu5_4C7PropertyName = "Cmu5_4C7";

        private Cell4to7 _cmu5_4C7;

        /// <summary>
        /// Sets and gets the Cmu5_4C7 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Cell4to7 Cmu5_4C7
        {
            get
            {
                return _cmu5_4C7;
            }

            set
            {
                if (_cmu5_4C7.Equals(value))
                {
                    return;
                }

                _cmu5_4C7 = value;
                NotifyPropertyChanged(Cmu5_4C7PropertyName);
            }
        }

        #endregion Cmu5_4C7

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
            CurrentBmu = this;
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
            _cmu1Status = new CmuStatus();
            _cmu2Status = new CmuStatus();
            _cmu3Status = new CmuStatus();
            _cmu4Status = new CmuStatus();
            _cmu5Status = new CmuStatus();

            _cmu1_0C3 = new Cell0to3();
            _cmu2_0C3 = new Cell0to3();
            _cmu3_0C3 = new Cell0to3();
            _cmu4_0C3 = new Cell0to3();
            _cmu5_0C3 = new Cell0to3();

            _cmu1_4C7 = new Cell4to7();
            _cmu2_4C7 = new Cell4to7();
            _cmu3_4C7 = new Cell4to7();
            _cmu4_4C7 = new Cell4to7();
            _cmu5_4C7 = new Cell4to7();
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