# Practical 6 – URL Routing and Dependency Injection

**Aim:**  
1. Understand how URL Routing works in ASP.NET MVC.  
2. Implement Dependency Injection (DI) using Ninject so that the controller does not create its own objects.

---

# Part A – URL Routing

## Step 1: Create the Project

1. Open **Visual Studio 2019**.
2. Click **Create a new project**.
3. Search for: `ASP.NET Web Application`
4. Select **ASP.NET Web Application (.NET Framework)**.
5. Click **Next**.
6. Configure:
   - **Project name:** `URLRoutingDemo`
   - **Location:** Choose any folder
   - **Framework:** .NET Framework 4.7.2
7. Click **Create**.
8. Select **MVC**.
9. Authentication: **No Authentication**.
10. Click **Create**.

---

## Step 2: Create StudentController

1. In **Solution Explorer**, right-click the **Controllers** folder.
2. Select **Add → Controller**.
3. Choose **MVC 5 Controller - Empty**.
4. Click **Add**.
5. Enter name: `StudentController`
6. Click **Add**.

Visual Studio creates `Controllers/StudentController.cs`.

### Replace the entire code with:

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

(You can also copy the file `StudentController-Routing.cs` from this folder.)

---

## Step 3: Test the First URL

1. Press **Ctrl + F5** to run the project.
2. Suppose the browser opens at `https://localhost:44300/`
3. In the address bar type:

```
https://localhost:44300/Student/Index
```

**Expected Output:**
```
Welcome to Student Page
```

### Why this works?

The URL `/Student/Index` is mapped as:
- Controller → `Student`
- Action → `Index`

So MVC executes `StudentController.Index()`.

---

## Step 4: Test URL with Parameter

In the browser address bar type:

```
https://localhost:44300/Student/Details/101
```

**Expected Output:**
```
Student ID is: 101
```

### How routing maps it

```
/Student/Details/101
     ↓
Controller = Student
Action     = Details
id         = 101
```

The default route is defined in `App_Start/RouteConfig.cs`:

```csharp
routes.MapRoute(
    name: "Default",
    url: "{controller}/{action}/{id}",
    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
);
```

---

# Part B – Dependency Injection (using Ninject)

## Step 1: Create a New Project for DI

1. Create another project the same way as above.
2. **Project name:** `DependencyInjectionDemo`
3. Template: **MVC** + No Authentication.

---

## Step 2: Install Ninject Packages

1. Go to **Tools → NuGet Package Manager → Package Manager Console**.
2. Run these two commands one by one:

```powershell
Install-Package Ninject
Install-Package Ninject.MVC5
```

---

## Step 3: Create the Service Interface

1. Right-click the project name in Solution Explorer.
2. Select **Add → New Folder**.
3. Name the folder: `Services`
4. Right-click the **Services** folder → **Add → Class**.
5. Name: `IStudentService.cs`
6. Click **Add**.

### Replace the code with:

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

---

## Step 4: Create the Service Class (Implementation)

1. Right-click the **Services** folder → **Add → Class**.
2. Name: `StudentService.cs`
3. Click **Add**.

### Replace the code with:

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

---

## Step 5: Create StudentController that uses Dependency Injection

1. Right-click **Controllers** folder → **Add → Controller**.
2. Choose **MVC 5 Controller - Empty**.
3. Name: `StudentController`
4. Click **Add**.

### Replace the entire code with:

```csharp
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
```

---

## Step 6: Create Ninject Configuration Class

1. Right-click the project → **Add → New Folder**.
2. Name the folder: `App_Start` (if it does not already exist).
3. Right-click **App_Start** → **Add → Class**.
4. Name: `NinjectConfig.cs`
5. Click **Add**.

### Replace the code with:

```csharp
using Ninject;
using System.Web.Mvc;
using DependencyInjectionDemo.Services;

namespace DependencyInjectionDemo.App_Start
{
    public class NinjectConfig
    {
        public static void RegisterDependencies()
        {
            var kernel = new StandardKernel();

            // Tell Ninject: when someone asks for IStudentService, give StudentService
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

---

## Step 7: Register Ninject in Global.asax.cs

1. Open the file **Global.asax.cs** (it is in the root of the project).
2. Add this line at the top with the other `using` statements:

```csharp
using DependencyInjectionDemo.App_Start;
```

3. Inside the `Application_Start()` method, add this line at the end:

```csharp
NinjectConfig.RegisterDependencies();
```

Your complete `Application_Start` method should look like this:

```csharp
protected void Application_Start()
{
    AreaRegistration.RegisterAllAreas();
    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    RouteConfig.RegisterRoutes(RouteTable.Routes);
    BundleConfig.RegisterBundles(BundleTable.Bundles);

    // Start Dependency Injection
    NinjectConfig.RegisterDependencies();
}
```

---

## Step 8: Create the View

1. Open `StudentController.cs`.
2. Right-click inside the `Index()` method.
3. Select **Add View...**
4. Settings:
   - **View name:** `Index`
   - **Template:** Empty (without model)
5. Click **Add**.

Visual Studio creates `Views/Student/Index.cshtml`.

### Replace the content of the view with:

```html
@model List<string>

@{
    ViewBag.Title = "Student List";
}

<h2>Student List</h2>

<ul>
@foreach (var student in Model)
{
    <li>@student</li>
}
</ul>
```

---

## Step 9: Build and Run

1. Press **Ctrl + Shift + B** to build the solution (make sure there are no errors).
2. Press **Ctrl + F5** to run.
3. In the browser open:

```
/Student/Index
```

**Expected Output:**
```
Student List
• Rahul
• Priya
• Amit
• Sneha
```

---

## What is Dependency Injection?

- The controller **does not create** the `StudentService` object itself.
- Ninject **injects** (gives) the object through the constructor.
- This makes the code loosely coupled and easier to test.

---

## Files in this Practical Folder

| File                              | Where to put it                  |
|-----------------------------------|----------------------------------|
| `StudentController-Routing.cs`    | Controllers (for Part A)         |
| `IStudentService.cs`              | Services folder                  |
| `StudentService.cs`               | Services folder                  |
| `StudentController-DI.cs`         | Controllers (for Part B)         |
| `NinjectConfig.cs`                | App_Start folder                 |
| `Index.cshtml`                    | Views/Student/                   |
