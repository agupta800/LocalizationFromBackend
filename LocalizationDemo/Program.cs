using Arebis.Core.AspNet.Mvc.Localization;
using Arebis.Core.Localization;
using LocalizationDemo.Localize;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Localization

builder.Services
    .AddDbContext<LocalizationDemo.Data.Localize.LocalizeDbContext>(
    optionsAction: options => options.UseSqlServer(
      builder.Configuration.GetConnectionString("conn")));

builder.Services
    .AddTransient<ILocalizationSource, DbContextLocalizationSource>();

builder.Services
    .AddLocalizationFromSource(builder.Configuration, options => {
        options.Domains = new string[] { "MyApp" };
        options.CacheFileName = "LocalizationCache.json";
        options.UseOnlyReviewedLocalizationValues = false;
        options.AllowLocalizeFormat = true;
    });

builder.Services.AddModelBindingLocalizationFromSource();

builder.Services.AddControllers(config =>
{
    config.Filters.Add<ModelStateLocalizationFilter>();
});

#endregion

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddDataAnnotationsLocalizationFromSource();
builder.Services.AddTransient<ITranslationService, BingTranslationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days...
    app.UseHsts();
}

#region Request Localization


var supportedCultures = new[] { "en", "en-US", "fr", "fr-CA", "ne", "ne-NP" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

#endregion

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();