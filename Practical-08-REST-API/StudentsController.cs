using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using StudentRESTService.Models;

namespace StudentRESTService.Controllers
{
    public class StudentsController : ApiController
    {
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
