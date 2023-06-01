using IS_Projekt.Extensions;
using IS_Projekt.Models;
namespace IS_Projekt.Services
{
    public interface IFileService
    {
        Task<IEnumerable<T?>> ImportDataFromFile<T>(string path) where T : DataModel, new();
        Task ExportDataToFile<T>(string path) where T: DataModel;
    }
}
