using Blogy.Business.Extensions;
using Blogy.Business.Services.AiServices;
using Blogy.DataAccess.Extensions;
using Blogy.WebUI.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. IOC

builder.Services.AddServicesExt();
builder.Services.AddRepositoriesExt(builder.Configuration);
// Yapay Zeka Makale Servisini Tanıtıyoruz
builder.Services.AddScoped<AiArticleService>();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ValidationExceptionFilter>();
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/Index";
    options.AccessDeniedPath = "/Login/Index";
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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
