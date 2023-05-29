using IS_Projekt.Services;
using Microsoft.AspNetCore.Mvc;

namespace IS_Projekt.Controllers
{
    [ApiController]
    [Route("api/xml")]
    public class XmlController : ControllerBase
    {
        private readonly IXmlService _xmlService;
        public XmlController(IXmlService xmlService)
        {
            _xmlService = xmlService;
        }

        [HttpGet] //horrible temporary solution just for testing
        public async Task<IActionResult> ImportXmlFile()
        {
            var xml = await _xmlService.ImportInternetUseDataFromXmlFile("./internet_use.xml");
            return Ok(xml);
        }
    }
}
