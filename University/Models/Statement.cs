using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class Statement
    {
        [Key]
        public int StatementId { get; set; }
        public int SubjectId { get; set; }
        public int LecturerId { get; set; }
        public int Semester { get; set; }
        public Subject Subject { get; set; }
        public Lecturer Lecturer { get; set; }
        public ICollection<StatementStudentList> StatementStudentLists { get; set; }
    }
}
