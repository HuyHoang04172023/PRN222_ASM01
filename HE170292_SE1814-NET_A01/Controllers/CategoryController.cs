using HE170292_SE1814_NET_A01.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HE170292_SE1814_NET_A01.Controllers
{
    [Authorize(Roles = "Staff")]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoriesService;

        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
            _categoriesService = new CategoryService();
        }
        public IActionResult Index()
        {
            var categories = _categoriesService.GetCategories();
            return View(categories);
        }
    }
}
