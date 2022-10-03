using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PHIMS.Models;

namespace PHIMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PHIMS.Models.Admin>? Admin { get; set; }
        public DbSet<PHIMS.Models.Doctor>? Doctor { get; set; }
        public DbSet<PHIMS.Models.User>? User { get; set; }
        public DbSet<PHIMS.Models.Pateint>? Pateint { get; set; }
        public DbSet<PHIMS.Models.Lab>? Lab { get; set; }
        public DbSet<PHIMS.Models.Result>? Result { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}