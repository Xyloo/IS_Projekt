using IS_Projekt.Extensions;
using IS_Projekt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;

namespace IS_Projekt.Controllers
{
    [ApiController]
    [Route("api/xml")]
    public class XmlController : ControllerBase, IDataController
    {
        private readonly IFileService _xmlService;
        public XmlController(IFileService xmlService)
        {
            _xmlService = xmlService;
        }

        [HttpGet("import/internetuse")] //horrible temporary solution just for testing
        public async Task<IActionResult> ImportInternetUse()
        {
            var xml = await _xmlService.ImportDataFromFile("./Resources/internet_use.xml", DataTypes.InternetUse);
            return Ok(xml);
        }

        [HttpGet("import/ecommerce")] //horrible temporary solution just for testing
        public async Task<IActionResult> ImportECommerce()
        {
            var xml = await _xmlService.ImportDataFromFile("./Resources/ecommerce.xml", DataTypes.ECommerce);
            return Ok(xml);
        }

        [HttpGet("export/ecommerce")]
        public async Task<IActionResult> ExportECommerce()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources/ecommerce_download.xml");

            await _xmlService.ExportDataToFile(filePath, DataTypes.ECommerce);

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
            await _xmlService.ExportDataToFile(filePath, DataTypes.InternetUse);

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
