using IS_Projekt.Extensions;
using IS_Projekt.Models;

namespace IS_Projekt.Repos
{
    public interface IFileDataRepository
    {
        Task<IEnumerable<DataModel>> ImportData(IEnumerable<DataModel> parsedData);
        Task<IEnumerable<DataModel>> ExportData(DataTypes dataType);
    }
}
