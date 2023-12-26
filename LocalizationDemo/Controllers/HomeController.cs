using Arebis.Core.AspNet.Mvc.Localization;
using LocalizationDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace LocalizationDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer _localizer;


        public HomeController(ILogger<HomeController> logger, IStringLocalizer localizer)
        {
            _logger = logger;
            _localizer = localizer;

        }

        public IActionResult Index()
        {
         
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        // UpdatePerson action on HomeController:
        
        public IActionResult UpdatePerson(PersonModel model)
        {

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
