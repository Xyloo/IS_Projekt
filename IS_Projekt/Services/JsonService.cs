using IS_Projekt.Extensions;
using IS_Projekt.Models;
using IS_Projekt.Repos;
using Newtonsoft.Json;

namespace IS_Projekt.Services
{
    public class JsonService : IFileService
    {
        public readonly IFileDataRepository _repository;

        public JsonService(IFileDataRepository jsonRepository)
        {
            _repository = jsonRepository;
        }

        public async Task ExportDataToFile(string path, DataTypes dataType)
        {
            var data = await _repository.ExportData(dataType);
            var jsonString = JsonConvert.SerializeObject(data);
            await File.WriteAllTextAsync(path, jsonString);
        }

        public async Task<IEnumerable<DataModel?>> ImportDataFromFile(string path, DataTypes dataType)
        {
            await using var fs = new FileStream(path, FileMode.Open);
            var deserializedData = await System.Text.Json.JsonSerializer.DeserializeAsync<List<JsonDataModel>>(fs);
            var dataT = dataType switch
            {
                DataTypes.ECommerce => "ECommerce",
                DataTypes.InternetUse => "InternetUse",
                _ => throw new ArgumentException("Invalid data type")
            };

            var dataList = deserializedData.Select(data => new DataModel
            {
                DataType = dataT,
                IndividualCriteria = data.indic_is,
                Country = CountryCodes.Countries[data.geo],
                UnitOfMeasure = data.unit,
                Year = data.TIME_PERIOD,
                Value = data.OBS_VALUE ?? 0.0
            }).ToList();

            return await _repository.ImportData(dataList);
        }

    }
}
