using Ethereality.DataParserMachine;
using SerialCommunicationUWP;
using System.Threading.Tasks;

namespace Ethereality.DataManagement
{
    public interface IDataManager
    {
        Task<SerialDeviceItem> GetSerial();

        Task<MainDataParsingService> GetTelemetryData();

        Task SerialPolling();
    }
}