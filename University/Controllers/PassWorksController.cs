using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Models;

namespace University.Controllers
{
    [Authorize(Roles ="Admin, Lecturer")]
    public class PassWorksController : Controller
    {
        readonly UniversityContext db;

        public PassWorksController(UniversityContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var passworks = db.PassWorks.Include(p => p.Subject).Include(s=>s.Works).Include(s=>s.Student);
           
            ViewBag.Passworks = passworks;
            return View(passworks);
        }
        [HttpGet]
        public IActionResult AddResult(int? groupid)
        {
            //IQueryable<PassWorks> students = db.PassWorks.Include(s=>s.Student);
            IQueryable<Student> students = db.Students;
            if(groupid!= null && groupid != 0)
            {
                foreach(Student s in students)
                {
                    foreach(StatementStudentList st in db.StatementStudents)
                    {
                        if (s.RecordNumber == st.RecordNumber && st.GroupId == groupid)
                        {
                            students = db.Students.Where(m=>m.RecordNumber==s.RecordNumber);
                        }
                    }
                }
               //students = db.StatementStudents.Where(s=>s.GroupId==groupid).Include(s => s.Student).Include(s => s.Group);
            }
            SelectList studentsList = new SelectList(students, "RecordNumber", "SecondName");
            SelectList subjects = new SelectList(db.Subjects, "SubjectId", "Title");
            SelectList groups = new SelectList(db.Groups, "GroupId", "GroupId");
            SelectList works = new SelectList(db.Works, "WorkId", "Title");
            ViewBag.Students = studentsList;
            ViewBag.Subjects = subjects;
            ViewBag.Works = works;
            ViewBag.Groups = groups;
            return View();
        }

        public IActionResult AddResult(PassWorks passWorks)
        {
            db.PassWorks.Add(passWorks);
            List<Statement> statements = db.Statements.ToList();
            foreach(Statement s in statements)
            {
                foreach(StatementStudentList st in db.StatementStudents)
                {
                    if(s.SubjectId == passWorks.SubjectId && st.RecordNumber == passWorks.StudentId)
                    {
                        st.Points += passWorks.Mark;
                    }
                }         
            }
            db.SaveChanges();
            return RedirectToAction("AddResult");
        } 
    }
}