using IS_Projekt.Extensions;
using IS_Projekt.Models;
namespace IS_Projekt.Services
{
    public interface IFileService
    {
        Task<IEnumerable<DataModel?>> ImportDataFromFile(string path, DataTypes dataType);
        Task ExportDataToFile(string path, DataTypes dataType);
    }
}
