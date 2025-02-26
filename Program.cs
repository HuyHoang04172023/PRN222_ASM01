using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/AaccessDenied";
    });

builder.Services.AddAuthorization();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "staff",
    pattern: "staff",
    defaults: new { controller = "Home", action = "Staff" }
);

app.MapControllerRoute(
    name: "lecturer",
    pattern: "lecturer",
    defaults: new { controller = "Home", action = "Lecturer" }
);

app.MapControllerRoute(
    name: "admin",
    pattern: "admin",
    defaults: new { controller = "Home", action = "Admin" }
);

app.MapControllerRoute(
    name: "login",
    pattern: "login",
    defaults: new { controller = "SystemAccount", action = "Login" }
);

app.MapControllerRoute(
    name: "logout",
    pattern: "logout",
    defaults: new { controller = "SystemAccount", action = "Logout" }
);

app.MapControllerRoute(
    name: "AaccessDenied",
    pattern: "AaccessDenied",
    defaults: new { controller = "SystemAccount", action = "AccessDenied" }
);

//Categories Route
app.MapControllerRoute(
    name: "categories",
    pattern: "categories",
    defaults: new { controller = "Category", action = "Index" }
);

app.MapControllerRoute(
    name: "CreateCategory",
    pattern: "categories/create",
    defaults: new { controller = "Category", action = "Create" }
);

app.MapControllerRoute(
    name: "DeleteCategory",
    pattern: "categories/delete/{id?}",
    defaults: new { controller = "Category", action = "Delete" }
);

app.MapControllerRoute(
    name: "GetCategory",
    pattern: "categories/{id?}",
    defaults: new { controller = "Category", action = "Update" }
);

//System Account Route
app.MapControllerRoute(
    name: "accounts",
    pattern: "accounts",
    defaults: new { controller = "SystemAccount", action = "Index" }
);

app.MapControllerRoute(
    name: "CreateAccount",
    pattern: "accounts/create",
    defaults: new { controller = "SystemAccount", action = "Create" }
);

app.MapControllerRoute(
    name: "GetAccount",
    pattern: "accounts/{id?}",
    defaults: new { controller = "SystemAccount", action = "Update" }
);

app.MapControllerRoute(
    name: "DeleteAccount",
    pattern: "accounts/delete/{id?}",
    defaults: new { controller = "SystemAccount", action = "Delete" }
);

//Staff User Proflie Route
app.MapControllerRoute(
    name: "Profile",
    pattern: "profile",
    defaults: new { controller = "SystemAccount", action = "GetProfile" }
);

//News Article Route
app.MapControllerRoute(
    name: "ReportNewsArticle",
    pattern: "news-articles/report",
    defaults: new { controller = "NewsArticle", action = "Report" }
);

app.MapControllerRoute(
    name: "HistoryNewsArticle",
    pattern: "news-articles/history",
    defaults: new { controller = "NewsArticle", action = "History" }
);

app.MapControllerRoute(
    name: "ActiveNewsArticle",
    pattern: "news-articles/active",
    defaults: new { controller = "NewsArticle", action = "Active" }
);

app.MapControllerRoute(
    name: "news-articles",
    pattern: "news-articles",
    defaults: new { controller = "NewsArticle", action = "Index" }
);

app.MapControllerRoute(
    name: "create-news-articles",
    pattern: "news-articles/create",
    defaults: new { controller = "NewsArticle", action = "Create" }
);

app.MapControllerRoute(
    name: "GetNewsArticle",
    pattern: "news-articles/{id?}",
    defaults: new { controller = "NewsArticle", action = "Update" }
);

app.MapControllerRoute(
    name: "DeleteNewsArticle",
    pattern: "news-articles/delete/{id?}",
    defaults: new { controller = "NewsArticle", action = "Delete" }
);



app.Run();
