using IS_Projekt.Dtos;
using IS_Projekt.Extensions;
using IS_Projekt.Models;

namespace IS_Projekt.Repos {
    public interface IDataRepository {

        Task<IEnumerable<DataModelDto>> GetAllData<T>() where T : DataModel;
        Task<DataModelDto?> GetDataById<T>(int id) where T : DataModel;
        Task<T> AddData<T>(DataModelDto dataDto) where T : DataModel, new();
        Task<T> DeleteData<T>(int id) where T: DataModel;
        Task<T> UpdateData<T>(DataModelDto dataDto, int id) where T: DataModel;
        Task<IEnumerable<DataModelDto>> GetFilteredData<T>(FilterCriteria filter) where T: DataModel;
    }
}
