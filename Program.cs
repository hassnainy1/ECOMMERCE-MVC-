using E_commerce_Website.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<myContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection")));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});

builder.Services.AddRazorPages();  // Add Razor Pages support

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// Route configuration for Order controller
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Order}/{action=Orders}/{id?}");  // Default route to Order/Orders

app.MapControllerRoute(
    name: "admin",
    pattern: "admin/{controller=Admin}/{action=Index}/{id?}");  // Admin controller route

app.MapControllerRoute(
    name: "home",
    pattern: "home/{controller=Home}/{action=Index}/{id?}");  // Home controller route

// Map Razor Pages routes
app.MapRazorPages();  // Add this line for Razor Pages

app.Run();

