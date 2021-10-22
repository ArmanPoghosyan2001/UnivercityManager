using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UnivercityManager.Models
{
    public class StudentModel
    {
        //[Required]
        public int Id { get; set; }
        //[Required]
        public string FirstName { get; set; }
        //[Required]
        public string LastName { get; set; }
        //[Required]
        public string Faculty { get; set; }
        //[Required]
        public double Mark { get; set; }
        public string ImgPath { get; set; }
    }
}
