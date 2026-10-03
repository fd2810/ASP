# Practical 3 – Creating Applications with Data (CRUD)

**Aim:** Build a Student Management System with full **Create, Read, Update, Delete** operations using Entity Framework and Scaffolding.

**Technology:** ASP.NET MVC 5 + Entity Framework 6 + SQL Server LocalDB

---

## Step 1: Create Project

1. Open Visual Studio 2019 → **Create a new project**.
2. Select **ASP.NET Web Application (.NET Framework)**.
3. Click **Next**.
4. Configure:
   - **Project Name:** `StudentCRUD`
   - **Framework:** .NET Framework 4.7.2
5. Click **Create**.
6. Select **MVC** + **No Authentication** → **Create**.

---

## Step 2: Install Entity Framework

Open **Package Manager Console** (`Tools → NuGet Package Manager → Package Manager Console`) and run:

```powershell
Install-Package EntityFramework
```

---

## Step 3: Create Model

### File: `Models/Student.cs`

```csharp
using System.ComponentModel.DataAnnotations;

namespace StudentCRUD.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Range(18, 60)]
        public int Age { get; set; }

        [StringLength(50)]
        public string Course { get; set; }
    }
}
```

---

## Step 4: Create Database Context

### File: `Models/StudentContext.cs`

```csharp
using System.Data.Entity;

namespace StudentCRUD.Models
{
    public class StudentContext : DbContext
    {
        public StudentContext() : base("StudentDB")
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
```

---

## Step 5: Add Connection String

Open **Web.config** and add inside `<configuration>`:

```xml
<connectionStrings>
  <add name="StudentDB"
       connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

---

## Step 6: Enable Migrations & Create Database

In **Package Manager Console** run these commands one by one:

```powershell
Enable-Migrations
Add-Migration InitialCreate
Update-Database
```

---

## Step 7: Scaffold Controller + Views

1. Right-click **Controllers** folder → **Add → Controller**.
2. Select **MVC 5 Controller with views, using Entity Framework**.
3. Configure:
   - **Model class:** `Student`
   - **Data context class:** `StudentContext`
   - **Controller name:** `StudentsController`
4. Click **Add**.

Visual Studio automatically creates:

- `Controllers/StudentsController.cs`
- Views: `Create`, `Edit`, `Delete`, `Details`, `Index`

---

## Step 8: Run the Application

Press **Ctrl + F5**.

Open in browser:

```
https://localhost:xxxxx/Students
```

### Test CRUD Operations

| Operation | How to Test                          |
|-----------|--------------------------------------|
| **Create**| Click "Create New" → Enter data → Create |
| **Read**  | List of students appears on Index    |
| **Update**| Click "Edit" → Change values → Save  |
| **Delete**| Click "Delete" → Confirm             |

---

## Important Generated Code (for understanding)

### Index (Read)
```csharp
public ActionResult Index()
{
    return View(db.Students.ToList());
}
```

### Create (POST)
```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Create(Student student)
{
    if (ModelState.IsValid)
    {
        db.Students.Add(student);
        db.SaveChanges();
        return RedirectToAction("Index");
    }
    return View(student);
}
```

### Edit
```csharp
db.Entry(student).State = EntityState.Modified;
db.SaveChanges();
```

### Delete
```csharp
Student student = db.Students.Find(id);
db.Students.Remove(student);
db.SaveChanges();
```

---

## Final Project Structure

```
StudentCRUD/
├── Controllers/
│   └── StudentsController.cs
├── Models/
│   ├── Student.cs
│   └── StudentContext.cs
├── Views/
│   └── Students/
│       ├── Create.cshtml
│       ├── Edit.cshtml
│       ├── Delete.cshtml
│       ├── Details.cshtml
│       └── Index.cshtml
├── Migrations/
└── Web.config
```
