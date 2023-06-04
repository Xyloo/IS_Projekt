using IS_Projekt.Dtos;
using IS_Projekt.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.ServiceModel;
using IS_Projekt.Models;

namespace IS_Projekt.Controllers
{
    [ServiceContract]
    public interface ISoapDataService
    {
        [OperationContract]
        public Task<SoapResponse<IEnumerable<DataModelDto>?>> GetAllData(string dataType);
        [OperationContract]
        public Task<SoapResponse<DataModelDto?>> GetDataById(string dataType, int id);

        [OperationContract]
        public Task<SoapResponse<DataModel?>> AddData(DataModelDto dataDto, string dataType);

        [OperationContract]
        public Task<SoapResponse<DataModel?>> DeleteData(string dataType, int id);

        [OperationContract]
        public Task<SoapResponse<DataModel?>> UpdateData(DataModelDto dataDto, string dataType, int id);

        [OperationContract]
        public Task<SoapResponse<IEnumerable<DataModelDto>?>> GetFilteredData([FromBody] FilterCriteria filter, string dataType);

        [OperationContract]
        public Task<SoapResponse<DataInfoDto?>> GetDataInfo();
    }
}
