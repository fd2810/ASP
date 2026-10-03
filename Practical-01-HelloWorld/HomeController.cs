using Microsoft.AspNetCore.Mvc;
// For .NET Framework use: using System.Web.Mvc;

namespace HelloWebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public IActionResult Index()
        {
            return View();
        }

        // POST: Home
        [HttpPost]
        public IActionResult Index(string name)
        {
            ViewBag.Message = "Welcome " + name;
            return View();
        }
    }
}
