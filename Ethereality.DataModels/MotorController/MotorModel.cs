
using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Core;

namespace Isis.Model.Vehicle.DriveSystem
{

    public class MotorModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private async void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                await
                   Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () => PropertyChanged(this, new PropertyChangedEventArgs(propertyName)));
            }
        }
        public MotorModel()
        {
            _mcTimeStamp = 0.00;
            _busCurrent = 0.00F;
            _busVoltage = 0.00F;
            _vehicleVelocity = 0.00F;
            _motorRpm = 0.00F;
            _serialNumber = 0;
            _tritiumID = "ssss";
            _ipmPhaseCTemp = 0.00F;
            _ipmPhaseBTemp = 0.00F;
            _ipmPhaseATemp = 0.00F;
            _receiveErrorCount = 0;
            _trasnmitErrorCount = 0;
            _errorFlags = 0;
            _limitFlags = 0;
            _dcBusAmpHours = 0.00F;
            _odometer = 0.00F;
            _errorFlagBits = new BitArray(16, false);
            _limitFlagBits = new BitArray(16, false);
            _motorTemp = 0.00F;
            _dspBoardTemp = 0.00F;
            _activeMotor = 0;
            _phaseCurrentA = 0.00F;
            _phaseCurrentB = 0.00F;
            _voltageVectorD = 0.00F;
            _voltageVectorQ = 0.00F;
            _currentVectorD = 0.00F;
            _currentVectorQ = 0.00F;
            _bemfD = 0.00F;
            _bemfQ = 0.00F;
            _rail15V = 0.00F;
            _rail3V3 = 0.00F;
            _rail1V9 = 0.00F;
            _ipmHeatSinkTemp = 0.00F;
        }

        #region TimeStamp

        /// <summary>
        /// The <see cref="McTimeStamp" /> property's name.
        /// </summary>
        public const string TimeStampPropertyName = "TimeStamp";

        private double _mcTimeStamp;

        /// <summary>
        /// Sets and gets the TimeStamp property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public double McTimeStamp
        {
            get
            {
                return _mcTimeStamp;
            }

            set
            {
                if (_mcTimeStamp == value)
                {
                    return;
                }

                
                _mcTimeStamp = value;
                NotifyPropertyChanged(TimeStampPropertyName);
            }
        }

        #endregion TimeStamp

        #region BusCurrent

        /// <summary>
        /// The <see cref="BusCurrent" /> property's name.
        /// </summary>
        public const string BusCurrentPropertyName = "BusCurrent";

        private float _busCurrent;

        /// <summary>
        /// Sets and gets the BusCurrent property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>

        public float BusCurrent
        {
            get
            {
                return _busCurrent;
            }

            set
            {
                if (_busCurrent == value)
                {
                    return;
                }

              
                _busCurrent = value;
                NotifyPropertyChanged(BusCurrentPropertyName);
            }
        }

        #endregion BusCurrent

        #region BusVoltage

        /// <summary>
        /// The <see cref="BusVoltage" /> property's name.
        /// </summary>
        public const string BusVoltagePropertyName = "BusVoltage";

        private float _busVoltage;

        /// <summary>
        /// Sets and gets the BusVoltage property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float BusVoltage
        {
            get
            {
                return _busVoltage;
            }

            set
            {
                if (_busVoltage == value)
                {
                    return;
                }

              
                _busVoltage = value;
                NotifyPropertyChanged(BusVoltagePropertyName);
            }
        }

        #endregion BusVoltage

        #region VehicleVelocity

        /// <summary>
        /// The <see cref="VehicleVelocity" /> property's name.
        /// </summary>
        public const string VehicleVelocityPropertyName = "VehicleVelocity";

        private float _vehicleVelocity;

        /// <summary>
        /// Sets and gets the VehicleVelocity property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float VehicleVelocity
        {
            get
            {
                return _vehicleVelocity;
            }

            set
            {
                if (_vehicleVelocity == value)
                {
                    return;
                }

             
                _vehicleVelocity = value;
                NotifyPropertyChanged(VehicleVelocityPropertyName);
            }
        }

        #endregion VehicleVelocity

        #region MotorRpm

        /// <summary>
        /// The <see cref="MotorRpm" /> property's name.
        /// </summary>
        public const string MotorRpmPropertyName = "MotorRpm";

        private float _motorRpm;

        /// <summary>
        /// Sets and gets the MotorRpm property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float MotorRpm
        {
            get
            {
                return _motorRpm;
            }

            set
            {
                if (_motorRpm == value)
                {
                    return;
                }

            
                _motorRpm = value;
                NotifyPropertyChanged(MotorRpmPropertyName);
            }
        }

        #endregion MotorRpm

        #region SerialNumber

        /// <summary>
        /// The <see cref="SerialNumber" /> property's name.
        /// </summary>
        public const string SerialNumberPropertyName = "SerialNumber";

        private uint _serialNumber;

        /// <summary>
        /// Sets and gets the SerialNumber property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public uint SerialNumber
        {
            get
            {
                return _serialNumber;
            }

            set
            {
                if (_serialNumber == value)
                {
                    return;
                }

            
                _serialNumber = value;
                NotifyPropertyChanged(SerialNumberPropertyName);
            }
        }

        #endregion SerialNumber

        #region TritiumID

        /// <summary>
        /// The <see cref="TritiumID" /> property's name.
        /// </summary>
        public const string TritiumIDPropertyName = "TritiumID";

        private string _tritiumID;

        /// <summary>
        /// Sets and gets the TritiumID property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string TritiumID
        {
            get
            {
                return _tritiumID;
            }

            set
            {
                if (_tritiumID == value)
                {
                    return;
                }

          
                _tritiumID = value;
                NotifyPropertyChanged(TritiumIDPropertyName);
            }
        }

        #endregion TritiumID

        #region ReceiveErrorCount

        /// <summary>
        /// The <see cref="ReceiveErrorCount" /> property's name.
        /// </summary>
        public const string ReceiveErrorCountPropertyName = "ReceiveErrorCount";

        private byte _receiveErrorCount;

        /// <summary>
        /// Sets and gets the ReceiveErrorCount property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public byte ReceiveErrorCount
        {
            get
            {
                return _receiveErrorCount;
            }

            set
            {
                if (_receiveErrorCount == value)
                {
                    return;
                }

           
                _receiveErrorCount = value;
                NotifyPropertyChanged(ReceiveErrorCountPropertyName);
            }
        }

        #endregion ReceiveErrorCount

        #region TransmitErrorCount

        /// <summary>
        /// The <see cref="TransmitErrorCount" /> property's name.
        /// </summary>
        public const string TransmitErrorCountPropertyName = "TransmitErrorCount";

        private byte _trasnmitErrorCount;

        /// <summary>
        /// Sets and gets the TransmitErrorCount property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public byte TransmitErrorCount
        {
            get
            {
                return _trasnmitErrorCount;
            }

            set
            {
                if (_trasnmitErrorCount == value)
                {
                    return;
                }

             
                _trasnmitErrorCount = value;
                NotifyPropertyChanged(TransmitErrorCountPropertyName);
            }
        }

        #endregion TransmitErrorCount

        #region ActiveMotor

        /// <summary>
        /// The <see cref="ActiveMotor" /> property's name.
        /// </summary>
        public const string ActiveMotorPropertyName = "ActiveMotor";

        private UInt16 _activeMotor;

        /// <summary>
        /// Sets and gets the ActiveMotor property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public UInt16 ActiveMotor
        {
            get
            {
                return _activeMotor;
            }

            set
            {
                if (_activeMotor == value)
                {
                    return;
                }

          
                _activeMotor = value;
                NotifyPropertyChanged(ActiveMotorPropertyName);
            }
        }

        #endregion ActiveMotor

        #region ErrorFlags

        /// <summary>
        /// The <see cref="ErrorFlags" /> property's name.
        /// </summary>
        public const string ErrorFlagsPropertyName = "ErrorFlags";

        private UInt16 _errorFlags;

        /// <summary>
        /// Sets and gets the ErrorFlags property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public UInt16 ErrorFlags
        {
            get
            {
                return _errorFlags;
            }

            set
            {
                if (_errorFlags == value)
                {
                    return;
                }

         
                _errorFlags = value;
                NotifyPropertyChanged(ErrorFlagsPropertyName);
            }
        }

        #endregion ErrorFlags

        #region LimitFlags

        /// <summary>
        /// The <see cref="LimitFlags" /> property's name.
        /// </summary>
        public const string LimitFlagsPropertyName = "LimitFlags";

        private UInt16 _limitFlags;

        /// <summary>
        /// Sets and gets the LimitFlags property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public UInt16 LimitFlags
        {
            get
            {
                return _limitFlags;
            }

            set
            {
                if (_limitFlags == value)
                {
                    return;
                }

             
                _limitFlags = value;
                NotifyPropertyChanged(LimitFlagsPropertyName);
            }
        }

        #endregion LimitFlags

        #region ErrorFlagBits

        /// <summary>
        /// The <see cref="ErrorFlagBits" /> property's name.
        /// </summary>
        public const string ErrorFlagBitsPropertyName = "ErrorFlagBits";

        private BitArray _errorFlagBits;

        /// <summary>
        /// Sets and gets the ErrorFlagBits property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public BitArray ErrorFlagBits
        {
            get
            {
                return _errorFlagBits;
            }

            set
            {
                if (_errorFlagBits == value)
                {
                    return;
                }

              
                _errorFlagBits = value;
                NotifyPropertyChanged(ErrorFlagBitsPropertyName);
            }
        }

        #endregion ErrorFlagBits

        #region LimitFlagBits

        /// <summary>
        /// The <see cref="LimitFlagBits" /> property's name.
        /// </summary>
        public const string LimitFlagBitsPropertyName = "LimitFlagBits";

        private BitArray _limitFlagBits;

        /// <summary>
        /// Sets and gets the LimitFlagBits property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public BitArray LimitFlagBits
        {
            get
            {
                return _limitFlagBits;
            }

            set
            {
                if (_limitFlagBits == value)
                {
                    return;
                }

        
                _limitFlagBits = value;
                NotifyPropertyChanged(LimitFlagBitsPropertyName);
            }
        }

        #endregion LimitFlagBits

        #region PhaseCurrentA

        /// <summary>
        /// The <see cref="PhaseCurrentA" /> property's name.
        /// </summary>
        public const string PhaseCurrentAPropertyName = "PhaseCurrentA";

        private float _phaseCurrentA;

        /// <summary>
        /// Sets and gets the PhaseCurrentA property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float PhaseCurrentA
        {
            get
            {
                return _phaseCurrentA;
            }

            set
            {
                if (_phaseCurrentA == value)
                {
                    return;
                }

           
                _phaseCurrentA = value;
                NotifyPropertyChanged(PhaseCurrentAPropertyName);
            }
        }

        #endregion PhaseCurrentA

        #region PhaseCurrentB

        /// <summary>
        /// The <see cref="PhaseCurrentB" /> property's name.
        /// </summary>
        public const string PhaseCurrentBPropertyName = "PhaseCurrentB";

        private float _phaseCurrentB;

        /// <summary>
        /// Sets and gets the PhaseCurrentB property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float PhaseCurrentB
        {
            get
            {
                return _phaseCurrentB;
            }

            set
            {
                if (_phaseCurrentB == value)
                {
                    return;
                }

              
                _phaseCurrentB = value;
                NotifyPropertyChanged(PhaseCurrentBPropertyName);
            }
        }

        #endregion PhaseCurrentB

        #region VoltageVectorD

        /// <summary>
        /// The <see cref="VoltageVectorD" /> property's name.
        /// </summary>
        public const string VoltageVectorDPropertyName = "VoltageVectorD";

        private float _voltageVectorD;

        /// <summary>
        /// Sets and gets the VoltageVectorD property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float VoltageVectorD
        {
            get
            {
                return _voltageVectorD;
            }

            set
            {
                if (_voltageVectorD == value)
                {
                    return;
                }

            
                _voltageVectorD = value;
                NotifyPropertyChanged(VoltageVectorDPropertyName);
            }
        }

        #endregion VoltageVectorD

        #region VoltageVectorQ

        /// <summary>
        /// The <see cref="VoltageVectorQ" /> property's name.
        /// </summary>
        public const string VoltageVectorQPropertyName = "VoltageVectorQ";

        private float _voltageVectorQ;

        /// <summary>
        /// Sets and gets the VoltageVectorQ property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float VoltageVectorQ
        {
            get
            {
                return _voltageVectorQ;
            }

            set
            {
                if (_voltageVectorQ == value)
                {
                    return;
                }

             
                _voltageVectorQ = value;
                NotifyPropertyChanged(VoltageVectorQPropertyName);
            }
        }

        #endregion VoltageVectorQ

        #region CurrentVectorD

        /// <summary>
        /// The <see cref="CurrentVectorD" /> property's name.
        /// </summary>
        public const string CurrentVectorDPropertyName = "CurrentVectorD";

        private float _currentVectorD;

        /// <summary>
        /// Sets and gets the CurrentVectorD property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float CurrentVectorD
        {
            get
            {
                return _currentVectorD;
            }

            set
            {
                if (_currentVectorD == value)
                {
                    return;
                }

           
                _currentVectorD = value;
                NotifyPropertyChanged(CurrentVectorDPropertyName);
            }
        }

        #endregion CurrentVectorD

        #region CurrentVectorQ

        /// <summary>
        /// The <see cref="CurrentVectorQ" /> property's name.
        /// </summary>
        public const string CurrentVectorQPropertyName = "CurrentVectorQ";

        private float _currentVectorQ;

        /// <summary>
        /// Sets and gets the CurrentVectorQ property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float CurrentVectorQ
        {
            get
            {
                return _currentVectorQ;
            }

            set
            {
                if (_currentVectorQ == value)
                {
                    return;
                }

                _currentVectorQ = value;
                NotifyPropertyChanged(CurrentVectorQPropertyName);
            }
        }

        #endregion CurrentVectorQ

        #region BEmfD

        /// <summary>
        /// The <see cref="BEmfD" /> property's name.
        /// </summary>
        public const string BEmfDPropertyName = "BEmfD";

        private float _bemfD;

        /// <summary>
        /// Sets and gets the BEmfD property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float BEmfD
        {
            get
            {
                return _bemfD;
            }

            set
            {
                if (_bemfD == value)
                {
                    return;
                }

   
                _bemfD = value;
                NotifyPropertyChanged(BEmfDPropertyName);
            }
        }

        #endregion BEmfD

        #region BemfQ

        /// <summary>
        /// The <see cref="BEmfQ" /> property's name.
        /// </summary>
        public const string BEmfQPropertyName = "BEmfQ";

        private float _bemfQ;

        /// <summary>
        /// Sets and gets the BEmfQ property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float BEmfQ
        {
            get
            {
                return _bemfQ;
            }

            set
            {
                if (_bemfQ == value)
                {
                    return;
                }

    
                _bemfQ = value;
                NotifyPropertyChanged(BEmfQPropertyName);
            }
        }

        #endregion BemfQ

        #region Rail15V

        /// <summary>
        /// The <see cref="Rail15V" /> property's name.
        /// </summary>
        public const string Rail15VPropertyName = "Rail15V";

        private float _rail15V;

        /// <summary>
        /// Sets and gets the Rail15V property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float Rail15V
        {
            get
            {
                return _rail15V;
            }

            set
            {
                if (_rail15V == value)
                {
                    return;
                }

    
                _rail15V = value;
                NotifyPropertyChanged(Rail15VPropertyName);
            }
        }

        #endregion Rail15V

        #region Rail3V3

        /// <summary>
        /// The <see cref="Rail3V3" /> property's name.
        /// </summary>
        public const string Rail3V3PropertyName = "Rail3V3";

        private float _rail3V3;

        /// <summary>
        /// Sets and gets the Rail3V3 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float Rail3V3
        {
            get
            {
                return _rail3V3;
            }

            set
            {
                if (_rail3V3 == value)
                {
                    return;
                }

     
                _rail3V3 = value;
                NotifyPropertyChanged(Rail3V3PropertyName);
            }
        }

        #endregion Rail3V3

        #region Rail1V9

        /// <summary>
        /// The <see cref="Rail1V9" /> property's name.
        /// </summary>
        public const string Rail1V9PropertyName = "Rail1V9";

        private float _rail1V9;

        /// <summary>
        /// Sets and gets the Rail1V9 property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float Rail1V9
        {
            get
            {
                return _rail1V9;
            }

            set
            {
                if (_rail1V9 == value)
                {
                    return;
                }

    
                _rail1V9 = value;
                NotifyPropertyChanged(Rail1V9PropertyName);
            }
        }

        #endregion Rail1V9

        #region IpmHeatSinkTemp

        /// <summary>
        /// The <see cref="IpmHeatSinkTemp" /> property's name.
        /// </summary>
        public const string IpmHeatSinkTempPropertyName = "IpmHeatSinkTemp";

        private float _ipmHeatSinkTemp;

        /// <summary>
        /// Sets and gets the IpmHeatSinkTemp property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float IpmHeatSinkTemp
        {
            get
            {
                return _ipmHeatSinkTemp;
            }

            set
            {
                if (_ipmHeatSinkTemp == value)
                {
                    return;
                }


                _ipmHeatSinkTemp = value;
                NotifyPropertyChanged(IpmHeatSinkTempPropertyName);
            }
        }

        #endregion IpmHeatSinkTemp

        #region IPMPhaseCTemp

        /// <summary>
        /// The <see cref="IPMPhaseCTemp" /> property's name.
        /// </summary>
        public const string IPMPhaseCTempPropertyName = "IPMPhaseCTemp";

        private float _ipmPhaseCTemp;

        /// <summary>
        /// Sets and gets the IPMPhaseCTemp property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float IPMPhaseCTemp
        {
            get
            {
                return _ipmPhaseCTemp;
            }

            set
            {
                if (_ipmPhaseCTemp == value)
                {
                    return;
                }

                _ipmPhaseCTemp = value;
                NotifyPropertyChanged(IPMPhaseCTempPropertyName);
            }
        }

        #endregion IPMPhaseCTemp

        #region IPMPhaseBTemp

        /// <summary>
        /// The <see cref="IPMPhaseBTemp" /> property's name.
        /// </summary>
        public const string IPMPhaseBTempPropertyName = "IPMPhaseBTemp";

        private float _ipmPhaseBTemp;

        /// <summary>
        /// Sets and gets the IPMPhaseBTemp property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float IPMPhaseBTemp
        {
            get
            {
                return _ipmPhaseBTemp;
            }

            set
            {
                if (_ipmPhaseBTemp == value)
                {
                    return;
                }

   
                _ipmPhaseBTemp = value;
                NotifyPropertyChanged(IPMPhaseBTempPropertyName);
            }
        }

        #endregion IPMPhaseBTemp

        #region IPMPhaseATemp

        /// <summary>
        /// The <see cref="IPMPhaseATemp" /> property's name.
        /// </summary>
        public const string IPMPhaseATempPropertyName = "IPMPhaseATemp";

        private float _ipmPhaseATemp;

        /// <summary>
        /// Sets and gets the IPMPhaseATemp property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float IPMPhaseATemp
        {
            get
            {
                return _ipmPhaseATemp;
            }

            set
            {
                if (_ipmPhaseATemp == value)
                {
                    return;
                }

     
                _ipmPhaseATemp = value;
                NotifyPropertyChanged(IPMPhaseATempPropertyName);
            }
        }

        #endregion IPMPhaseATemp

        #region MotorTemp

        /// <summary>
        /// The <see cref="MotorTemp" /> property's name.
        /// </summary>
        public const string MotorTempPropertyName = "MotorTemp";

        private float _motorTemp;

        /// <summary>
        /// Sets and gets the MotorTemp property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float MotorTemp
        {
            get
            {
                return _motorTemp;
            }

            set
            {
                if (_motorTemp == value)
                {
                    return;
                }

       
                _motorTemp = value;
                NotifyPropertyChanged(MotorTempPropertyName);
            }
        }

        #endregion MotorTemp

        #region DspBoardTemp

        /// <summary>
        /// The <see cref="DspBoardTemp" /> property's name.
        /// </summary>
        public const string DspBoardTempPropertyName = "DspBoardTemp";

        private float _dspBoardTemp;

        /// <summary>
        /// Sets and gets the DspBoardTemp property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float DspBoardTemp
        {
            get
            {
                return _dspBoardTemp;
            }

            set
            {
                if (_dspBoardTemp == value)
                {
                    return;
                }

      
                _dspBoardTemp = value;
                NotifyPropertyChanged(DspBoardTempPropertyName);
            }
        }

        #endregion DspBoardTemp

        // public double DcBusAmpHours { get; set; }

        #region DcBusAmpHours

        /// <summary>
        /// The <see cref="DcBusAmpHours" /> property's name.
        /// </summary>
        public const string DcBusAmpHoursPropertyName = "DcBusAmpHours";

        private float _dcBusAmpHours;

        /// <summary>
        /// Sets and gets the DcBusAmpHours property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float DcBusAmpHours
        {
            get
            {
                return _dcBusAmpHours;
            }

            set
            {
                if (_dcBusAmpHours == value)
                {
                    return;
                }

       
                _dcBusAmpHours = value;
                NotifyPropertyChanged(DcBusAmpHoursPropertyName);
            }
        }

        #endregion DcBusAmpHours

        // public double Odometer { get; set; }

        #region Odometer

        /// <summary>
        /// The <see cref="Odometer" /> property's name.
        /// </summary>
        public const string OdometerPropertyName = "Odometer";

        private float _odometer;

        /// <summary>
        /// Sets and gets the Odometer property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float Odometer
        {
            get
            {
                return _odometer;
            }

            set
            {
                if (_odometer == value)
                {
                    return;
                }

    
                _odometer = value;
                NotifyPropertyChanged(OdometerPropertyName);
            }
        }

        #endregion Odometer

        #region SlipSpeed

        /// <summary>
        /// The <see cref="SlipSpeed" /> property's name.
        /// </summary>
        public const string SlipSpeedPropertyName = "SlipSpeed";

        private float _slipSpeed;

        /// <summary>
        /// Sets and gets the SlipSpeed property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public float SlipSpeed
        {
            get
            {
                return _slipSpeed;
            }

            set
            {
                if (_slipSpeed == value)
                {
                    return;
                }

  
                _slipSpeed = value;
                NotifyPropertyChanged(SlipSpeedPropertyName);
            }
        }

        #endregion SlipSpeed
    }
}