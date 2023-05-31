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

        public async Task ExportDataToFile(string path, DataTypes dataType)
        {
            var xmlDocument = new XmlDocument();
            var xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            var root = xmlDocument.CreateElement("root");
            xmlDocument.InsertBefore(xmlDeclaration, xmlDocument.DocumentElement);
            xmlDocument.AppendChild(root);
            var data = await _xmlRepository.ExportData(dataType);
            foreach ( var item in data)
            {
                var row = xmlDocument.CreateElement("row");

                var geo = xmlDocument.CreateElement("geo");
                geo.InnerText = item.Country;
                row.AppendChild(geo);

                var indic_is = xmlDocument.CreateElement("indic_is");
                indic_is.InnerText = item.IndividualCriteria;
                row.AppendChild(indic_is);

                var unit = xmlDocument.CreateElement("unit");
                unit.InnerText = item.UnitOfMeasure;
                row.AppendChild(unit);

                var time_period = xmlDocument.CreateElement("TIME_PERIOD");
                time_period.InnerText = item.Year.ToString();
                row.AppendChild(time_period);

                var obs_value = xmlDocument.CreateElement("OBS_VALUE");
                obs_value.InnerText = item.Value.ToString(CultureInfo.InvariantCulture);
                row.AppendChild(obs_value);

                root.AppendChild(row);
            }
            xmlDocument.Save(path);
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
