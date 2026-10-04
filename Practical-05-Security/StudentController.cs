using System.Web.Mvc;

namespace SecureMVCApp.Controllers
{
    [Authorize]   // This attribute protects the whole controller
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
