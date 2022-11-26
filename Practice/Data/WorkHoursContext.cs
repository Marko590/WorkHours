using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practice.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WorkHours.Models;

namespace Practice.Data
{
    public class WorkHoursContext : IdentityDbContext<ApplicationUser>
    {
        
        public DbSet<Worksite> Worksites { get; set; } = null!;
        public DbSet<Worker> Workers { get; set; } = null!;
        public DbSet<Admin> Admins { get; set; } = null!;
        
        
        public DbSet<Work> Works { get; set; }= null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WorkHours1;Integrated Security=True;");
        }
       
    }
}
