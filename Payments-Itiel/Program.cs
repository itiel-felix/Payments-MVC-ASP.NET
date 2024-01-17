

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Payments_Itiel;
using Payments_Itiel.Entities;

var builder = WebApplication.CreateBuilder(args);

var userAuthenticity = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser().Build();
// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter(userAuthenticity));
});

builder.Services.AddAuthentication();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, options =>
{
    options.LoginPath = "/User/Login";
    options.AccessDeniedPath = "/User/Register";
});

// Add Db Context to controllers
// Dependency injection
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer("name=DefaultConnection");
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LogoutPath = "/User/Register"; // Especifica la ruta deseada después del logout
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
