using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class DbInitializer
    {
        public static void Initialize(UniversityContext universityContext)
        {
            
            if (!universityContext.Students.Any())
            {
                universityContext.Students.AddRange(
                    new Student
                    {
                        FirstName = "Olha",
                        SecondName = "Budiakova",
                        Login = "olkin",
                        Password = "olkin",
                        RecordNumber = "RN4537",
                        RoleID = 3
                    });
            }
            
            
            universityContext.SaveChanges();
        }
    }
}
