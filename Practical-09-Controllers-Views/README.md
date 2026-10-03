# Practical 9 – Creating an Application with Controllers and Views

**Aim:** Understand how Controllers and Views work together in ASP.NET MVC. Pass data using ViewBag and create multiple controllers.

---

## Step 1: Create Project

1. Create new project: **ASP.NET Web Application (.NET Framework)**
2. Name: `StudentMVC`
3. Template: **MVC** + No Authentication

---

## Step 2: Understand Existing HomeController

Open `Controllers/HomeController.cs`. You will see:

```csharp
public ActionResult Index()
{
    return View();
}
```

This returns the view `Views/Home/Index.cshtml`.

---

## Step 3: Add Student Action in HomeController

### Update `Controllers/HomeController.cs`

```csharp
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
```

---

## Step 4: Create Student View

1. Right-click inside the `Student()` method → **Add View**.
2. View name: `Student`
3. Template: **Empty (without model)**
4. Click **Add**.

### File: `Views/Home/Student.cshtml`

```html
@{
    ViewBag.Title = "Student Information";
}

<h2>Student Information</h2>
<p><strong>Name:</strong> @ViewBag.Name</p>
<p><strong>Roll Number:</strong> @ViewBag.RollNo</p>
<p><strong>Course:</strong> @ViewBag.Course</p>
```

---

## Step 5: Run

Press **Ctrl + F5**.

Open: `http://localhost:xxxxx/Home/Student`

**Output:**
```
Student Information
Name: Rahul
Roll Number: 101
Course: B.Sc. Information Technology
```

---

## Step 6: Create a Second Controller

1. Right-click **Controllers** → **Add → Controller**.
2. Select **MVC 5 Controller - Empty**.
3. Name: `StudentController`.

### File: `Controllers/StudentController.cs`

```csharp
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
```

---

## Step 7: Create Details View

Right-click `Details()` → **Add View** → Name: `Details` → Empty.

### File: `Views/Student/Details.cshtml`

```html
@{
    ViewBag.Title = "Student Details";
}

<h2>Student Details</h2>
<p>Name: @ViewBag.Name</p>
<p>Roll Number: @ViewBag.RollNo</p>
<p>Course: @ViewBag.Course</p>
```

---

## Step 8: Link from Home Page

Open `Views/Home/Index.cshtml` and add:

```html
<h2>Welcome to Student MVC Application</h2>

@Html.ActionLink("View Student Details", "Details", "Student")
```

---

## Step 9: Test

| URL                    | Result                          |
|------------------------|---------------------------------|
| `/Home/Student`        | Rahul's information             |
| `/Student/Details`     | Priya's information             |

---

## How MVC Flow Works

```
Browser → /Student/Details
          ↓
     StudentController
          ↓
      Details() action
          ↓
  Views/Student/Details.cshtml
          ↓
      HTML Response
```
