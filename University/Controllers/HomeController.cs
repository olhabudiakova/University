using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using University.Models;

namespace University.Controllers
{
    public class HomeController : Controller
    {
        readonly UniversityContext db;
        public HomeController(UniversityContext context)
        {
            db = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            if(role == "Student")
            {
                return RedirectToAction("Index", "Students");
            }

            return View();
        }      
    }
}
