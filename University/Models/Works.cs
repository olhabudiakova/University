using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class Works
    {
        [Key]
        public int WorkId { get; set; }
        public string Title { get; set; }
        public ICollection<PassWorks> PassWorks { get; set; }
    }
}
