using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string Title { get; set; }
        public ICollection<Statement> Statements { get; set; }
        public ICollection<PassWorks> PassWorks { get; set; }
    }
}
