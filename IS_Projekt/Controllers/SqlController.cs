using IS_Projekt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;
using IS_Projekt.Extensions;

namespace IS_Projekt.Controllers
{
    [ApiController]
    [Route("api/sql")]
    public class SqlController : ControllerBase
    {
        private readonly IFileService _sqlService;
        public SqlController(IFileService sqlService)
        {
            _sqlService = sqlService;
        }

        [HttpGet("import/ecommerce")] 
        public async Task<IActionResult> ImportECommerce()
        {
            await _sqlService.ImportDataFromFile("./Resources/ECommerce_sql.sql", DataTypes.ECommerce);
            return Ok();
        }

        [HttpGet("import/internetuse")] //horrible temporary solution just for testing
        public async Task<IActionResult> ImportInternetUse()
        {
            await _sqlService.ImportDataFromFile("./Resources/InternetUse_sql.sql", DataTypes.InternetUse);
            return Ok();
        }

        [HttpGet("export/ecommerce")]
        public async Task<IActionResult> ExportECommerce()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources/ecommerce_sql.txt");
            await _sqlService.ImportDataFromFile(filePath, DataTypes.ECommerce);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            var contentDisposition = new ContentDisposition
            {
                FileName = Path.GetFileName(filePath),
                Inline = false
            };
            Response.Headers[HeaderNames.ContentDisposition] = contentDisposition.ToString();
            return PhysicalFile(filePath, "text/plain");
        }


        [HttpGet("export/internetuse")]
        public async Task<IActionResult> ExportInternetUse()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources/internetuse_sql.txt");
            await _sqlService.ImportDataFromFile(filePath, DataTypes.InternetUse);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            var contentDisposition = new ContentDisposition
            {
                FileName = Path.GetFileName(filePath),
                Inline = false
            };
            Response.Headers[HeaderNames.ContentDisposition] = contentDisposition.ToString();
            return PhysicalFile(filePath, "text/plain");
        }
    }
}
