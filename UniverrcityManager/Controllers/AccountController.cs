using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnivercityManager.Models;

namespace UnivercityManager.Controllers
{
    public class AccountController : Controller
    {
        SignInManager<Admin> _signInManager;
        UserManager<Admin> _userManager;

        public AccountController(
            SignInManager<Admin> signInManager,
            UserManager<Admin> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Admin admin = new Admin
                {
                    Email = model.Email,
                    UserName = model.Email,
                };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(admin, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(admin, false);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Login(string ReturnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = ReturnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Login or password is incorrect");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "user");
        }
    }
}
