using IS_Projekt.Models;
namespace IS_Projekt.Services
{
    public interface IXmlService
    {
        Task<IEnumerable<InternetUse>> ImportInternetUseDataFromXmlFile(string path);
    }
}
