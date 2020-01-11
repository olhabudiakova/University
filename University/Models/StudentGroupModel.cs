using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class StudentGroupModel
    {
        public IEnumerable<StatementStudentList> Students { get; set; }
        public SelectList Groups { get; set; }
        public String SecondName { get; set; }
    }
}
