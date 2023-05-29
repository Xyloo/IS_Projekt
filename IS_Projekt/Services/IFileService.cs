using IS_Projekt.Models;
namespace IS_Projekt.Services
{
    public interface IFileService
    {
        Task<IEnumerable<InternetUse>> ImportInternetUseDataFromFile(string path);
        Task<IEnumerable<ECommerce>> ImportECommerceDataFromFile(string path);
        Task ExportECommerceDataToFile(string path); ///? cos takiego
        Task ExportInternetUseDataToFile(string path);

    }
}
