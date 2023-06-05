using IS_Projekt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;
using IS_Projekt.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("import/internetuse")] //horrible temporary solution just for testing
        public async Task<IActionResult> ImportInternetUse(IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    return BadRequest();
                }
                string filePath = "./Resources/internetuse.xml";
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                var data = await _xmlService.ImportDataFromFile<InternetUse>(filePath);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "\n" + ex.StackTrace);
            }
        }
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("import/ecommerce")] //horrible temporary solution just for testing
        public async Task<IActionResult> ImportECommerce(IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    return BadRequest();
                }

                string filePath = "./Resources/ecommerce.xml";
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                var data = await _xmlService.ImportDataFromFile<ECommerce>(filePath);
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message + "\n" + ex.StackTrace);
            }
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
