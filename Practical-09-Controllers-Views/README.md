# Practical 9 – Creating an Application with Controllers and Views

**Aim:** Understand how Controllers and Views work together in ASP.NET MVC. Pass data using ViewBag and create multiple controllers.

---

## Step 1: Create the Project

1. Open **Visual Studio 2019**.
2. Click **Create a new project**.
3. Search for: `ASP.NET Web Application`
4. Select **ASP.NET Web Application (.NET Framework)**.
5. Click **Next**.
6. Configure:
   - **Project name:** `StudentMVC`
   - **Location:** Choose any folder
   - **Framework:** .NET Framework 4.7.2
7. Click **Create**.
8. Select **MVC**.
9. Leave Authentication as **No Authentication**.
10. Click **Create**.

Visual Studio creates the complete MVC project structure.

---

## Step 2: Understand the Existing HomeController

1. In **Solution Explorer**, expand the **Controllers** folder.
2. Double-click **HomeController.cs**.

You will see code similar to this:

```csharp
public ActionResult Index()
{
    return View();
}
```

This means: when someone opens `/Home/Index`, MVC will show the file `Views/Home/Index.cshtml`.

---

## Step 3: Add a New Action – Student()

1. Still inside `HomeController.cs`, add a new method below the existing ones:

```csharp
public ActionResult Student()
{
    ViewBag.Name = "Rahul";
    ViewBag.Course = "B.Sc. Information Technology";
    ViewBag.RollNo = 101;

    return View();
}
```

Your `HomeController` should now look like this (you can also copy the full file `HomeController.cs` from this folder):

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

## Step 4: Create the View for Student Action

1. Inside `HomeController.cs`, **right-click** on the word `Student` (inside the method).
2. Select **Add View...**
3. In the dialog box:
   - **View name:** `Student`
   - **Template:** Empty (without model)
   - Leave other options default
4. Click **Add**.

Visual Studio creates:
```
Views
 └── Home
      └── Student.cshtml
```

### Open `Views/Home/Student.cshtml` and replace everything with:

```html
@{
    ViewBag.Title = "Student Information";
}

<h2>Student Information</h2>
<p><strong>Name:</strong> @ViewBag.Name</p>
<p><strong>Roll Number:</strong> @ViewBag.RollNo</p>
<p><strong>Course:</strong> @ViewBag.Course</p>
```

(You can also copy the file `Student.cshtml` from this folder.)

---

## Step 5: Run and Test the First Page

1. Press **Ctrl + F5**.
2. In the browser address bar type:

```
/Home/Student
```

**Expected Output:**
```
Student Information
Name: Rahul
Roll Number: 101
Course: B.Sc. Information Technology
```

---

## Step 6: Create a Second Controller

1. In **Solution Explorer**, right-click the **Controllers** folder.
2. Select **Add → Controller**.
3. Choose **MVC 5 Controller - Empty**.
4. Click **Add**.
5. Enter name: `StudentController`
6. Click **Add**.

---

## Step 7: Add Actions to StudentController

Open the new `StudentController.cs` and replace the code with:

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

(You can copy the file `StudentController.cs` from this folder.)

---

## Step 8: Create the Details View

1. Open `StudentController.cs`.
2. Right-click inside the `Details()` method.
3. Select **Add View...**
4. Settings:
   - **View name:** `Details`
   - **Template:** Empty (without model)
5. Click **Add**.

### Open `Views/Student/Details.cshtml` and replace with:

```html
@{
    ViewBag.Title = "Student Details";
}

<h2>Student Details</h2>
<p>Name: @ViewBag.Name</p>
<p>Roll Number: @ViewBag.RollNo</p>
<p>Course: @ViewBag.Course</p>
```

(You can copy the file `Details.cshtml` from this folder.)

---

## Step 9: Add a Link on the Home Page

1. Open `Views/Home/Index.cshtml`.
2. Add these lines:

```html
<h2>Welcome to Student MVC Application</h2>

@Html.ActionLink("View Student Details", "Details", "Student")
```

This creates a nice link that goes to `/Student/Details`.

---

## Step 10: Final Testing

| URL you type              | What you should see                  |
|---------------------------|--------------------------------------|
| `/Home/Student`           | Rahul’s information                  |
| `/Student/Details`        | Priya’s information                  |
| Click the link on Home    | Same as `/Student/Details`           |

---

## How the Flow Works (Important to Understand)

```
Browser types → /Student/Details
                    ↓
            StudentController
                    ↓
              Details() method
                    ↓
        Views/Student/Details.cshtml
                    ↓
              HTML shown to user
```

---

## Files in this Practical Folder

| File                  | Where to put it              |
|-----------------------|------------------------------|
| `HomeController.cs`   | Controllers folder           |
| `StudentController.cs`| Controllers folder           |
| `Student.cshtml`      | Views/Home/                  |
| `Details.cshtml`      | Views/Student/               |
