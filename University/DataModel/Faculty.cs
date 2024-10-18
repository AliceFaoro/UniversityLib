using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.DataModel
{
    public class Faculty
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string NameFaculty { get; set; }
        public List<Exam> ExamsFaculty { get; set; } = new();
        public List<Professor> ProfessorsFaculty { get; set; } = new();



    }
}
