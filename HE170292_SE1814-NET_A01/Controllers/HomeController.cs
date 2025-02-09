using System.Diagnostics;
using HE170292_SE1814_NET_A01.Models;
using HE170292_SE1814_NET_A01.Services;
using Microsoft.AspNetCore.Mvc;

namespace HE170292_SE1814_NET_A01.Controllers
{
    public class HomeController : Controller
    {
        private readonly CategoryService _categoriesService;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _categoriesService = new CategoryService();
        }

        public IActionResult Index()
        {
            var categories = _categoriesService.GetCategories();
            return View(categories);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
