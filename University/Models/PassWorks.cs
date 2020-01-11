using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class PassWorks
    {
        [Key]
        public int PassId { get; set; }
        public int Mark { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
       
        public int WorksWorkId { get; set; }
        public string StudentId { get; set; }
        public int SubjectId { get; set; }
        public Works Works { get; set; }
        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}
