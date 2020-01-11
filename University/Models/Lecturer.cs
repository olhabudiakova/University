using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class Lecturer : Client
    {
        [Key]
        public int LecturerId { get; set; }
        public string AcademicDegree { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public ICollection<Statement> Statements { get; set; }
    }
}
