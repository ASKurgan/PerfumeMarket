using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PerfumeMarket.DAL;
using PerfumeMarket.DAL.Interfaces;
using PerfumeMarket.DAL.Repositories;
using PerfumeMarket.Domain.Entity;
using PerfumeMarket.Service.Implementations;
using PerfumeMarket.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
    });

builder.Services.AddScoped<IBaseRepository<Perfume>, PerfumeRepository>();
builder.Services.AddScoped<IBaseRepository<Basket>, BasketRepository>();
builder.Services.AddScoped<IBaseRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IBaseRepository<Profile>, ProfileRepository>();
builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();

builder.Services.AddScoped<IPerfumeService, PerfumeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IBasketService, BasketService>();
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
