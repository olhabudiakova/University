using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Models;
using University.ViewModels;

namespace University.Controllers
{
    public class AccountController : Controller
    {
        public UniversityContext universityContext;
        public AccountController(UniversityContext universityContext)
        {
            this.universityContext = universityContext;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                Student student = await universityContext.Students.Include(s=>s.Role).FirstOrDefaultAsync(s => s.RecordNumber == model.Login && s.Password == model.Password);
                Lecturer lecturer = await universityContext.Lecturers.Include(l=>l.Role).FirstOrDefaultAsync(l => l.Login == model.Login && l.Password == model.Password);
                if(student != null)
                {
                    await Authenticate(student);
                    return RedirectToAction("Index", "Home");
                }
                else if(lecturer != null)
                {
                    await Authenticate(lecturer);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректный логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(Client user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}