using IS_Projekt.Dtos;
using IS_Projekt.Extensions;
using IS_Projekt.Models;
using IS_Projekt.Repos;
using Microsoft.AspNetCore.Mvc;

namespace IS_Projekt.Controllers
{
    [ApiController]
    public class SoapController : ControllerBase, ISoapDataService
    {
        private readonly IDataRepository _dataRepository;

        public SoapController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet("{dataType}")]
        public async Task<SoapResponse<IEnumerable<DataModelDto>?>> GetAllData(string dataType) //tested
        {
            var data = dataType switch
            {
                "ecommerce" => await _dataRepository.GetAllData<ECommerce>(),
                "internetuse" => await _dataRepository.GetAllData<InternetUse>(),
                _ => null
            };
            if (data == null)
            {
                return new SoapResponse<IEnumerable<DataModelDto>?>
                {
                    Error = "Bad Request. Allowed values: ecommerce, internetuse"
                };
            }
            return new SoapResponse<IEnumerable<DataModelDto>?>
            {
                Data = data
            };
        }

        [HttpGet("{datatype}/{id:int}")]
        public async Task<SoapResponse<DataModelDto?>> GetDataById(string dataType, int id) //tested
        {
            var data = dataType switch
            {
                "ecommerce" => await _dataRepository.GetDataById<ECommerce>(id),
                "internetuse" => await _dataRepository.GetDataById<InternetUse>(id),
                _ => null
            };
            if (data == null)
            {
                return new SoapResponse<DataModelDto?>
                {
                    Error = "Bad Request. Allowed values: ecommerce, internetuse or ID is out of range"
                };
            }
            return new SoapResponse<DataModelDto?>
            {
                Data = data
            };
        }

        [HttpPost("{dataType}")]
        public async Task<SoapResponse<DataModel?>> AddData(DataModelDto dataDto, string dataType) //tested
        {
            try
            {
                DataModel? data = dataType switch
                {
                    "ecommerce" => await _dataRepository.AddData<ECommerce>(dataDto),
                    "internetuse" => await _dataRepository.AddData<InternetUse>(dataDto),
                    _ => null
                };

                if (data != null)
                {
                    return new SoapResponse<DataModel?>()
                    {
                        Data = data,
                        Message = "Data added successfully"
                    };
                }
                return new SoapResponse<DataModel?>()
                {
                    Error = "Bad Request.\nAllowed dataType values: ecommerce, internetuse.\nTo check for valid values in other fields, use GetDataInfo endpoint. \nIgnore the ID field or provide a parsable value."
                };
            }
            catch (Exception ex)
            {
                return new SoapResponse<DataModel?>()
                {
                    Error = $"Error: {ex.Message}"
                };
            }
        }

        [HttpDelete("{dataType}/{id}")]
        public async Task<SoapResponse<DataModel?>> DeleteData(string dataType, int id) //tested
        {
            try
            {
                DataModel? data = dataType switch
                {
                    "ecommerce" => await _dataRepository.DeleteData<ECommerce>(id),
                    "internetuse" => await _dataRepository.DeleteData<InternetUse>(id),
                    _ => null
                };
                if (data != null)
                {
                    return new SoapResponse<DataModel?>()
                    {
                        Data = data,
                        Message = "Data deleted successfully"
                    };
                }
                return new SoapResponse<DataModel?>()
                {
                    Error = "Bad Request.\nAllowed dataType values: ecommerce, internetuse.\nTo check for valid values in other fields, use GetDataInfo endpoint. \nID might albo be out of range."
                };
            }
            catch (Exception ex)
            {
                return new SoapResponse<DataModel?>()
                {
                    Error = $"Error: {ex.Message}"
                };
            }
        }

        [HttpPut("{dataType}/{id}")]
        public async Task<SoapResponse<DataModel?>> UpdateData(DataModelDto dataDto, string dataType, int id) //tested
        {
            try
            {
                DataModel? data = dataType switch
                {
                    "ecommerce" => await _dataRepository.UpdateData<ECommerce>(dataDto, id),
                    "internetuse" => await _dataRepository.UpdateData<InternetUse>(dataDto, id),
                    _ => null
                };
                if (data != null)
                {
                    return new SoapResponse<DataModel?>()
                    {
                        Data = data,
                        Message = "Data updated successfully"
                    };
                }
                return new SoapResponse<DataModel?>()
                {
                    Error = "Bad Request.\nAllowed dataType values: ecommerce, internetuse.\nTo check for valid values in other fields, use GetDataInfo endpoint."
                };
            }
            catch (Exception ex)
            {
                return new SoapResponse<DataModel?>()
                {
                    Error = $"Error: {ex.Message}"
                };
            }
        }

        [HttpPost("{dataType}/filter")]
        public async Task<SoapResponse<IEnumerable<DataModelDto>?>> GetFilteredData([FromBody] FilterCriteria filter, string dataType) //tested
        {
            var data = dataType switch
            {
                "ecommerce" => await _dataRepository.GetFilteredData<ECommerce>(filter),
                "internetuse" => await _dataRepository.GetFilteredData<InternetUse>(filter),
                _ => null
            };
            if (data == null)
            {
                return new SoapResponse<IEnumerable<DataModelDto>?>
                {
                    Error = "Bad Request.\nAllowed dataType values: ecommerce, internetuse.\nTo check for valid values in other fields, use GetDataInfo endpoint."
                };
            }
            return new SoapResponse<IEnumerable<DataModelDto>?>
            {
                Data = data
            };
        }

        [HttpGet("info")]
        public async Task<SoapResponse<DataInfoDto?>> GetDataInfo()
        {
            try
            {
                var dataInfo = await _dataRepository.GetDataInfo();
                return new SoapResponse<DataInfoDto?>()
                {
                    Data = dataInfo
                };
            }
            catch (Exception ex)
            {
                return new SoapResponse<DataInfoDto?>()
                {
                    Error = $"Error: {ex.Message}"
                };
            }
        }
    }
}