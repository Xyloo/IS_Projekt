using IS_Projekt.Dtos;
using IS_Projekt.Extensions;
using IS_Projekt.Models;
using IS_Projekt.Repos;
using Microsoft.AspNetCore.Mvc;

namespace IS_Projekt.Controllers
{
    [ApiController]
    [Route("api/data")]
    public class DataController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        public DataController(IDataRepository dataRepository) 
        {
            _dataRepository = dataRepository;
        }

        [HttpGet("{dataType}")]
        public async Task<IActionResult> GetAllData(string dataType) //tested
        {
            if(dataType == "ecommerce"){
                var data = await _dataRepository.GetAllData<ECommerce>();
                return Ok(data);
            }
            else if(dataType == "internetuse"){
                var data = await _dataRepository.GetAllData<InternetUse>(); 
                return Ok(data);
            }
            return BadRequest();
        }

        [HttpGet("{datatype}/{id}")]
        public async Task<IActionResult> GetDataById(string dataType, int id) //tested
        {
            if(dataType == "ecommerce"){            
                var data = await _dataRepository.GetDataById<ECommerce>(id);
                return Ok(data);
            }
            else if(dataType == "internetuse"){
                var data = await _dataRepository.GetDataById<InternetUse>(id);
                return Ok(data);
            }
            return BadRequest();
        }

        [HttpPost("{dataType}")]
        public async Task<IActionResult> AddData(DataModelDto dataDto, string dataType) //tested
        {
            try
            {
                if (dataType == "ecommerce")
                {
                    var data = await _dataRepository.AddData<ECommerce>(dataDto);
                    return Ok(data);
                }
                else if (dataType == "internetuse")
                {
                    var data = await _dataRepository.AddData<InternetUse>(dataDto);
                    return Ok(data);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }

        }

        [HttpDelete("{dataType}/{id}")]
        public async Task<IActionResult> DeleteData(string dataType, int id) //tested
        {
            try
            {
                if (dataType == "ecommerce")
                {
                    var data = await _dataRepository.DeleteData<ECommerce>(id);
                    return Ok(data);
                }
                else if (dataType == "internetuse")
                {
                    var data = await _dataRepository.DeleteData<InternetUse>(id);
                    return Ok(data);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("{dataType}/{id}")]
        public async Task<IActionResult> UpdateData(DataModelDto dataDto, string dataType, int id) //tested
        {
            try
            {
                if (dataType == "ecommerce")
                {
                    var data = await _dataRepository.UpdateData<ECommerce>(dataDto, id);
                    return Ok(data);
                }
                else if (dataType == "internetuse")
                {
                    var data = await _dataRepository.UpdateData<InternetUse>(dataDto, id);
                    return Ok(data);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("{dataType}/filter")]
        public async Task<IActionResult> GetFilteredData([FromBody] FilterCriteria filter, string dataType) //tested
        {
            if (dataType == "ecommerce")
            {
                var data = await _dataRepository.GetFilteredData<ECommerce>(filter);
                return Ok(data);
            }
            else if (dataType == "internetuse")
            {
                var data = await _dataRepository.GetFilteredData<InternetUse>(filter);
                return Ok(data);
            }
            return BadRequest();
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetDataInfo()
        {
            try
            {
                var dataInfo = await _dataRepository.GetDataInfo();
                return Ok(dataInfo);

            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
