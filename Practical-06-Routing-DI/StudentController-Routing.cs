using System.Web.Mvc;

namespace URLRoutingDemo.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            return Content("Welcome to Student Page");
        }

        public ActionResult Details(int id)
        {
            return Content("Student ID is: " + id);
        }
    }
}
