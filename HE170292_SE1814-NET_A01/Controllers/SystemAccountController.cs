using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using HE170292_SE1814_NET_A01.Models;
using HE170292_SE1814_NET_A01.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HE170292_SE1814_NET_A01.Controllers
{
    public class SystemAccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly FunewsManagementContext _dbContext;
        private readonly SystemAccountService _systemAccountsService;
        private readonly ILogger<SystemAccountController> _logger;

        public SystemAccountController(ILogger<SystemAccountController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _systemAccountsService = new SystemAccountService();
            _dbContext = new FunewsManagementContext();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Index()
        {
            var accounts = _systemAccountsService.GetSystemAccounts();
            ViewBag.Accounts = accounts;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(SystemAccount account)
        {
            Console.WriteLine($"AccountId before saving: {account}");
            bool success = _systemAccountsService.CreateSystemAccount(account);

            if (!success)
            {
                return BadRequest("Could not create account.");
            }

            return RedirectToAction("Index", "accounts");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Update(short id)
        {
            var account = _systemAccountsService.GetSystemAccountById(id);
            if (account == null)
            {
                return NotFound();
            }

            ViewBag.account = account;
            ViewBag.Roles = new SelectList(new[]
            {
                new { Value = 1, Text = "Staff" },
                new { Value = 2, Text = "Lecturer" },
                new { Value = 3, Text = "Admin" }
            }, "Value", "Text", account.AccountRole);

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(SystemAccount account)
        {
            bool success = _systemAccountsService.UpdateSystemAccount(account);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "accounts");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(short id)
        {
            bool success = _systemAccountsService.DeleteSystemAccountById(id);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "accounts");
        }























































        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var adminEmail = _configuration["AdminAccount:AccountEmail"];
            var adminPassword = _configuration["AdminAccount:AccountPassword"];
            var adminRole = _configuration["AdminAccount:AccountRole"];

            if (email == adminEmail && password == adminPassword)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _configuration["AdminAccount:AccountName"]),
                new Claim(ClaimTypes.Email, adminEmail),
                new Claim(ClaimTypes.Role, adminRole) 
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home"); 
            }

            var user = CheckUserInDatabase(email, password); 
            if (user != null)
            {
                var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.AccountName),
                new Claim(ClaimTypes.Email, user.AccountEmail),
                new Claim(ClaimTypes.Role, user.AccountRole == 1 ? "Staff" :
                                              user.AccountRole == 2 ? "Lecturer" : "Admin")
            };

                var userClaimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userClaimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Email hoặc mật khẩu không đúng.");
            return View();
        }

        private SystemAccount CheckUserInDatabase(string email, string password)
        {
            return _dbContext.SystemAccounts.FirstOrDefault(u => u.AccountEmail == email && u.AccountPassword == password);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "SystemAccount");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
