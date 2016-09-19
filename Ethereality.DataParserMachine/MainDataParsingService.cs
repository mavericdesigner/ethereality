
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Core;
using Ethereality.DataModels.Battery;
using Ethereality.DataModels.MPPT;
using Ethereality.DataModels.DriveSystem;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Threading;
using System.Collections.Concurrent;
using System.Linq;

namespace Ethereality.DataParserMachine
{
   public struct CanFrameModel
    {
        public UInt16 CANID;
        public byte[] CANDATA;
        public  CanFrameModel(UInt16 canID,byte[] canDATA)
        {
            CANID = canID;
            CANDATA = canDATA;
        }
    }
   public enum CanBytePosition
    {
        Sync=0x00,
        ByteID0=0x01,
        ByteID1=0x02,
        ByteData0=0x03,
        ByteData1 = 0x04,
        ByteData2 = 0x05,
        ByteData3 = 0x06,
        ByteData4 = 0x07,
        ByteData5 = 0x08,
        ByteData6 = 0x09,
        ByteData7 = 0x0A,

    }
   public class MainDataParsingService:INotifyPropertyChanged  
    {
        public static MainDataParsingService CurrentMainDataParsingService;
        private bool ParseEnable;
        private Queue<MpptModel> mpptQueue;

        public Queue<MpptModel> MpptQueue
        {
            get { return mpptQueue; }
            set
            {
                if (mpptQueue.Equals(value))
                {
                    return;
                }
                mpptQueue = value;
                NotifyPropertyChanged("MpptQueue");
            }
        }

        private MpptModel mpptWord;

        public MpptModel MpptWord
        {
            get { return mpptWord; }
            set
            {
                if (mpptWord.Equals(value))
                {
                    return;
                }
                mpptWord = value;
                NotifyPropertyChanged("MpptWord");
            }
        }

        private MotorModel motorWord;

        public MotorModel MotorWord
        {
            get { return motorWord; }
            set
            {
                if (motorWord.Equals(value))
                {
                    return;
                }
                motorWord = value;
                NotifyPropertyChanged("MotorWord");
            }
        }

        private BmuModel batteryWord;

        public BmuModel BatteryWord
        {
            get { return batteryWord; }
            set
            {
                if (batteryWord.Equals(value))
                {
                    return;
                }
                batteryWord = value;
                NotifyPropertyChanged("BatteryWord");
            }
        }

        public MainDataParsingService()
        {
            CurrentMainDataParsingService = this;
            MpptQueue = new Queue<MpptModel>(50);
            CanFrame = new CanFrameModel(0, new byte[8]);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private CanFrameModel canFrame;

        public CanFrameModel CanFrame
        {
            get { return canFrame; }
            set
            {
                if (canFrame.Equals(value))
                {
                    return;
                }
                canFrame = value;
                NotifyPropertyChanged("CanFrame");
            }
        }
        public async Task StartParser(ConcurrentQueue<byte> ByteQueue)
        {
            Task t =Task.Run(() =>
            {
                    try
                    { 
                        DispatcherHelper.CheckBeginInvokeOnUI(async () =>
                        {
                           await BytesToCanFrameConversion(ByteQueue);
                        });

                    
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
              
            });
            await t.AsAsyncAction();
        }

        public  void StopParser()
        {
            ParseEnable = false;
        }

        public async Task BytesToCanFrameConversion(ConcurrentQueue<byte> ByteQueue)
        {
            byte tmp = 0;
            bool IsAvailable = false;
            byte[] tempArray = new byte[11];
            ByteQueue.TryPeek(out tmp);
            if ((tmp == 0xFF) && (ByteQueue.Count >= 11))
            {
                IsAvailable = ByteQueue.TryDequeue(out tmp);
                if (IsAvailable)
                    tempArray[(int)CanBytePosition.Sync] = tmp;
                IsAvailable = ByteQueue.TryDequeue(out tmp);
                if (IsAvailable)
                    tempArray[(int)CanBytePosition.ByteID0] = tmp;
                IsAvailable = ByteQueue.TryDequeue(out tmp);
                if (IsAvailable)
                    tempArray[(int)CanBytePosition.ByteID1] = tmp;
                IsAvailable = ByteQueue.TryDequeue(out tmp);
                if (IsAvailable)
                    tempArray[(int)CanBytePosition.ByteData0] = tmp;
                IsAvailable = ByteQueue.TryDequeue(out tmp);
                if (IsAvailable)
                    tempArray[(int)CanBytePosition.ByteData1] = tmp;
                IsAvailable = ByteQueue.TryDequeue(out tmp);
                if (IsAvailable)
                    tempArray[(int)CanBytePosition.ByteData2] = tmp;
                IsAvailable = ByteQueue.TryDequeue(out tmp);
                if (IsAvailable)
                    tempArray[(int)CanBytePosition.ByteData3] = tmp;
                IsAvailable = ByteQueue.TryDequeue(out tmp);
                if (IsAvailable)
                    tempArray[(int)CanBytePosition.ByteData4] = tmp;
                IsAvailable = ByteQueue.TryDequeue(out tmp);
                if (IsAvailable)
                    tempArray[(int)CanBytePosition.ByteData5] = tmp;
                IsAvailable = ByteQueue.TryDequeue(out tmp);
                if (IsAvailable)
                    tempArray[(int)CanBytePosition.ByteData6] = tmp;
                IsAvailable = ByteQueue.TryDequeue(out tmp);
                if (IsAvailable)
                    tempArray[(int)CanBytePosition.ByteData7] = tmp;

                CanFrame = new CanFrameModel()
                {
                    CANID = BitConverter.ToUInt16(tempArray, (int)CanBytePosition.ByteID0),
                    CANDATA = tempArray.Skip((int)CanBytePosition.ByteID1).ToArray(),
                };

                await ParseCanFrame();


            }
        }
        public async Task ParseCanFrame()
        {
            if ((CanFrame.CANID>0x400)&&(CanFrame.CANID<0x438))
            {
                Task motorParseTask=Task.Run(()=>
                ParseMotorCan());
                motorParseTask.Start();
                await motorParseTask;
            }
            else if ((CanFrame.CANID>=0x600)&&(CanFrame.CANID<=0x6FD))
            {
                Task batteryParseTask=Task.Run(()=>
                ParseBatteryCan());
                await batteryParseTask;
             
            }
            else if ((CanFrame.CANID>=0x771)&&(CanFrame.CANID<=0x776))
            {
                Task mpptParseTask=Task.Run(()=>
                ParseMpptCan());
                await mpptParseTask;
            }

           
        }

        private void PanelMovingAverage(MpptModel NewValue)
        {

        }
        private void ParseMpptCan()
        {
            
            switch ((PanelNum)canFrame.CANID)
            {
                case PanelNum.Mppt1Num:
                    MpptWord.MpptMsg0 = new MpptMsgStruct()
                    {
                        TimeStamp = DateTime.Now,
                        Voltage = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.ArrayVolt),
                        Current = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.ArrayCurrent),
                        Battery = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.BatteryVolt),
                        Temperature = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.MpptTemp)
                    };
                    break;

                case PanelNum.Mppt2Num:
                    MpptWord.MpptMsg1 = new MpptMsgStruct()
                    {
                        TimeStamp = DateTime.Now,
                        Voltage = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.ArrayVolt),
                        Current = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.ArrayCurrent),
                        Battery= BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.BatteryVolt),
                        Temperature = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.MpptTemp)
                    };

                    break;

                case PanelNum.Mppt3Num:
                    MpptWord.MpptMsg2 = new MpptMsgStruct()
                    {
                        TimeStamp = DateTime.Now,
                        Voltage = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.ArrayVolt),
                        Current = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.ArrayCurrent),
                        Battery = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.BatteryVolt),
                        Temperature = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.MpptTemp)
                    };

                    break;

                case PanelNum.Mppt4Num:
                    MpptWord.MpptMsg3 = new MpptMsgStruct()
                    {
                        TimeStamp = DateTime.Now,
                        Voltage = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.ArrayVolt),
                        Current = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.ArrayCurrent),
                        Battery = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.BatteryVolt),
                        Temperature = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.MpptTemp)
                    };
                    break;

                case PanelNum.Mppt5Num:
                    MpptWord.MpptMsg4 = new MpptMsgStruct()
                    {
                        Voltage = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.ArrayVolt),
                        Current = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.ArrayCurrent),
                        Battery = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.BatteryVolt),
                        Temperature = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.MpptTemp)
                    };
                    break;

                case PanelNum.Mppt6Num:
                    MpptWord.MpptMsg5 = new MpptMsgStruct()
                    {
                        Voltage = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.ArrayVolt),
                        Current = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.ArrayCurrent),
                        Battery = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.BatteryVolt),
                        Temperature = BitConverter.ToInt16(CanFrame.CANDATA, (byte)MpptMsgNum.MpptTemp)
                    };
                    break;
            
            }
        }

        private void ParseBatteryCan()
        {
            switch ((BatteryPackMsgAddress)CanFrame.CANID)
            {
                case BatteryPackMsgAddress.Cmu1Status:
                    BatteryWord.Cmu1Status = new CmuStatus()
                    {
                        SerialNumber = BitConverter.ToUInt32(CanFrame.CANDATA, 0),
                        PcbTemp = BitConverter.ToInt16(CanFrame.CANDATA, 4),
                        CellTemp = BitConverter.ToInt16(CanFrame.CANDATA, 6)
                    };
                    break;

                case BatteryPackMsgAddress.Cmu1Volt0:
                    BatteryWord.Cmu1_0C3 = new Cell0to3()
                    {
                        Cell0 = BitConverter.ToInt16(CanFrame.CANDATA, 0),
                        Cell1 = BitConverter.ToInt16(CanFrame.CANDATA, 2),
                        Cell2 = BitConverter.ToInt16(CanFrame.CANDATA, 4),
                        Cell3 = BitConverter.ToInt16(CanFrame.CANDATA, 6)
                    };
                    break;

                case BatteryPackMsgAddress.Cmu1Volt1:
                    BatteryWord.Cmu1_4C7 = new Cell4to7()
                    {
                        Cell4 = BitConverter.ToInt16(CanFrame.CANDATA, 0),
                        Cell5 = BitConverter.ToInt16(CanFrame.CANDATA, 2),
                        Cell6 = BitConverter.ToInt16(CanFrame.CANDATA, 4),
                        Cell7 = BitConverter.ToInt16(CanFrame.CANDATA, 6),
                    };
                    break;

                case BatteryPackMsgAddress.Cmu2Status:
                    BatteryWord.Cmu2Status = new CmuStatus()
                    {
                        SerialNumber = BitConverter.ToUInt32(CanFrame.CANDATA, 0),
                        PcbTemp = BitConverter.ToInt16(CanFrame.CANDATA, 4),
                        CellTemp = BitConverter.ToInt16(CanFrame.CANDATA, 6)
                    };
                    break;

                case BatteryPackMsgAddress.Cmu2Volt0:
                    BatteryWord.Cmu2_0C3 = new Cell0to3()
                    {
                        Cell0 = BitConverter.ToInt16(CanFrame.CANDATA, 0),
                        Cell1 = BitConverter.ToInt16(CanFrame.CANDATA, 2),
                        Cell2 = BitConverter.ToInt16(CanFrame.CANDATA, 4),
                        Cell3 = BitConverter.ToInt16(CanFrame.CANDATA, 6)
                    };
                    break;

                case BatteryPackMsgAddress.Cmu2Volt1:
                    BatteryWord.Cmu2_4C7 = new Cell4to7()
                    {
                        Cell4 = BitConverter.ToInt16(CanFrame.CANDATA, 0),
                        Cell5 = BitConverter.ToInt16(CanFrame.CANDATA, 2),
                        Cell6 = BitConverter.ToInt16(CanFrame.CANDATA, 4),
                        Cell7 = BitConverter.ToInt16(CanFrame.CANDATA, 6)
                    };
                    break;

                case BatteryPackMsgAddress.Cmu3Status:
                    BatteryWord.Cmu3Status = new CmuStatus()
                    {
                        SerialNumber = BitConverter.ToUInt32(CanFrame.CANDATA, 0),
                        PcbTemp = BitConverter.ToInt16(CanFrame.CANDATA, 4),
                        CellTemp = BitConverter.ToInt16(CanFrame.CANDATA, 6)
                    };
                    break;

                case BatteryPackMsgAddress.Cmu3Volt0:
                    BatteryWord.Cmu3_0C3 = new Cell0to3()
                    {
                        Cell0 = BitConverter.ToInt16(CanFrame.CANDATA, 0),
                        Cell1 = BitConverter.ToInt16(CanFrame.CANDATA, 2),
                        Cell2 = BitConverter.ToInt16(CanFrame.CANDATA, 4),
                        Cell3 = BitConverter.ToInt16(CanFrame.CANDATA, 6)
                    };
                    break;

                case BatteryPackMsgAddress.Cmu3Volt1:
                    BatteryWord.Cmu3_4C7 = new Cell4to7()
                    {
                        Cell4 = BitConverter.ToInt16(CanFrame.CANDATA, 0),
                        Cell5 = BitConverter.ToInt16(CanFrame.CANDATA, 2),
                        Cell6 = BitConverter.ToInt16(CanFrame.CANDATA, 4),
                        Cell7 = BitConverter.ToInt16(CanFrame.CANDATA, 6)
                    };
                    break;

                case BatteryPackMsgAddress.Cmu4Status:
                    BatteryWord.Cmu4Status = new CmuStatus()
                    {
                        SerialNumber = BitConverter.ToUInt32(CanFrame.CANDATA, 0),
                        PcbTemp = BitConverter.ToInt16(CanFrame.CANDATA, 4),
                        CellTemp = BitConverter.ToInt16(CanFrame.CANDATA, 6)
                    };
                    break;

                case BatteryPackMsgAddress.Cmu4Volt0:
                    BatteryWord.Cmu4_0C3 = new Cell0to3()
                    {
                        Cell0 = BitConverter.ToInt16(CanFrame.CANDATA, 0),
                        Cell1 = BitConverter.ToInt16(CanFrame.CANDATA, 2),
                        Cell2 = BitConverter.ToInt16(CanFrame.CANDATA, 4),
                        Cell3 = BitConverter.ToInt16(CanFrame.CANDATA, 6)
                    };
                    break;

                case BatteryPackMsgAddress.Cmu4Volt1:
                    BatteryWord.Cmu4_4C7 = new Cell4to7()
                    {
                        Cell4 = BitConverter.ToInt16(CanFrame.CANDATA, 0),
                        Cell5 = BitConverter.ToInt16(CanFrame.CANDATA, 2),
                        Cell6 = BitConverter.ToInt16(CanFrame.CANDATA, 4),
                        Cell7 = BitConverter.ToInt16(CanFrame.CANDATA, 6)
                    };
                    break;

                case BatteryPackMsgAddress.Cmu5Status:
                    BatteryWord.Cmu5Status = new CmuStatus()
                    {
                        SerialNumber = BitConverter.ToUInt32(CanFrame.CANDATA, 0),
                        PcbTemp = BitConverter.ToInt16(CanFrame.CANDATA, 4),
                        CellTemp = BitConverter.ToInt16(CanFrame.CANDATA, 6)
                    };
                    break;

                case BatteryPackMsgAddress.Cmu5Volt0:
                    BatteryWord.Cmu5_0C3 = new Cell0to3()
                    {
                        Cell0 = BitConverter.ToInt16(CanFrame.CANDATA, 0),
                        Cell1 = BitConverter.ToInt16(CanFrame.CANDATA, 2),
                        Cell2 = BitConverter.ToInt16(CanFrame.CANDATA, 4),
                        Cell3 = BitConverter.ToInt16(CanFrame.CANDATA, 6)
                    };
                    break;

                case BatteryPackMsgAddress.Cmu5Volt1:
                    BatteryWord.Cmu5_4C7 = new Cell4to7()
                    {
                        Cell4 = BitConverter.ToInt16(CanFrame.CANDATA, 0),
                        Cell5 = BitConverter.ToInt16(CanFrame.CANDATA, 2),
                        Cell6 = BitConverter.ToInt16(CanFrame.CANDATA, 4),
                        Cell7 = BitConverter.ToInt16(CanFrame.CANDATA, 6)
                    };
                    break;

                case BatteryPackMsgAddress.BmsID:
                    BatteryWord.BmuHeartSerialProp = new BmuHeartBeatSerialNumber()
                    {
                        BmuHeartbeat = BitConverter.ToUInt32(CanFrame.CANDATA, 0),
                        BmuSerialNumber = BitConverter.ToUInt32(CanFrame.CANDATA, 4)
                    };

                    break;

                case BatteryPackMsgAddress.PackSoc:
                    BatteryWord.PackSocProp = new PackStateOfCharge()
                    {
                        SocAmpHours = BitConverter.ToSingle(CanFrame.CANDATA, 0),
                        SocPercentage = BitConverter.ToSingle(CanFrame.CANDATA, 4)
                    };
                    break;

                case BatteryPackMsgAddress.PackBalanceStateofCharge:
                    BatteryWord.PackBalanceSoc = new PackBalanceStateOfCharge()
                    {
                        BalanceSocAmpHours = BitConverter.ToSingle(CanFrame.CANDATA, 0),
                        BalanceSocPercentage = BitConverter.ToSingle(CanFrame.CANDATA, 4)
                    };
                    break;

                case BatteryPackMsgAddress.ChargeControlInformation:
                    BatteryWord.ChargerControlInfo = new ChargerControlInformation()
                    {
                        ChargingCellVoltageError = BitConverter.ToInt16(CanFrame.CANDATA, 0),
                        CellTemperatureMargin = BitConverter.ToInt16(CanFrame.CANDATA, 2),
                        DischargingCellVoltageError = BitConverter.ToInt16(CanFrame.CANDATA, 4),
                        TotalPackCapacity = BitConverter.ToInt16(CanFrame.CANDATA, 6)
                    };
                    break;

                case BatteryPackMsgAddress.PrechargeStatus:
                    BatteryWord.PrechargeStatusProp = new PrechargeStatus()
                    {
                        PrechargeContactorDriverStatus = CanFrame.CANDATA[0],
                        PrechargeStates = CanFrame.CANDATA[1],
                        ContactorSupplyVoltage12V = CanFrame.CANDATA[2],
                        PrechargeTimerState = CanFrame.CANDATA[6],
                        PrechargeTimerCounter = CanFrame.CANDATA[7]
                    };
                    break;

                case BatteryPackMsgAddress.MinMaxCellVoltage:
                    BatteryWord.MinMaxCellVolt = new MinimumMaximumCellVoltage()
                    {
                        MinimumCellVoltage = BitConverter.ToUInt16(CanFrame.CANDATA, 0),
                        MaximumCellVoltage = BitConverter.ToUInt16(CanFrame.CANDATA, 2),
                        CmuNumberMinCellVoltage = CanFrame.CANDATA[4],
                        CellInCmuMinCellVoltage = CanFrame.CANDATA[5],
                        CmuNumberMaxCellVoltage = CanFrame.CANDATA[6],
                        CellInCmuMaxCellVoltage = CanFrame.CANDATA[7]
                    };
                    break;

                case BatteryPackMsgAddress.MinMaxCellTemperature:
                    BatteryWord.MinMaxCellTemp = new MinimumMaximumCellTemperature()
                    {
                        MinimumCellTemperature = BitConverter.ToUInt16(CanFrame.CANDATA, 0),
                        MaximumCellTemperature = BitConverter.ToUInt16(CanFrame.CANDATA, 2),
                        CmuNumberMinCellTemperature = CanFrame.CANDATA[4],
                        CmuNumberMaxCellTemperature = CanFrame.CANDATA[6]
                    };
                    break;

                case BatteryPackMsgAddress.BatteryPackVoltageCurrent:
                    BatteryWord.BatteryPackVA = new BatteryPackVoltageCurrent()
                    {
                        BatteryVoltage = BitConverter.ToUInt32(CanFrame.CANDATA, 0),//mV
                        BatteryCurrent = BitConverter.ToInt32(CanFrame.CANDATA, 4)//mA
                    };
                    break;

                case BatteryPackMsgAddress.BatteryPackStatus:
                    BatteryWord.BatteryPackStatusProp = new BatteryPackStatus()
                    {
                        BalanceVoltageThresholdRising = BitConverter.ToUInt16(CanFrame.CANDATA, 0),
                        BalanceVoltageThresholdFalling = BitConverter.ToUInt16(CanFrame.CANDATA, 2),
                        BatteryPackStatusFlags = CanFrame.CANDATA[6],
                        BmsCmuCount = CanFrame.CANDATA[7],
                        BmuFirmwareBuiltNumber = BitConverter.ToUInt16(CanFrame.CANDATA, 4)
                    };
                    break;

                case BatteryPackMsgAddress.BatterPackFanStatus:
                    BatteryWord.BatteryPackFanStatusProp = new BatteryPackFanStatus()
                    {
                        SpeedFan0 = BitConverter.ToUInt16(CanFrame.CANDATA, 0),
                        SpeedFan1 = BitConverter.ToUInt16(CanFrame.CANDATA, 2),
                        CurrentConsumption12vFanPlusContactor = BitConverter.ToUInt16(CanFrame.CANDATA, 4),
                        CurrentConsumption12vCmu = BitConverter.ToUInt16(CanFrame.CANDATA, 6)
                    };
                    break;

                case BatteryPackMsgAddress.ExtendedBatteryPackStatus:
                    BatteryWord.BatteryPackStatusExt = new ExtendedBatteryPackStatus()
                    {
                        StatusFlags = BitConverter.ToUInt32(CanFrame.CANDATA, 0),
                        BmuHardwareVersion = CanFrame.CANDATA[4],
                        BmuModelID = CanFrame.CANDATA[5]
                    };
                    break;
                default:
                    break;
            }
        }
        public void ErrorMessages(UInt16 ErrorFlags, UInt16 LimitFlags)
        {
            byte[] errorFlagBits = BitConverter.GetBytes(ErrorFlags);
            byte[] limitFlagBits = BitConverter.GetBytes(LimitFlags);

            ErrorFlags errorFlag = (ErrorFlags)errorFlagBits[0];
            LimitFlags limitFlag = (LimitFlags)limitFlagBits[0];
            MotorWord.ErrorFlagBits = new BitArray(errorFlagBits);
            MotorWord.LimitFlagBits = new BitArray(limitFlagBits);

            return;
        }
        private void ParseMotorCan()
        {
            switch ((MCPacketID)canFrame.CANID)
            {
                case MCPacketID.ID:
                    MotorWord.TritiumID = BitConverter.ToString(CanFrame.CANDATA, 4);
                    MotorWord.SerialNumber = BitConverter.ToUInt32(CanFrame.CANDATA, 0);
                    break;

                case MCPacketID.StatusInfo:
                    MotorWord.ReceiveErrorCount = CanFrame.CANDATA[7];
                    MotorWord.TransmitErrorCount = CanFrame.CANDATA[6];
                    MotorWord.ActiveMotor = BitConverter.ToUInt16(CanFrame.CANDATA, 4);
                    MotorWord.ErrorFlags = BitConverter.ToUInt16(CanFrame.CANDATA, 2);
                    MotorWord.LimitFlags = BitConverter.ToUInt16(CanFrame.CANDATA, 0);
                    ErrorMessages(MotorWord.ErrorFlags, MotorWord.LimitFlags);
                    break;

                case MCPacketID.VelocityMeasure:

                    MotorWord.VehicleVelocity = BitConverter.ToSingle(CanFrame.CANDATA, 4);
                    MotorWord.MotorRpm = BitConverter.ToSingle(CanFrame.CANDATA, 0);
                    break;

                case MCPacketID.BusMeasure:

                    MotorWord.BusCurrent = BitConverter.ToSingle(CanFrame.CANDATA, 4);

                    MotorWord.BusVoltage = BitConverter.ToSingle(CanFrame.CANDATA, 0);
                    break;

                case MCPacketID.PhaseCurrentMeasure:

                    MotorWord.PhaseCurrentA = BitConverter.ToSingle(CanFrame.CANDATA, 4);

                    MotorWord.PhaseCurrentB = BitConverter.ToSingle(CanFrame.CANDATA, 0);
                    break;

                case MCPacketID.VoltageVectorMeasure:

                    MotorWord.VoltageVectorD = BitConverter.ToSingle(CanFrame.CANDATA, 4);

                    MotorWord.VoltageVectorQ = BitConverter.ToSingle(CanFrame.CANDATA, 0);
                    break;

                case MCPacketID.CurrentVectorMeasure:

                    MotorWord.CurrentVectorD = BitConverter.ToSingle(CanFrame.CANDATA, 4);

                    MotorWord.CurrentVectorQ = BitConverter.ToSingle(CanFrame.CANDATA, 0);
                    break;

                case MCPacketID.BackEMFMeasure:

                    MotorWord.BEmfD = BitConverter.ToSingle(CanFrame.CANDATA, 4);

                    MotorWord.BEmfQ = BitConverter.ToSingle(CanFrame.CANDATA, 0);
                    break;

                case MCPacketID.Rail15VMeasure:

                    MotorWord.Rail15V = BitConverter.ToSingle(CanFrame.CANDATA, 4);
                    break;

                case MCPacketID.Rail3V2And1V9Measure:

                    MotorWord.Rail3V3 = BitConverter.ToSingle(CanFrame.CANDATA, 4);

                    MotorWord.Rail1V9 = BitConverter.ToSingle(CanFrame.CANDATA, 0);
                    break;

                case MCPacketID.Reserved:
                    break;

                case MCPacketID.IPMPhaseCMotorTempMeasure:

                    MotorWord.IPMPhaseCTemp = BitConverter.ToSingle(CanFrame.CANDATA, 4);

                    MotorWord.MotorTemp = BitConverter.ToSingle(CanFrame.CANDATA, 0);
                    break;

                case MCPacketID.IPMPhaseBDspTempMeasure:

                    MotorWord.IPMPhaseBTemp = BitConverter.ToSingle(CanFrame.CANDATA, 4);

                    MotorWord.DspBoardTemp = BitConverter.ToSingle(CanFrame.CANDATA, 0);
                    break;

                case MCPacketID.IPMPhaseATempMeasure:

                    MotorWord.IPMPhaseATemp = BitConverter.ToSingle(CanFrame.CANDATA, 4);
                    break;

                case MCPacketID.OdometerBusAmpHMeasure:

                    MotorWord.DcBusAmpHours = BitConverter.ToSingle(CanFrame.CANDATA, 4);

                    MotorWord.Odometer = BitConverter.ToSingle(CanFrame.CANDATA, 0);
                    break;

                case MCPacketID.SlipSpeedMeasure:

                    MotorWord.SlipSpeed = BitConverter.ToSingle(CanFrame.CANDATA, 4);
                    break;

                default:
                    break;
            }

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