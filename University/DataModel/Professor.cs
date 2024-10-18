using System.ComponentModel.DataAnnotations;

namespace University.DataModel
{
    public class Professor
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Faculty Faculty { get; set; }
        public decimal Pay { get; set; }
        public List<Exam> ProfessorExams = new();
    }
}
