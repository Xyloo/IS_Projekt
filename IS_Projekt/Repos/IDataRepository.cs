using IS_Projekt.Extensions;
using IS_Projekt.Models;

namespace IS_Projekt.Repos {
    public interface IDataRepository {
        Task<DataModel> GetDataById(int id);
        Task<IEnumerable<DataModel>> GetAllData();
        Task<IEnumerable<DataModel>> GetDataByCountry(string country);
        Task<IEnumerable<DataModel>> GetDataByYear(int year);
        Task<IEnumerable<DataModel>> GetDataByIndividualCriteria(string individualCriteria);
        Task<IEnumerable<DataModel>> GetDataByUnitOfMeasure(string unitOfMeasure);
        Task<IEnumerable<DataModel>> GetDataByCountryAndYear(string country, int year);
        Task<IEnumerable<DataModel>> GetFilteredData(FilterCriteria filter);
        Task<DataModel> AddData(DataModel data);
        Task<DataModel> UpdateData(DataModel data);
        Task<DataModel> DeleteData(int id);
        
    }
}
