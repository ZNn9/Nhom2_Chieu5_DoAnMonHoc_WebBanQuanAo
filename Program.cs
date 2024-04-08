using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.DataAccess;
using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Models;
using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add and Use Database
builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();

// Add Database by Endentity
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection")));

/*builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
*/

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {})
        .AddDefaultUI()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

// 
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.LogoutPath = $"/Identity/Account/AccessDenid";
});


// Add Endpoint
builder.Services.AddEndpointsApiExplorer();

// Add Razor
builder.Services.AddRazorPages();

// Add Session đặt trước AddControllersWithViews
builder.Services.AddDistributedMemoryCache();

// Add Session đặt trước AddControllersWithViews
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Add Session
app.UseSession();

app.UseHttpsRedirection();  
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
   );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{area:exists}/controller=Admin}/{action=Index}/{id?}");
});*/

app.Run();
