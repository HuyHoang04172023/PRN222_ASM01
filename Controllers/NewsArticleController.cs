using HE170292_SE1814_NET_A01.Models;
using HE170292_SE1814_NET_A01.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HE170292_SE1814_NET_A01.Controllers
{
    public class NewsArticleController : Controller
    {
        private readonly NewArticleService _newsArticleService;
        private readonly CategoryService _categoriesService;
        private readonly TagService _tagsService;


        private readonly ILogger<NewsArticleController> _logger;

        public NewsArticleController(ILogger<NewsArticleController> logger)
        {
            _logger = logger;
            _newsArticleService = new NewArticleService();
            _categoriesService = new CategoryService();
            _tagsService = new TagService();
        }

        [Authorize(Roles = "Staff")]
        [HttpGet]
        public IActionResult Index()
        {
            var articles = _newsArticleService.GetNewsArticles();
            ViewBag.Articles = articles;
            return View();
        }

        [Authorize(Roles = "Staff")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _categoriesService.GetCategories();
            ViewBag.Tags = _tagsService.GetTags();
            return View();
        }

        [Authorize(Roles = "Staff")]
        [HttpPost]
        public IActionResult Create(NewsArticle article, List<int> selectedTags)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please check the input fields.";
                ViewBag.Categories = _categoriesService.GetCategories();
                ViewBag.Tags = _tagsService.GetTags();
                return View(article);
            }

            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var success = _newsArticleService.CreateNewsArticle(article, selectedTags, userId);

                if (success)
                {
                    TempData["SuccessMessage"] = "Article created successfully.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database Error: " + ex.Message);
                TempData["ErrorMessage"] = "An error occurred while creating the article.";
            }

            ViewBag.Categories = _categoriesService.GetCategories();
            ViewBag.Tags = _tagsService.GetTags();
            return View(article);
        }

        [Authorize(Roles = "Staff")]
        [HttpGet]
        public IActionResult Update(string id)
        {
            var article = _newsArticleService.GetNewsArticleById(id);
            if (article == null)
            {
                TempData["ErrorMessage"] = "News article not found.";
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categoriesService.GetCategories();
            ViewBag.Tags = _tagsService.GetTags();
            return View(article);
        }

        [Authorize(Roles = "Staff")]
        [HttpPost]
        public IActionResult Update(NewsArticle updatedArticle, List<int> selectedTags)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please check the input fields.";
                ViewBag.Categories = _categoriesService.GetCategories();
                ViewBag.Tags = _tagsService.GetTags();
                return View(updatedArticle);
            }

            var userId = HttpContext.Session.GetInt32("UserId");

            try
            {
                var success = _newsArticleService.UpdateNewsArticle(updatedArticle, selectedTags, userId);

                if (success)
                {
                    TempData["SuccessMessage"] = "Article updated successfully.";
                    return Redirect("/news-articles");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database Error: " + ex.Message);
                TempData["ErrorMessage"] = "An error occurred while updating the article.";
            }

            ViewBag.Categories = _categoriesService.GetCategories();
            ViewBag.Tags = _tagsService.GetTags();
            return View(updatedArticle);
        }

        [Authorize(Roles = "Staff")]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            try
            {
                var success = _newsArticleService.DeleteNewsArticleById(id);

                if (success)
                {
                    TempData["SuccessMessage"] = "Article deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to delete article. It might be in use.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database Error: " + ex.Message);
                TempData["ErrorMessage"] = "An error occurred while deleting the article.";
            }

            return Redirect("/news-articles");
        }

        [Authorize(Roles = "Staff")]
        [HttpGet]
        public IActionResult History()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You must log in to view your news history.";
                return Redirect("/login");
            }

            var articles = _newsArticleService.GetNewsArticlesByUser((short)userId);

            if (articles == null || !articles.Any())
            {
                TempData["InfoMessage"] = "You have not created any news articles yet.";
            }

            return View("History", articles);
        }

        [Authorize(Roles = "Lecturer")]
        [HttpGet]
        public IActionResult Active()
        {
            var articles = _newsArticleService.GetNewsArticlesActive();
            ViewBag.Articles = articles;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Report()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Report(DateTime startDate, DateTime endDate)
        {
            var articles = _newsArticleService.GetNewsReport(startDate, endDate);
            ViewBag.Articles = articles;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            return View();
        }

    }
}
