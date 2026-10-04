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
