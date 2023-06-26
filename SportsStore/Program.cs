using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.DataProvider;
using SportsStore.DataProvider.Interfaces;
using SportsStore.DataProvider.Repository;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDBContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration["ConnectionStrings:SportsStoreConnDev"]);
});

builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.UseStaticFiles();
app.UseSession();

app.MapControllerRoute("catpage", "{category}/Page/{productPage:int}",
    new { Controller = "Home", action = "Index" });

app.MapControllerRoute("page", "Page/{productPage:int}",
    new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("category", "{category}",
    new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("pagination", "Products/Page/{productPage}", 
    new { Controller = "Home", action = "Index" });

app.MapDefaultControllerRoute();
app.MapRazorPages();

SeedData.EnsurePopulated(app);

app.Run();
