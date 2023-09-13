using CafeAdminPanelDB.CustomActionFilter;
using CafeAdminPanelDB.Models;
using CafeAdminPanelDB.ViewModel.UserVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CafeAdminPanelDB.Controllers
{

  
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM loginVM)
        {
            string jsonUser=JsonConvert.SerializeObject(loginVM);
            HttpContext.Session.SetString("loginUser", jsonUser);
            return RedirectToAction(nameof(Index),"Admin");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}