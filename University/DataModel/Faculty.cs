using System.ComponentModel.DataAnnotations;

namespace University.DataModel
{
    public class Faculty
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string NameFaculty { get; set; }
        public List<Course> CoursesFaculty { get; set; } = new();
        public List<Exam> ExamsFaculty { get; set; } = new();
        public List<Professor> ProfessorsFaculty { get; set; } = new();



    }
}
