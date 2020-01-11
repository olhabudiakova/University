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
    [Authorize(Roles ="Admin")]
    public class OtherController : Controller
    {
        readonly UniversityContext db;
        public OtherController(UniversityContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult WorksTypes()
        {
            return View(db.Works.ToList());
        }
        public IActionResult Groups()
        {
            var groups = db.Groups.Include(g => g.Specialty);
            return View(groups.ToList());
        }
        public IActionResult Specialties()
        {
            return View(db.Specialties.ToList());
        }
        public IActionResult Subjects()
        {
            return View(db.Subjects.ToList());
        }
        [HttpGet]
        public IActionResult CreateWorkType()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateWorkType(Works works)
        {
            db.Works.Add(works);
            db.SaveChanges();
            return RedirectToAction("WorksTypes");
        }
        [HttpGet]
        public IActionResult CreateSubject()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateSubject(Subject subject)
        {
            db.Subjects.Add(subject);
            db.SaveChanges();
            return RedirectToAction("Subjects");
        }
        [HttpGet]
        public IActionResult CreateSpecialty()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateSpecialty(Specialty specialty)
        {
            db.Specialties.Add(specialty);
            db.SaveChanges();
            return RedirectToAction("Specialties");
        }
        [HttpGet]
        public IActionResult CreateGroup()
        {
            SelectList specialties = new SelectList(db.Specialties, "SpecialtyId", "Title");
            ViewBag.Specialties = specialties;
            return View();
        }
        [HttpPost]
        public IActionResult CreateGroup(Group group)
        {
            db.Groups.Add(group);
            db.SaveChanges();
            return RedirectToAction("Groups");
        }
    }
}