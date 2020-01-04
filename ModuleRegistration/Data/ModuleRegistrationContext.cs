using Microsoft.EntityFrameworkCore;
using ModuleRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModuleRegistration.Data
{
    public class ModuleRegistrationContext : DbContext
    {
        public ModuleRegistrationContext(DbContextOptions<ModuleRegistrationContext> options) : base(options) { }

        public DbSet<Module> Modules { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<ModuleStudent> ModuleStudents { get; set; }
        public DbSet<ModuleStaff> ModuleStaff { get; set; }
    }
}
