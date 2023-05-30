using IS_Projekt.Models;

namespace IS_Projekt.Repos
{
    public interface IFileDataRepository
    {
        Task<IEnumerable<InternetUse>> ImportDataInternetUse(IEnumerable<InternetUse> parsedData);
        Task<IEnumerable<InternetUse>> ExportDataInternetUse();
        Task<IEnumerable<ECommerce>> ImportDataECommerce(IEnumerable<ECommerce> parsedData);
        Task<IEnumerable<ECommerce>> ExportDataECommerce();
    }
}
