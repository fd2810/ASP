# Practical 5 – Implementing Security to Application

**Aim:** Understand Authentication & Authorization and protect controllers/actions using `[Authorize]`.

---

## Important Concepts

| Term            | Meaning                                      |
|-----------------|----------------------------------------------|
| Authentication  | Who are you? (Login)                         |
| Authorization   | What can you do? (Permissions / Roles)       |

---

## Step 1: Create Project with Individual User Accounts

1. Open Visual Studio 2019 → **Create a new project**.
2. Select **ASP.NET Web Application (.NET Framework)** or **ASP.NET Core Web App (MVC)**.
3. Project Name: `SecureMVCApp`
4. Click **Create**.
5. Choose **MVC**.
6. Click **Change Authentication** → Select **Individual User Accounts**.
7. Click **OK** → **Create**.

Visual Studio automatically creates:
- Login & Register pages
- Identity database
- User management

---

## Step 2: Run & Register a User

1. Press **Ctrl + F5**.
2. Click **Register** (top-right).
3. Enter:
   - Email: `student@gmail.com`
   - Password: `Student@123`
4. Click **Register**.

The user is saved in `AspNetUsers` table (password is hashed).

---

## Step 3: Create a Secure Controller

### File: `Controllers/StudentController.cs`

```csharp
using System.Web.Mvc;
// For ASP.NET Core: using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

namespace SecureMVCApp.Controllers
{
    [Authorize]   // Protects the entire controller
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
```

### Create View: `Views/Student/Index.cshtml`

```html
<h2>Student Dashboard</h2>
<h3>Only logged-in users can see this page.</h3>
```

---

## Step 4: Test Authorization

1. Run the application.
2. Open: `https://localhost:xxxxx/Student`
3. **Without login** → Automatically redirected to Login page.
4. **After login** → Student Dashboard is shown.

---

## Step 5: Protect Only One Action

```csharp
public class StudentController : Controller
{
    public ActionResult About()
    {
        return View();
    }

    [Authorize]
    public ActionResult Result()
    {
        return View();
    }
}
```

- `/Student/About` → Everyone can access
- `/Student/Result` → Login required

---

## Step 6: Allow Anonymous Users on Specific Action

```csharp
[Authorize]
public class StudentController : Controller
{
    [AllowAnonymous]
    public ActionResult About()
    {
        return View();
    }

    public ActionResult Dashboard()
    {
        return View();
    }
}
```

| Page      | Login Required |
|-----------|----------------|
| About     | No             |
| Dashboard | Yes            |

---

## Summary

- `[Authorize]` → Only authenticated users
- `[AllowAnonymous]` → Public access (even if controller is protected)
- Individual User Accounts template gives ready-made Login/Register
