# Practical 10 – Creating Application Using HTML Helpers (Form + Validation)

**Aim:** Create a Student Registration form using HTML Helpers and model validation.

> Note: The original PDF mentions Tag Helpers / Razor Pages, but the actual code uses classic **HTML Helpers** (`@Html.TextBoxFor`, etc.) which are standard in ASP.NET MVC 5.

---

## Step 1: Create Project

1. Create new project: **ASP.NET Web Application (.NET Framework)**
2. Name: `StudentRegistration`
3. Template: **MVC** + No Authentication

---

## Step 2: Create Student Model

### File: `Models/Student.cs`

```csharp
using System.ComponentModel.DataAnnotations;

namespace StudentRegistration.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Course is required")]
        public string Course { get; set; }

        public bool Agree { get; set; }
    }
}
```

---

## Step 3: Create StudentController

### File: `Controllers/StudentController.cs`

```csharp
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
                // Here you can save to database
                return View();
            }

            // Validation failed – return the same view with errors
            return View(student);
        }
    }
}
```

---

## Step 4: Create the Register View (with HTML Helpers)

### File: `Views/Student/Register.cshtml`

```html
@model StudentRegistration.Models.Student

@{
    ViewBag.Title = "Student Registration";
}

<h2>Student Registration</h2>

@using (Html.BeginForm())
{
    <div>
        @Html.LabelFor(m => m.Name)<br />
        @Html.TextBoxFor(m => m.Name)
        @Html.ValidationMessageFor(m => m.Name)
    </div>
    <br />

    <div>
        @Html.LabelFor(m => m.Email)<br />
        @Html.TextBoxFor(m => m.Email)
        @Html.ValidationMessageFor(m => m.Email)
    </div>
    <br />

    <div>
        @Html.LabelFor(m => m.Password)<br />
        @Html.PasswordFor(m => m.Password)
        @Html.ValidationMessageFor(m => m.Password)
    </div>
    <br />

    <div>
        @Html.LabelFor(m => m.Gender)<br />
        @Html.RadioButtonFor(m => m.Gender, "Male") Male
        @Html.RadioButtonFor(m => m.Gender, "Female") Female
        @Html.ValidationMessageFor(m => m.Gender)
    </div>
    <br />

    <div>
        @Html.LabelFor(m => m.Course)<br />
        @Html.DropDownListFor(m => m.Course,
            new SelectList(new[] { "BSc Data Science", "BSc IT", "BCA", "MCA" }),
            "Select Course")
        @Html.ValidationMessageFor(m => m.Course)
    </div>
    <br />

    <div>
        @Html.CheckBoxFor(m => m.Agree)
        I agree to the terms and conditions.
    </div>
    <br />

    <input type="submit" value="Register" />
}

@if (ViewBag.Message != null)
{
    <h3 style="color:green">@ViewBag.Message</h3>
}
```

---

## Step 5: Enable Client-Side Validation (optional but recommended)

Make sure these scripts are present in `_Layout.cshtml` or the view:

```html
<script src="~/Scripts/jquery.validate.min.js"></script>
<script src="~/Scripts/jquery.validate.unobtrusive.min.js"></script>
```

---

## Step 6: Run & Test

1. Press **Ctrl + F5**.
2. Open: `/Student/Register`

### Test Cases

| Action                          | Expected Result                     |
|---------------------------------|-------------------------------------|
| Submit empty form               | Validation messages appear          |
| Fill all fields correctly       | "Student registered successfully!"  |
| Leave Name empty                | "Name is required"                  |

---

## HTML Helpers Used

| Helper                      | Purpose                          |
|-----------------------------|----------------------------------|
| `@Html.LabelFor`            | Creates label                    |
| `@Html.TextBoxFor`          | Text input                       |
| `@Html.PasswordFor`         | Password input                   |
| `@Html.RadioButtonFor`      | Radio buttons                    |
| `@Html.DropDownListFor`     | Dropdown list                    |
| `@Html.CheckBoxFor`         | Checkbox                         |
| `@Html.ValidationMessageFor`| Shows validation error           |
| `@Html.BeginForm`           | Creates `<form>` tag             |
