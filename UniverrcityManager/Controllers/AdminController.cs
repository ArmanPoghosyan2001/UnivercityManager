using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UnivercityManager.Models;

namespace UnivercityManager.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        UnivercityDB _context;
        UserManager<Admin> _userManager;
        SignInManager<Admin> _signInManager;
        public AdminController(
            UnivercityDB context,
            UserManager<Admin> userManager,
            SignInManager<Admin> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Admin admin = new Admin
        //        {
        //            Email = model.Email,
        //            UserName = model.Email,
        //        };
        //        // добавляем пользователя
        //        var result = await _userManager.CreateAsync(admin, model.Password);
        //        if (result.Succeeded)
        //        {
        //            // установка куки
        //            await _signInManager.SignInAsync(admin, false);
        //            return RedirectToAction("Index", "Admin");
        //        }
        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }
        //        }
        //    }
        //    return View(model);
        //}


        //[HttpGet]
        //public IActionResult Login(string ReturnUrl = null)
        //{
        //    return View(new LoginViewModel { ReturnUrl = ReturnUrl });
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result =
        //            await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

        //        if (result.Succeeded)
        //        {
        //            // проверяем, принадлежит ли URL приложению
        //            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        //            {
        //                return Redirect(model.ReturnUrl);
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index", "Admin");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Login or password is incorrect");
        //        }
        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Logout()
        //{
        //    // удаляем аутентификационные куки
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "user");
        //}

        [HttpGet]
        public IActionResult Index()
        {
            var students = _context.Students.ToList().OrderByDescending(x => x.Mark);
            return View(students);
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(StudentModel student)
        {
            if (student.Mark < 8)
            {
                student.ImgPath = "/imgs/failed.jpg";
            }
            else
            {
                student.ImgPath = "/imgs/passed.jpg";
            }
            _context.Add(student);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            StudentModel student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(StudentModel student)
        {
            if (ModelState.IsValid)
            {
                if (student.Mark < 8)
                {
                    student.ImgPath = "/imgs/failed.jpg";
                }
                else
                {
                    student.ImgPath = "/imgs/passed.jpg";
                }
                _context.Update(student);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            StudentModel student = _context.Students.Find(id);
            _context.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

    }
}
