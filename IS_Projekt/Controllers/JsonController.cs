using IS_Projekt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;

namespace IS_Projekt.Controllers
{
    [ApiController]
    [Route("api/json")]
    public class JsonController : ControllerBase
    {
        private readonly IFileService _jsonService;
        public JsonController(IJsonService jsonService)
        {
            _jsonService = jsonService;
        }

        [HttpGet("/import/ecommerce")] //horrible temporary solution just for testing
        public async Task<IActionResult> ImportECommerce()
        {
            var xml = await _jsonService.ImportECommerceDataFromFile("./Resources/commerce.json");
            return Ok(xml);
        }

        [HttpGet("/import/internetuse")] //horrible temporary solution just for testing
        public async Task<IActionResult> ImportInternetUse()
        {
            var xml = await _jsonService.ImportInternetUseDataFromFile("./Resources/internetuse.json");
            return Ok(xml);
        }



        [HttpGet("/export/ecommerce")]
        public async Task<IActionResult> ExportECommerce()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources/ecommerce_download.json");
            var mimeType = "application/json";

            await _jsonService.ExportECommerceDataToFile("./Resources/ecommerce_download.json");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            // Set the content disposition header so the browser knows it's a file.
            var contentDisposition = new ContentDisposition
            {
                FileName = Path.GetFileName(filePath),
                Inline = false  // false = prompt the user for download
            };
            Response.Headers[HeaderNames.ContentDisposition] = contentDisposition.ToString();


            return PhysicalFile(filePath, "application/json");
        }


        [HttpGet("/export/internetuse")]
        public async Task<IActionResult> ExportInternetUse()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources/internetuse_download.json");
            await _jsonService.ExportInternetUseDataToFile("./Resources/internetuse_download.json");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            // Set the content disposition header so the browser knows it's a file.
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
