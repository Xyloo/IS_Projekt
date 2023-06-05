using Microsoft.AspNetCore.Mvc;

namespace IS_Projekt.Controllers
{
    public interface IDataFileController
    {
        public Task<IActionResult> ImportECommerce(IFormFile file);
        public Task<IActionResult> ImportInternetUse(IFormFile file);
        public Task<IActionResult> ExportECommerce();
        public Task<IActionResult> ExportInternetUse();
    }
}
