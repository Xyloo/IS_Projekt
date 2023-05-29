using IS_Projekt.Extensions;
using IS_Projekt.Models;
using IS_Projekt.Repos;
using System.Globalization;
using System.Xml;

namespace IS_Projekt.Services
{
    public class XmlService : IXmlService
    {
        private readonly IXmlRepository _xmlRepository;

        public XmlService(IXmlRepository xmlRepository)
        {
            _xmlRepository = xmlRepository;
        }

        public async Task<IEnumerable<InternetUse>> ImportInternetUseDataFromXmlFile(string path)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            var internetUseList = new List<InternetUse>();
            var allObservations = xmlDoc.SelectNodes("//row");

            foreach (XmlNode observation in allObservations!)
            {
                var internetUse = new InternetUse();
                internetUse.Country = CountryCodes.Countries[GetNodeValue(observation, "geo")!];
                internetUse.IndividualCriteria = GetNodeValue(observation, "indic_is")!;
                internetUse.Year = int.Parse(GetNodeValue(observation, "TIME_PERIOD")!);
                internetUse.Value = double.Parse(GetNodeValue(observation, "OBS_VALUE")!, CultureInfo.InvariantCulture);
                internetUseList.Add(internetUse);
            }   
            return await _xmlRepository.ImportInternetUseDataFromXmlFile(internetUseList);
          
        }
        private string? GetNodeValue(XmlNode parentNode, string nodeName)
        {
            XmlNode? node = parentNode.SelectSingleNode(nodeName);
            if (node == null)
            {
                return null;
            }
            return node?.InnerText.Trim();
        }
    }
}
