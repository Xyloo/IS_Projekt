using IS_Projekt.Extensions;
using IS_Projekt.Services;
using Microsoft.AspNetCore.Mvc;

namespace IS_Projekt.Controllers
{
    [ApiController]
    [Route("api/xml")]
    public class XmlController : ControllerBase
    {
        private readonly IFileService _xmlService;
        public XmlController(IFileService xmlService)
        {
            _xmlService = xmlService;
        }

        [HttpGet] //horrible temporary solution just for testing
        public async Task<IActionResult> ImportXmlFile()
        {
            var xml = await _xmlService.ImportDataFromFile("./Resources/internet_use.xml", DataTypes.InternetUse);
            return Ok(xml);
        }
    }
}
