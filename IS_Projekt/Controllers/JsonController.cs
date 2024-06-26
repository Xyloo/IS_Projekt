﻿using IS_Projekt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;
using IS_Projekt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace IS_Projekt.Controllers
{
    [ApiController]
    [Route("api/json")]
    public class JsonController : ControllerBase, IDataFileController
    {
        private readonly IJsonService _jsonService;
        public JsonController(IJsonService jsonService)
        {
            _jsonService = jsonService;
        }
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("import/ecommerce")] //horrible temporary solution just for testing
        public async Task<IActionResult> ImportECommerce(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest();
            }

            string filePath = "./ecommerce.json";

            //saving file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var data = await _jsonService.ImportDataFromFile<ECommerce>(filePath);
            return Ok();
        }
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("import/internetuse")] //horrible temporary solution just for testing
        public async Task<IActionResult> ImportInternetUse(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest();
            }

            string filePath = "./internetuse.json";

            //saving file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var data = await _jsonService.ImportDataFromFile<InternetUse>(filePath);
            return Ok();
        }

        [HttpGet("export/ecommerce")]
        public async Task<IActionResult> ExportECommerce()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "ecommerce_download.json");

            await _jsonService.ExportDataToFile<ECommerce>(filePath);

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
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "internetuse_download.json");
            await _jsonService.ExportDataToFile<InternetUse>(filePath);

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
