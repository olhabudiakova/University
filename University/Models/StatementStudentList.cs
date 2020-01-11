using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class StatementStudentList
    {
        [MaxLength(50)]
        public string RecordNumber { get; set; }
        public int StatementId { get; set; }
        public int GroupId { get; set; }
        public int Points { get; set; }
        public Statement Statement { get; set; }
        public Student Student { get; set; }
        public Group Group { get; set; }
    }
}
