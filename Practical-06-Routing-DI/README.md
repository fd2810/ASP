# Practical 6 – URL Routing and Dependency Injection

**Aim:** Understand default routing and implement Dependency Injection using Ninject.

---

# Part A – URL Routing

## Step 1: Create Project

1. Create new project: **ASP.NET Web Application (.NET Framework)**
2. Name: `URLRoutingDemo`
3. Template: **MVC** + No Authentication

## Step 2: Create StudentController

### File: `Controllers/StudentController.cs`

```csharp
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
```

## Step 3: Test Routing

Run the project (**Ctrl + F5**).

| URL                              | Output                     |
|----------------------------------|----------------------------|
| `/Student/Index`                 | Welcome to Student Page    |
| `/Student/Details/101`           | Student ID is: 101         |

### How Routing Works

```
/Student/Details/101
   ↓
Controller = Student
Action     = Details
id         = 101
```

Default route (in `RouteConfig.cs`):

```csharp
routes.MapRoute(
    name: "Default",
    url: "{controller}/{action}/{id}",
    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
);
```

---

# Part B – Dependency Injection (using Ninject)

## Step 1: Create Project

Name: `DependencyInjectionDemo`  
Template: MVC

## Step 2: Install Ninject

In Package Manager Console:

```powershell
Install-Package Ninject
Install-Package Ninject.MVC5
```

## Step 3: Create Service Interface & Class

### File: `Services/IStudentService.cs`

```csharp
using System.Collections.Generic;

namespace DependencyInjectionDemo.Services
{
    public interface IStudentService
    {
        List<string> GetStudents();
    }
}
```

### File: `Services/StudentService.cs`

```csharp
using System.Collections.Generic;

namespace DependencyInjectionDemo.Services
{
    public class StudentService : IStudentService
    {
        public List<string> GetStudents()
        {
            return new List<string>
            {
                "Rahul",
                "Priya",
                "Amit",
                "Sneha"
            };
        }
    }
}
```

## Step 4: Create Controller that uses DI

### File: `Controllers/StudentController.cs`

```csharp
using System.Web.Mvc;
using DependencyInjectionDemo.Services;

namespace DependencyInjectionDemo.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService studentService;

        // Constructor Injection
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
```

## Step 5: Configure Ninject

### File: `App_Start/NinjectConfig.cs`

```csharp
using Ninject;
using Ninject.Web.Common;
using System.Web.Mvc;
using DependencyInjectionDemo.Services;

namespace DependencyInjectionDemo.App_Start
{
    public class NinjectConfig
    {
        public static void RegisterDependencies()
        {
            var kernel = new StandardKernel();

            // Bind interface to concrete class
            kernel.Bind<IStudentService>().To<StudentService>();

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public object GetService(System.Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public System.Collections.Generic.IEnumerable<object> GetServices(System.Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}
```

## Step 6: Register in Global.asax.cs

```csharp
using DependencyInjectionDemo.App_Start;

protected void Application_Start()
{
    AreaRegistration.RegisterAllAreas();
    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    RouteConfig.RegisterRoutes(RouteTable.Routes);
    BundleConfig.RegisterBundles(BundleTable.Bundles);

    // Start Ninject
    NinjectConfig.RegisterDependencies();
}
```

## Step 7: Create View

### File: `Views/Student/Index.cshtml`

```html
@model List<string>

<h2>Student List</h2>
<ul>
@foreach (var student in Model)
{
    <li>@student</li>
}
</ul>
```

## Step 8: Run

Open: `/Student/Index`

**Output:**
- Rahul
- Priya
- Amit
- Sneha
