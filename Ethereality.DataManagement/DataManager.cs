using SerialCommunicationUWP;
using Ethereality.DataParserMachine;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Threading;
using System;
using Windows.UI.Core;
using System.Collections.Concurrent;

namespace Ethereality.DataManagement
{
    public class DataManager:IDataManager
    {
        private SerialDeviceItem serialDeviceItem;
        private MainDataParsingService dataParsingService;
        public async Task<SerialDeviceItem> GetSerial()
        {
            serialDeviceItem = new SerialDeviceItem();
            serialDeviceItem.ConfigDevice.ShortConfig();
            return await Task.FromResult<SerialDeviceItem>(serialDeviceItem);
            
        }
        public async Task<MainDataParsingService> GetTelemetryData()
        {
            dataParsingService = new MainDataParsingService();
            return await Task.FromResult<MainDataParsingService>(dataParsingService);
        }
        public void SerialPolling(SerialDeviceItem serialDevice)
        {
            Task.Run(async () =>
            {
                while (EventHandlerForDevice.Current.IsDeviceConnected)
                {
                    try
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            serialDevice.ReadWriteHandle.ReadButton_Click();
                            ReadTelemetry(serialDevice.ReadWriteHandle.ReadSerialQueue);                            
                        });

                        await Task.Delay(50);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            });
        }

        private async void ReadTelemetry(ConcurrentQueue<byte> DataQueue)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                new DispatchedHandler(async () =>
                {
                   await dataParsingService.StartParser(DataQueue);
                }));
        }
        
    }
}