using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnivercityManager.Models;

namespace UnivercityManager.Controllers
{
    public class UserController : Controller
    {
        UnivercityDB _context;

        public UserController(UnivercityDB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var users = _context.Students.ToList();
            return View(users);
        }

        [HttpPost]
        public IActionResult Search(string search)
        {
            var users = _context.Students.Where(s => EF.Functions.Like(s.FirstName , $"%{search}%")).ToList();
            return View(users);
        }
    }
}
