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
            var users = _context.Students.ToList().OrderByDescending(x => x.Mark);
            return View(users);
        }

        [HttpGet]
        public JsonResult Search(string prefix)
        {
            List<StudentModel> result = new List<StudentModel>();
            if (prefix != null)
            {
                result = _context.Students.Where(p => p.FirstName.Contains(prefix) || p.LastName.Contains(prefix))
                                   .OrderByDescending(s=> s.Mark).ToList();
            }
            else
            {
                result = _context.Students.OrderByDescending(s => s.Mark).ToList();
            }
            return Json(result);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        //public IActionResult Message(MessageModel model)
        //{

        //}

        public JsonResult Results()
        {
            ResultsVM results = new ResultsVM();
            results.countPassed = _context.Students.Count(x => x.Mark > 8);
            results.countFailed = _context.Students.Count(x => x.Mark < 8);
            return Json(results);
        }
    }
}
