using Microsoft.AspNetCore.Mvc;

namespace IS_Projekt.Controllers
{
    public interface IDataController
    {
        public Task<IActionResult> ImportECommerce();
        public Task<IActionResult> ImportInternetUse();
        public Task<IActionResult> ExportECommerce();
        public Task<IActionResult> ExportInternetUse();
    }
}
