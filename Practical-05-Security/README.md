# Practical 5 – Implementing Security to Application

**Aim:** Understand Authentication (Who are you?) and Authorization (What can you do?) and protect pages using `[Authorize]`.

---

## Important Concepts

| Term              | Meaning                                      |
|-------------------|----------------------------------------------|
| **Authentication**| Who are you? (Login with email & password)   |
| **Authorization** | What can you do? (Can this user open this page?) |

---

## Step 1: Create a New Project with Individual User Accounts

1. Open **Visual Studio 2019**.
2. Click **Create a new project**.
3. In the search box type: `ASP.NET Web Application`
4. Select **ASP.NET Web Application (.NET Framework)**.
5. Click **Next**.
6. Configure the project:
   - **Project name:** `SecureMVCApp`
   - **Location:** Choose any folder on your computer
   - **Framework:** .NET Framework 4.7.2 (or 4.8)
7. Click **Create**.
8. In the next window:
   - Select **MVC**
   - Click the button **Change Authentication**
   - Select **Individual User Accounts**
   - Click **OK**
9. Click **Create**.

Visual Studio will automatically create:
- Login page
- Register page
- Logout functionality
- Identity database support

---

## Step 2: Run the Application for the First Time

1. Press **Ctrl + F5** (or click the green IIS Express button).
2. The home page will open in the browser.
3. Look at the top-right corner — you will see **Register** and **Log in** links.

---

## Step 3: Register a New User

1. Click **Register**.
2. Fill the form:
   - **Email:** `student@gmail.com`
   - **Password:** `Student@123`
   - **Confirm password:** `Student@123`
3. Click **Register**.

The user is now saved in the database.  
(The password is stored in encrypted/hashed form, not plain text.)

---

## Step 4: Verify the User in Database (Optional but good to see)

1. In Visual Studio go to **View → Server Explorer** (or SQL Server Object Explorer).
2. Expand **Data Connections**.
3. Expand the connection that appears after registration.
4. Expand **Tables**.
5. Right-click **AspNetUsers** → **Show Table Data**.
6. You will see your registered email.

---

## Step 5: Create a Secure Controller

1. In **Solution Explorer**, right-click the **Controllers** folder.
2. Select **Add → Controller**.
3. Choose **MVC 5 Controller - Empty**.
4. Click **Add**.
5. Enter the name: `StudentController`
6. Click **Add**.

Visual Studio creates the file `Controllers/StudentController.cs`.

### Now replace the entire code of `StudentController.cs` with this:

```csharp
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
```

> **Note:** The `[Authorize]` attribute means only logged-in users can open any action of this controller.

---

## Step 6: Create the View for StudentController

1. Open `StudentController.cs`.
2. Right-click inside the `Index()` method.
3. Select **Add View...**
4. In the dialog:
   - **View name:** `Index`
   - **Template:** Empty (without model)
   - Leave other options as default
5. Click **Add**.

Visual Studio creates the folder and file:
```
Views
 └── Student
      └── Index.cshtml
```

### Now open `Views/Student/Index.cshtml` and replace its content with:

```html
@{
    ViewBag.Title = "Student Dashboard";
}

<h2>Student Dashboard</h2>
<h3>Only logged-in users can see this page.</h3>
```

---

## Step 7: Test Authorization

1. Press **Ctrl + F5** to run the application.
2. In the browser address bar type:
   ```
   /Student
   ```
   or
   ```
   /Student/Index
   ```

**What happens?**

- If you are **not logged in** → You are automatically redirected to the Login page.
- After you **log in** → The Student Dashboard page opens.

This proves that `[Authorize]` is working.

---

## Step 8: Protect Only One Action (Optional Experiment)

Sometimes you want only one method to be protected, not the whole controller.

Change `StudentController.cs` to:

```csharp
using System.Web.Mvc;

namespace SecureMVCApp.Controllers
{
    public class StudentController : Controller
    {
        // This action is public – anyone can open it
        public ActionResult About()
        {
            return View();
        }

        // Only this action requires login
        [Authorize]
        public ActionResult Result()
        {
            return View();
        }
    }
}
```

You can create simple views for `About` and `Result` the same way (right-click → Add View).

| URL                    | Login Required? |
|------------------------|-----------------|
| `/Student/About`       | No              |
| `/Student/Result`      | Yes             |

---

## Step 9: Allow Anonymous Access on One Action

If the whole controller is protected with `[Authorize]`, but you still want one public page:

```csharp
[Authorize]
public class StudentController : Controller
{
    [AllowAnonymous]   // This page is public
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

| Page       | Login Required? |
|------------|-----------------|
| About      | No              |
| Dashboard  | Yes             |

---

## Summary – What You Learned

- How to create a project with **Individual User Accounts**
- How to register and login users
- How to protect a whole controller with `[Authorize]`
- How to protect only one action
- How to make one action public with `[AllowAnonymous]`

---

## Files in this Practical Folder

- `StudentController.cs` → Copy this into your Controllers folder
- `Index.cshtml` → Copy this into Views/Student/
