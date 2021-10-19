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
        [HttpGet]
        public JsonResult Search(string prefix)
        {
            List<StudentModel> result = new List<StudentModel>();
            if (prefix != null)
            {
                result = _context.Students.Where(p => p.FirstName.Contains(prefix) || p.LastName.Contains(prefix))
                                   .ToList();
            }
            else
            {
                result = _context.Students.ToList();
            }
            return Json(result);
        }

        //[HttpGet]
        //public IActionResult Search(string search)
        //{
        //    var users = _context.Students.Where(s => EF.Functions.Like(s.FirstName , $"%{search}%")).ToList();
        //    return View(users);
        //}
    }
}
