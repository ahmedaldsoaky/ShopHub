using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using myshop.BLL.Mapping;
using myshop.DAL.Context;
using myshop.Entities.Models;
using myshop.Web.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    )) ;

builder.Services.AddIdentity<ApplicationUser,IdentityRole>(
    options=>
    {
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(4);
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 4;
    }
    ).AddDefaultTokenProviders().AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();


///
/// 
/// ///  Dependency Injection for UnitOfWork and Repositories
builder.Services.AddDataAccess();

builder.Services.AddAutoMapper(
    cfg => { },
    AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddHttpContextAccessor();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
var app = builder.Build();
// ask for it
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
    await IdentitySeeder.SeedRolesAsync(scope.ServiceProvider);
    await IdentitySeeder.SeedAdminAsync(scope.ServiceProvider);
}





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

app.UseSession();

app.MapRazorPages();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();

