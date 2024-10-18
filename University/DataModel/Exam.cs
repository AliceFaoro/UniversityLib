using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
