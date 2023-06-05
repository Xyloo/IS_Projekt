using IS_Projekt.Models;

namespace IS_Projekt.Repos
{
    public interface IFileDataRepository
    {
        Task<IEnumerable<T>> ImportData<T>(IEnumerable<T> parsedData) where T : DataModel;
        Task<IEnumerable<T>> ExportData<T>() where T : DataModel;
        Task<IEnumerable<CountryModel>> GetCountries();
        Task<IEnumerable<YearModel>> GetYears();

    }
}
