using System.Web.Mvc;
using DependencyInjectionDemo.Services;

namespace DependencyInjectionDemo.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService studentService;

        // Constructor Injection – Ninject will supply the object
        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public ActionResult Index()
        {
            var students = studentService.GetStudents();
            return View(students);
        }
    }
}
