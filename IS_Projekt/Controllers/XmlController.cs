using IS_Projekt.Extensions;
using IS_Projekt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;
using IS_Projekt.Models;

namespace IS_Projekt.Controllers
{
    [ApiController]
    [Route("api/xml")]
    public class XmlController : ControllerBase, IDataFileController
    {
        private readonly IXmlService _xmlService;
        private readonly ILogger<XmlController> _logger;
        public XmlController(IXmlService xmlService, ILogger<XmlController> logger)
        {
            _xmlService = xmlService;
            _logger = logger;
        }

        [HttpGet("import/internetuse")] //horrible temporary solution just for testing
        public async Task<IActionResult> ImportInternetUse()
        {
            var xml = await _xmlService.ImportDataFromFile<InternetUse>("./Resources/internet_use.xml");
            return Ok(xml);
        }

        [HttpGet("import/ecommerce")] //horrible temporary solution just for testing
        public async Task<IActionResult> ImportECommerce()
        {
            var xml = await _xmlService.ImportDataFromFile<ECommerce>("./Resources/ecommerce.xml");
            return Ok(xml);
        }

        [HttpGet("export/ecommerce")]
        public async Task<IActionResult> ExportECommerce()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources/ecommerce_download.xml");

            await _xmlService.ExportDataToFile<ECommerce>(filePath);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var contentDisposition = new ContentDisposition
            {
                FileName = Path.GetFileName(filePath),
                Inline = false  // false = prompt the user for download
            };
            Response.Headers[HeaderNames.ContentDisposition] = contentDisposition.ToString();


            return PhysicalFile(filePath, "application/xml");
        }

        [HttpGet("export/internetuse")]
        public async Task<IActionResult> ExportInternetUse()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources/internetuse_download.xml");
            await _xmlService.ExportDataToFile<InternetUse>(filePath);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var contentDisposition = new ContentDisposition
            {
                FileName = Path.GetFileName(filePath),
                Inline = false  // false = prompt the user for download
            };
            Response.Headers[HeaderNames.ContentDisposition] = contentDisposition.ToString();


            return PhysicalFile(filePath, "application/xml");
        }
    }
}
