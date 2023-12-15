﻿using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contexts
{
    public class RevisionDbContext : IdentityDbContext<ApplicationUser>
    {
        public RevisionDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; } 
        public DbSet<Employee> Employees { get; set; }
    }
}
