using IS_Projekt.Extensions;
using IS_Projekt.Models;
using IS_Projekt.Repos;
using System.Globalization;
using System.Xml;

namespace IS_Projekt.Services
{
    public class XmlService : IFileService
    {
        private readonly IFileDataRepository _xmlRepository;

        public XmlService(IFileDataRepository xmlRepository)
        {
            _xmlRepository = xmlRepository;
        }

        public Task ExportDataToFile(string path, DataTypes dataType)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DataModel?>> ImportDataFromFile(string path, DataTypes dataType)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            var dataList = new List<DataModel>();
            var allObservations = xmlDoc.SelectNodes("//row");

            foreach (XmlNode observation in allObservations!)
            {
                var data = new DataModel();
                data.DataType = dataType switch
                {
                    DataTypes.ECommerce => "ECommerce",
                    DataTypes.InternetUse => "InternetUse",
                    _ => throw new ArgumentException("Invalid data type")
                };
                data.Country = CountryCodes.Countries[GetNodeValue(observation, "geo")!];
                data.IndividualCriteria = GetNodeValue(observation, "indic_is")!;
                data.Year = int.Parse(GetNodeValue(observation, "TIME_PERIOD")!);
                data.UnitOfMeasure = GetNodeValue(observation, "unit")!;
                data.Value = double.Parse(GetNodeValue(observation, "OBS_VALUE")!, CultureInfo.InvariantCulture);
                dataList.Add(data);
            }
            return await _xmlRepository.ImportData(dataList);
        }

        private string? GetNodeValue(XmlNode parentNode, string nodeName)
        {
            XmlNode? node = parentNode.SelectSingleNode(nodeName);
            return node?.InnerText.Trim();
        }
    }
}
