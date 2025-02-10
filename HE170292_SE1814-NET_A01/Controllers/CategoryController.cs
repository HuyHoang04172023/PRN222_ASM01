using HE170292_SE1814_NET_A01.Models;
using HE170292_SE1814_NET_A01.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HE170292_SE1814_NET_A01.Controllers
{
    
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoriesService;

        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
            _categoriesService = new CategoryService();
        }

        [Authorize(Roles = "Staff")]
        [HttpGet]
        public IActionResult Index()
        {
            var categories = _categoriesService.GetCategories();
            ViewBag.Categories = categories;
            return View();
        }

        [Authorize(Roles = "Staff")]
        [HttpGet]
        public IActionResult Create()
        {
            var categories = _categoriesService.GetCategories();
            ViewBag.Categories = categories;
            return View();
        }

        [Authorize(Roles = "Staff")]
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            bool success = _categoriesService.CreateCategory(category);

            if (success)
            {
                return RedirectToAction("Index", "categories");
            }

            ModelState.AddModelError("", "Failed to create category. Please try again.");
            return View(category);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var categories = _categoriesService.GetCategories();
            ViewBag.Categories = categories;

            var category = _categoriesService.GetCategoryById(id);
            if (category == null)
            {
                TempData["ErrorMessage"] = "Category not found.";
                return RedirectToAction("Index", "categories");
            }

            ViewBag.Categories = new SelectList(_categoriesService.GetCategories(), "CategoryId", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category updatedCategory)
        {
            if (!ModelState.IsValid)
            {
                var categories = _categoriesService.GetCategories();
                ViewBag.Categories = categories;
                return View(updatedCategory);
            }

            bool success = _categoriesService.UpdateCategory(updatedCategory);

            if (success)
            {
                TempData["SuccessMessage"] = "Category updated successfully.";
                return RedirectToAction("Index", "categories");
            }

            TempData["ErrorMessage"] = "Failed to update category. Please try again.";
            return View(updatedCategory);
        }

        [Authorize(Roles = "Staff")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            bool success = _categoriesService.DeleteCategory(id);

            if (!success)
            {
                TempData["ErrorMessage"] = "Cannot delete this category because it is either not found or is linked to news articles.";
                return RedirectToAction("Index", "categories");
            }

            TempData["SuccessMessage"] = "Category deleted successfully.";
            return RedirectToAction("Index", "categories");
        }
    }
}
