using System.Web.Mvc;

namespace StudentMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Student()
        {
            ViewBag.Name = "Rahul";
            ViewBag.Course = "B.Sc. Information Technology";
            ViewBag.RollNo = 101;

            return View();
        }
    }
}
