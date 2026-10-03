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
