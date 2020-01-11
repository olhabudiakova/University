using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class Student : Client
    {
        [Key]
        [MaxLength(50)]
        public string RecordNumber { get; set; }
        public ICollection<StatementStudentList> StatementStudentLists { get; set; }
        public ICollection<PassWorks> PassWorks { get; set; }
        
    }
}
