using System.ComponentModel.DataAnnotations;

namespace University.DataModel
{
    public class Exam
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public Faculty Faculty { get; set; }
        public Course Course { get; set; }
        public Professor ExamProfessor { get; set; }
        public DateTime ExamDate { get; set; }


    }
}
