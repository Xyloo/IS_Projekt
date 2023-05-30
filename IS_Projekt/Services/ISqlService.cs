using IS_Projekt.Models;

namespace IS_Projekt.Services
{
    public interface ISqlService
    {
        Task ImportInternetUseDataFromFile(string path);
        Task ImportECommerceDataFromFile(string path);
        Task ExportECommerceDataToFile(string path); ///? cos takiego
        Task ExportInternetUseDataToFile(string path);
    }
}
