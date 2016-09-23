using System.Threading.Tasks;

namespace Ethereality.ViewModels.Models
{
    public interface IDataService
    {
        Task<DataItem> GetData();
    }
}