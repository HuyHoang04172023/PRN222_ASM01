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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

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
    name: "GetCategory",
    pattern: "categories/{id?}",
    defaults: new { controller = "Category", action = "Update" }
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

app.Run();
