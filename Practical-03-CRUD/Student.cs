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
