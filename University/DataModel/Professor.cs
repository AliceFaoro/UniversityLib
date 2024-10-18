using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
