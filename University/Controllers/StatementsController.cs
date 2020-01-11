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
    public class StatementsController : Controller
    {
        readonly UniversityContext db;

        public StatementsController(UniversityContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {

            var statements = db.StatementStudents.Include(s => s.Statement.Subject).Include(g=>g.Group);
            return View(statements.ToList());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateStatement()
        {
            SelectList subjects = new SelectList(db.Subjects, "SubjectId", "Title");
            SelectList lecturers = new SelectList(db.Lecturers, "LecturerId", "SecondName");
            ViewBag.Subjects = subjects;
            ViewBag.Lecturers = lecturers;
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateStatement(Statement statement)
        {
            db.Statements.Add(statement);
            db.SaveChanges();
            return RedirectToAction("StudentToStatement", new { id = statement.StatementId});
        }
        [HttpGet]
        public IActionResult StudentToStatement()
        {
           // List<string> recordNumbers = new List<string>(db.Students.Count());
            List<Student> student = db.Students.ToList();
           
           // var statement = db.Statements;
                //db.Statements.Where(s=>s.StatementId == (db.Statements.Max(s=>s.StatementId)));
            SelectList groups = new SelectList(db.Groups, "GroupId", "GroupId");
            SelectList students = new SelectList(student, "RecordNumber", "SecondName");
            SelectList statements = new SelectList(db.Statements, "StatementId", "StatementId");
            ViewBag.Students = students;
            ViewBag.Groups = groups;
            ViewBag.Statemets = statements;
            return View();
        } 
        [HttpPost]
        public IActionResult StudentToStatement(StatementStudentList statementStudentList)
        {
            statementStudentList.StatementId = db.Statements.Max(s => s.StatementId);
            db.StatementStudents.Add(statementStudentList);
            db.SaveChanges();
            return RedirectToAction("StudentToStatement", new { id = statementStudentList.StatementId });
        }
    }
}