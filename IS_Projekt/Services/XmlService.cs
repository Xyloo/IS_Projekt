using IS_Projekt.Models;
using IS_Projekt.Repos;
using System.Globalization;
using System.Xml.Linq;

namespace IS_Projekt.Services
{
    public class XmlService : IXmlService
    {
        private readonly IFileDataRepository _xmlRepository;
        private readonly ILogger<XmlService> _logger;

        public XmlService(IFileDataRepository xmlRepository, ILogger<XmlService> logger)
        {
            _xmlRepository = xmlRepository;
            _logger = logger;
        }

        public async Task ExportDataToFile<T>(string path) where T : DataModel
        {
            var data = await _xmlRepository.ExportData<T>();
            _logger.LogInformation($"Exporting {data.Count()} rows to file {path}");
            _logger.LogInformation($"Type: {typeof(T)}");
            var first = data.First();

            var xml = new XDocument(
                new XElement("root",
                    data.Select(dm => new XElement("row",
                        new XElement("TIME_PERIOD", dm.Year.Year),
                        new XElement("geo", dm.Country.CountryCode),
                        new XElement("unit", dm.UnitOfMeasure),
                        new XElement("OBS_VALUE", dm.Value),
                        new XElement("indic_is", dm.IndividualCriteria)
                    ))
                ));
            xml.Save(path);
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
                    Value = double.TryParse(x.Element("OBS_VALUE")?.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out double obsValue) ? obsValue : 0.0,
                    UnitOfMeasure = x.Element("unit")?.Value,
                    Year = years.FirstOrDefault(y => y.Year == int.Parse(x.Element("TIME_PERIOD")?.Value ?? "0")),
                    Country = countries.FirstOrDefault(c => c.CountryCode == x.Element("geo")?.Value, countries.Last())
                })
                .ToList();

            return await _xmlRepository.ImportData(xmlDoc);
        }
    }
}