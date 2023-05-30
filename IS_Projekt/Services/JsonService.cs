using IS_Projekt.Extensions;
using IS_Projekt.Models;
using IS_Projekt.Repos;
using Newtonsoft.Json;
using System.Text.Json;

namespace IS_Projekt.Services
{
    public class JsonService : IJsonService
    {
        public readonly IFileDataRepository _repository;

        public JsonService(IJsonRepository jsonRepository)
        {
            _repository = jsonRepository;
        }

        public async Task ExportECommerceDataToFile(string path)
        {
            var eCommerceData = await _repository.ExportDataECommerce();
            var jsonString = JsonConvert.SerializeObject(eCommerceData);
            await File.WriteAllTextAsync(path, jsonString);
        }

        public async Task ExportInternetUseDataToFile(string path)
        {
            var InternetUseData = await _repository.ExportDataInternetUse();
            var jsonString = JsonConvert.SerializeObject(InternetUseData);
            await File.WriteAllTextAsync(path, jsonString);
        }

        public async Task<IEnumerable<ECommerce>> ImportECommerceDataFromFile(string path)
        {
            using FileStream fs = new FileStream(path, FileMode.Open);
            var eCommerceData = await System.Text.Json.JsonSerializer.DeserializeAsync<List<JsonECommerceData>>(fs);

            var eCommerceList = eCommerceData.Select(data => new ECommerce
            {
                IndividualCriteria = data.indic_is,
                Country = CountryCodes.Countries[data.geo],
                UnitOfMeasure = data.unit,
                Year = data.TIME_PERIOD,
                Value = data.OBS_VALUE ?? 0.0
            }).ToList();

            return await _repository.ImportDataECommerce(eCommerceList);
        }

        public async Task<IEnumerable<InternetUse>> ImportInternetUseDataFromFile(string path)
        {
            using FileStream fs = new FileStream(path, FileMode.Open);
            var InternetUseData = await System.Text.Json.JsonSerializer.DeserializeAsync<List<JsonECommerceData>>(fs);

            var InternetUseDataList = InternetUseData.Select(data => new InternetUse
            {
                IndividualCriteria = data.indic_is,
                Country = CountryCodes.Countries[data.geo],
                Year = data.TIME_PERIOD,
                Value = data.OBS_VALUE ?? 0.0
            }).ToList();

            return await _repository.ImportDataInternetUse(InternetUseDataList);
        }
        
    }
}
