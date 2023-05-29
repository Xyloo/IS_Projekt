using IS_Projekt.Models;

namespace IS_Projekt.Repos
{
    public interface IXmlRepository
    {
        Task<IEnumerable<InternetUse>> ImportInternetUseDataFromXmlFile(IEnumerable<InternetUse> parsedXml);
    }
}
