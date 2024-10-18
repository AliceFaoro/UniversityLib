using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.DataModel
{
    public class Course
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string CourseName { get; set; }
        public Faculty Faculty { get; set; }
        public Professor CourseProfessor { get; set; }
        public List<Exam> CourseExams { get; set; }
    }
}
