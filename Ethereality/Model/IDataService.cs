using System.Threading.Tasks;

namespace Ethereality.Model
{
    public interface IDataService
    {
        Task<DataItem> GetData();
    }
}