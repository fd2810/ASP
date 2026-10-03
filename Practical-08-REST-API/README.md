# Practical 8 – Creating a RESTful Service (Web API)

**Aim:** Create a RESTful Web API with GET, POST, PUT, DELETE operations for Students.

---

## Step 1: Create Project

1. Open Visual Studio → **Create a new project**.
2. Select **ASP.NET Web Application (.NET Framework)**.
3. Name: `StudentRESTService`
4. Framework: 4.7.2 or 4.8
5. Select **Web API** template → Create.

---

## Step 2: Create Student Model

### File: `Models/Student.cs`

```csharp
namespace StudentRESTService.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public int Age { get; set; }
    }
}
```

---

## Step 3: Create StudentsController

### File: `Controllers/StudentsController.cs`

```csharp
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using StudentRESTService.Models;

namespace StudentRESTService.Controllers
{
    public class StudentsController : ApiController
    {
        // In-memory data store
        private static List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "Rahul", Course = "BSc IT", Age = 20 },
            new Student { Id = 2, Name = "Priya", Course = "BSc IT", Age = 21 }
        };

        // GET: api/students
        public IHttpActionResult Get()
        {
            return Ok(students);
        }

        // GET: api/students/1
        public IHttpActionResult Get(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // POST: api/students
        public IHttpActionResult Post(Student student)
        {
            student.Id = students.Count + 1;
            students.Add(student);
            return Ok(student);
        }

        // PUT: api/students/1
        public IHttpActionResult Put(int id, Student student)
        {
            var existing = students.FirstOrDefault(s => s.Id == id);
            if (existing == null)
                return NotFound();

            existing.Name = student.Name;
            existing.Course = student.Course;
            existing.Age = student.Age;

            return Ok(existing);
        }

        // DELETE: api/students/1
        public IHttpActionResult Delete(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();

            students.Remove(student);
            return Ok(student);
        }
    }
}
```

---

## Step 4: Understand Routing

File: `App_Start/WebApiConfig.cs`

```csharp
config.Routes.MapHttpRoute(
    name: "DefaultApi",
    routeTemplate: "api/{controller}/{id}",
    defaults: new { id = RouteParameter.Optional }
);
```

This means:

| HTTP Method | URL                    | Action          |
|-------------|------------------------|-----------------|
| GET         | `/api/students`        | Get all         |
| GET         | `/api/students/1`      | Get by id       |
| POST        | `/api/students`        | Create          |
| PUT         | `/api/students/1`      | Update          |
| DELETE      | `/api/students/1`      | Delete          |

---

## Step 5: Run & Test

1. Press **Ctrl + F5**.
2. Open browser: `http://localhost:xxxxx/api/students`

**Sample GET Response (JSON):**

```json
[
  {
    "Id": 1,
    "Name": "Rahul",
    "Course": "BSc IT",
    "Age": 20
  },
  {
    "Id": 2,
    "Name": "Priya",
    "Course": "BSc IT",
    "Age": 21
  }
]
```

### Testing with Postman / Browser

| Method | URL                     | Body (JSON)                          |
|--------|-------------------------|--------------------------------------|
| GET    | /api/students           | –                                    |
| GET    | /api/students/1         | –                                    |
| POST   | /api/students           | `{"Name":"Amit","Course":"BCA","Age":22}` |
| PUT    | /api/students/1         | `{"Name":"Rahul Updated",...}`       |
| DELETE | /api/students/1         | –                                    |

---

## REST Summary

| Verb    | Purpose        |
|---------|----------------|
| GET     | Retrieve data  |
| POST    | Add new data   |
| PUT     | Update data    |
| DELETE  | Remove data    |
