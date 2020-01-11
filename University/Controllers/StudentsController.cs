using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestSharp;
using System.Text.Json;

namespace University.Controllers
{
    public class StudentsController : Controller
    {
        readonly UniversityContext db;
        List<CountAverage> countAverageList = new List<CountAverage>();
        public StudentsController(UniversityContext context)
        {
            db = context;
        }
        // GET: Students
        [HttpGet]
        public ActionResult Index(int? group, string secondName)
        {
            IQueryable<Student> students = db.Students.Include(s=>s.StatementStudentLists).Include(p=>p.PassWorks);
            List<Student> student = new List<Student>();
            if (group != null && group != 0)
            {
                foreach (Student s in db.Students)
                {
                    foreach (StatementStudentList st in db.StatementStudents)
                    {
                        if (s.RecordNumber == st.RecordNumber)
                        {

                            if (st.GroupId == group)
                            {
                                students = db.Students.Where(q => q.RecordNumber == s.RecordNumber).Include(s => s.StatementStudentLists).Include(p => p.PassWorks);

                                student.AddRange(students);
                            }
                        }
                        
                    }
                }
               
                students = student.AsQueryable().Distinct();
            }
            if (!String.IsNullOrEmpty(secondName))
            {
                students = students.Where(s => s.SecondName == secondName).Include(s=>s.StatementStudentLists);
            }
            List<Group> groupsList = db.Groups.ToList();


            foreach (StatementStudentList s in db.StatementStudents.Include(p => p.Statement))
            {
                CountAverage countAverage = new CountAverage();
                countAverage.RecordNumber = s.RecordNumber;
               
                countAverage.Points = db.StatementStudents.Where(p => p.RecordNumber == s.RecordNumber).Select(p => p.Points).ToList();
                // countAverage.Average = (countAverage.Points.Aggregate((q, i) => q + i) / countAverage.Points.Count).ToString();
                countAverageList.Add(countAverage);
            }

            foreach (CountAverage c in countAverageList)
            {
                //c.Marks = db.PassWorks.Where(s=>s.StudentId==c.RecordNumber && s.SubjectId==c.SubjectId).Select(s=>s.Mark).ToList();
                c.Average = Calculate(c);
               // c.Average = (c.Points.Aggregate((q, i) => q + i) / c.Points.Count).ToString();           
            }
            // устанавливаем начальный элемент, который позволит выбрать всех
            groupsList.Insert(0, new Group { GroupId = 0 });
            SelectList groups = new SelectList(groupsList, "GroupId", "GroupId");  
            ViewBag.Groups = groups;
            ViewBag.Average = countAverageList;
           
            string SecondName = secondName;
            return View(students.ToList());
        }
        [HttpPost]
        public string Calculate(CountAverage countAverage)
        {
          
            var client = new RestClient("https://localhost:44382/calculator/CountAverage");
            var request = new RestRequest(Method.POST);
            var requestBody = JsonSerializer.Serialize(countAverage,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("expressionData", requestBody, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
           // countAverage.Average = response.Content;
            return response.Content;
        }

        [HttpGet]
        public ActionResult CreateStudent()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateStudent(Student student)
        {
            student.RoleID = 3;
            db.Students.Add(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult About(string? id)
        {
            string sessionLogin = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            string login = null;
            if (!string.IsNullOrEmpty(id))
            {
                foreach (Student s in db.Students)
                {
                    if (s.RecordNumber == id)
                        login = s.Login;
                }
                if (login == sessionLogin || (role == "Admin" || role == "Lecturer"))
                {
                    var pass = db.PassWorks.Include(s => s.Subject);
                    //IQueryable<Student> student = db.Students;
                    //var student = db.Students.Where(s => s.RecordNumber == id).Include(st => st.StatementStudentLists).Include(m => m.PassWorks);
                    var student = db.StatementStudents.Where(s => s.RecordNumber == id).Include(s => s.Student).Include(s => s.Statement.Subject);
                    IQueryable<Subject> subjects= db.Subjects;
                    IQueryable<Works> works = db.Works;
                    IEnumerable<PassWorks> marks = db.PassWorks.Include(s=>s.Subject).Include(s=>s.Works).Where(s => s.StudentId == id);
                    foreach(PassWorks pw in marks)
                    {

                        ViewBag.Subject = subjects.Where(s=>s.SubjectId==pw.SubjectId);
                        ViewBag.Works = works.Where(s=>s.WorkId==pw.WorksWorkId);
                    }
                    ViewBag.Students = student;
                    ViewBag.PassWorks = marks;
                    return View(student.ToList());
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult AboutSubject(string? id, int? id2)
        {
            IQueryable<PassWorks> passWorks = db.PassWorks.Include(w => w.Works).Include(s=>s.Subject).Where(s=>s.StudentId==id && s.SubjectId==id2);
            ViewBag.PassWorks = passWorks;
            return View(passWorks.ToList());
        }
    }
}