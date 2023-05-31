using IS_Projekt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;
using IS_Projekt.Extensions;

namespace IS_Projekt.Controllers
{
    [ApiController]
    [Route("api/json")]
    public class JsonController : ControllerBase, IDataController
    {
        private readonly IFileService _jsonService;
        public JsonController(IFileService jsonService)
        {
            _jsonService = jsonService;
        }

        [HttpGet("import/ecommerce")] //horrible temporary solution just for testing
        public async Task<IActionResult> ImportECommerce()
        {
            var data = await _jsonService.ImportDataFromFile("./Resources/ecommerce.json", DataTypes.ECommerce);
            return Ok(data);
        }

        [HttpGet("import/internetuse")] //horrible temporary solution just for testing
        public async Task<IActionResult> ImportInternetUse()
        {
            var data = await _jsonService.ImportDataFromFile("./Resources/internetuse_raw.json", DataTypes.InternetUse);
            return Ok(data);
        }

        [HttpGet("export/ecommerce")]
        public async Task<IActionResult> ExportECommerce()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources/ecommerce_download.json");

            await _jsonService.ExportDataToFile(filePath, DataTypes.ECommerce);

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


            return PhysicalFile(filePath, "application/json");
        }


        [HttpGet("export/internetuse")]
        public async Task<IActionResult> ExportInternetUse()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources/internetuse_download.json");
            await _jsonService.ExportDataToFile(filePath, DataTypes.InternetUse);

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


            return PhysicalFile(filePath, "application/json");
        }
    }
}
