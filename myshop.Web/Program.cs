using myshop.BLL.Extensions.DependencyInjection;
using myshop.Web.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireRole("Admin");
    });
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

///    ==== -->>>   ///  Dependency Injection for UnitOfWork and Repositories
builder.Services.AddDependances(builder.Configuration);



builder.Services.AddAutoMapper(
    cfg => { },
    AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IOTimeout = TimeSpan.FromMinutes(1);
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

Console.WriteLine(app.Environment.WebRootPath);
Console.WriteLine(app.Environment.ContentRootPath);


// initialize the application (e.g., seed data, create roles, etc.)
await app.Services.InitializeApplicationAsync();


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


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}"); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

