using System.Web.Mvc;

namespace StudentMVC.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Student Management";
            return View();
        }

        public ActionResult Details()
        {
            ViewBag.Name = "Priya";
            ViewBag.Course = "B.Sc. Information Technology";
            ViewBag.RollNo = 102;
            return View();
        }
    }
}
