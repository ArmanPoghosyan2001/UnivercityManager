using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnivercityManager.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Faculty { get; set; }
        public double Mark { get; set; }
    }
}
