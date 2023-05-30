using IS_Projekt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;

namespace IS_Projekt.Controllers
{
    [ApiController]
    [Route("api/sql")]
    public class SqlController : ControllerBase
    {
        private readonly ISqlService _sqlService;
        public SqlController(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        [HttpGet("import/ecommerce")] 
        public async Task<IActionResult> ImportECommerce()
        {
            await _sqlService.ImportECommerceDataFromFile("./Resources/ECommerce_sql.sql");
            return Ok();
        }

        [HttpGet("import/internetuse")] //horrible temporary solution just for testing
        public async Task<IActionResult> ImportInternetUse()
        {
            await _sqlService.ImportInternetUseDataFromFile("./Resources/InternetUse_sql.sql");
            return Ok();
        }

        [HttpGet("export/ecommerce")]
        public async Task<IActionResult> ExportECommerce()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources/ecommerce_sql.txt");
            await _sqlService.ExportECommerceDataToFile(filePath);
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
            await _sqlService.ExportInternetUseDataToFile(filePath);
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
