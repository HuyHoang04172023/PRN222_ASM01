using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using HE170292_SE1814_NET_A01.Models;

namespace HE170292_SE1814_NET_A01.Controllers
{
    public class SystemAccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly FunewsManagementContext _dbContext;

        public SystemAccountController(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new FunewsManagementContext();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra thông tin đăng nhập
                var user = _dbContext.SystemAccounts
                    .FirstOrDefault(u => u.AccountEmail == model.Email && u.AccountPassword == model.Password);

                if (user != null)
                {
                    // Lấy Admin Role từ appsettings
                    var adminRole = int.Parse(_configuration["AdminSettings:AdminRole"]);

                    // Tạo Claims
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.AccountName),
                    new Claim(ClaimTypes.Email, user.AccountEmail),
                    new Claim(ClaimTypes.Role, user.AccountRole == adminRole ? "Admin" :
                                               user.AccountRole == 2 ? "Lecturer" : "Staff")
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true // Ghi nhớ đăng nhập
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Email hoặc mật khẩu không đúng.");
            }

            return View(model);
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
