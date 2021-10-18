using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnivercityManager.Models
{
    public class UnivercityDB : IdentityDbContext<Admin>
    {
        public DbSet<StudentModel> Students { get; set; }
        public UnivercityDB(DbContextOptions<UnivercityDB> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
