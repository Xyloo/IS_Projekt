using IS_Projekt.Extensions;
using IS_Projekt.Models;
using IS_Projekt.Repos;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;

namespace IS_Projekt.Services
{
    public class XmlService : IXmlService
    {
        private readonly IFileDataRepository _xmlRepository;

        public XmlService(IFileDataRepository xmlRepository)
        {
            _xmlRepository = xmlRepository;
        }

        public async Task ExportDataToFile<T>(string path) where T : DataModel
        {
            var xmlDocument = new XmlDocument();
            var xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            var root = xmlDocument.CreateElement("root");
            xmlDocument.InsertBefore(xmlDeclaration, xmlDocument.DocumentElement);
            xmlDocument.AppendChild(root);
            var data = await _xmlRepository.ExportData<T>();
            foreach ( var item in data)
            {
                var row = xmlDocument.CreateElement("row");

                var geo = xmlDocument.CreateElement("geo");
               // geo.InnerText = item.Country;
                row.AppendChild(geo);

                var indic_is = xmlDocument.CreateElement("indic_is");
                indic_is.InnerText = item.IndividualCriteria;
                row.AppendChild(indic_is);

                var unit = xmlDocument.CreateElement("unit");
                unit.InnerText = item.UnitOfMeasure;
                row.AppendChild(unit);

                var time_period = xmlDocument.CreateElement("TIME_PERIOD");
                //time_period.InnerText = item.Year.ToString();
                row.AppendChild(time_period);

                var obs_value = xmlDocument.CreateElement("OBS_VALUE");
                obs_value.InnerText = item.Value.ToString(CultureInfo.InvariantCulture);
                row.AppendChild(obs_value);

                root.AppendChild(row);
            }
            xmlDocument.Save(path);
        }

        public async Task<IEnumerable<T?>> ImportDataFromFile<T>(string path) where T : DataModel, new()
        {
            var years = await _xmlRepository.GetYears();
            var countries = await _xmlRepository.GetCountries();
            var xmlDoc = XDocument
                .Load(path)
                .Root
                .Elements("row")
                .Select(x => new T
                {
                    IndividualCriteria = x.Element("indic_is")?.Value,
                    Value = double.Parse(x.Element("OBS_VALUE")?.Value ?? "0.0", CultureInfo.InvariantCulture),
                    UnitOfMeasure = x.Element("unit")?.Value,
                    Year = years.FirstOrDefault(y => y.Year == int.Parse(x.Element("TIME_PERIOD")?.Value ?? "0")),
                    Country = countries.FirstOrDefault(c => c.CountryCode == x.Element("geo")?.Value, countries.Last())
                })
                .ToList();

            return await _xmlRepository.ImportData(xmlDoc);
        }

    }
}
