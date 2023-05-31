﻿using IS_Projekt.Extensions;
using IS_Projekt.Models;
using IS_Projekt.Repos;
using Newtonsoft.Json;

namespace IS_Projekt.Services
{
    public class JsonService : IJsonService
    {
        public readonly IFileDataRepository _repository;

        public JsonService(IFileDataRepository jsonRepository)
        {
            _repository = jsonRepository;
        }

        public async Task ExportDataToFile<T>(string path) where T : DataModel
        {
            var data = await _repository.ExportData<T>();
            var jsonString = JsonConvert.SerializeObject(data);
            await File.WriteAllTextAsync(path, jsonString);
        }

        public async Task<IEnumerable<T?>> ImportDataFromFile<T>(string path) where T : DataModel, new()
        {
            await using var fs = new FileStream(path, FileMode.Open);
            var deserializedData = await System.Text.Json.JsonSerializer.DeserializeAsync<List<JsonDataModel>>(fs);

            var dataList = deserializedData.Select(data => new T
            {
                IndividualCriteria = data.indic_is,
                //Country = new Countries(),
                UnitOfMeasure = data.unit,
                //Year = new Years(),
                Value = data.OBS_VALUE ?? 0.0
            }).ToList();

            return await _repository.ImportData<T>(dataList);
        }

    }
}
