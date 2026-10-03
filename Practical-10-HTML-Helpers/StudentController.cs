using System.Web.Mvc;
using StudentRegistration.Models;

namespace StudentRegistration.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Student/Register
        [HttpPost]
        public ActionResult Register(Student student)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Student registered successfully!";
                return View();
            }

            return View(student);
        }
    }
}
