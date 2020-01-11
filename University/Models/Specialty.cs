using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class Specialty
    {
        [Key]
        public int SpecialtyId { get; set; }
        public string Title { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
