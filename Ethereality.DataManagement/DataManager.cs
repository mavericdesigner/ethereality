using Ethereality.DataParserMachine;
using GalaSoft.MvvmLight.Threading;
using SerialCommunicationUWP;
using System;
using System.Threading.Tasks;

namespace Ethereality.DataManagement
{
    public class DataManager : IDataManager
    {
        private static SerialDeviceItem serialDeviceItem;
        private static MainDataParsingService dataParsingService;

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

        public async Task SerialPolling()
        {
            Task t1 = Task.Run(async () =>
               {
                   while (EventHandlerForDevice.Current.IsDeviceConnected)
                   {
                       try
                       {
                           DispatcherHelper.CheckBeginInvokeOnUI(() =>
                           {
                               serialDeviceItem.ReadWriteHandle.ReadButton_Click();

                               
                               });
                         
                               await Task.Delay(50);
                         
                          
                       }
                       catch (Exception ex)
                       {
                           throw ex;
                       }
                   }
               });

            Task t2 = Task.Run(async () =>
            {
                while (EventHandlerForDevice.Current.IsDeviceConnected)
                {
                    try
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                         

                            dataParsingService.StartParser(serialDeviceItem.ReadWriteHandle.ReadSerialQueue);
                        });

                        await Task.Delay(50);


                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            });
            await t1;
            await t2;
        }
    }
}