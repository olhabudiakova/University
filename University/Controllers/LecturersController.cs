using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Models;

namespace University.Controllers
{
    [Authorize(Roles = "Admin, Lecturer")]
    public class LecturersController : Controller
    {
        UniversityContext db;
        public LecturersController(UniversityContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Lecturers.ToList());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateLecturer()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateLecturer(Lecturer lecturer)
        {
            lecturer.RoleID = 2;
            db.Lecturers.Add(lecturer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}