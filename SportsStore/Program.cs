using Microsoft.EntityFrameworkCore;
using SportsStore.DataProvider;
using SportsStore.DataProvider.Interfaces;
using SportsStore.DataProvider.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDBContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration["ConnectionStrings:SportsStoreConnDev"]);
});

builder.Services.AddScoped<IStoreRepository, StoreRepository>();

var app = builder.Build();


//app.MapGet("/", () => "Hello World!");
app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
