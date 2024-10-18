using System.ComponentModel.DataAnnotations;

namespace University.DataModel
{
    public class Student
    {
        [Required]
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Faculty Faculty { get; set; }
        public List<Exam> StudentsExams { get; set; } = new();
        public List<Course> StudentsCourses { get; set; } = new();
        public decimal Tuition { get; set; }
        public decimal AvgGrades { get; set; }
    }
}

