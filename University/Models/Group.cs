using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        public string StudyYear { get; set; }
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
        public ICollection<StatementStudentList> StatementStudentLists { get; set; }
    }
}
