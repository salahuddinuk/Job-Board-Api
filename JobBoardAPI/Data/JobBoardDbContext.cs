using JobBoardAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace JobBoardAPI.Data
{
    public class JobBoardDbContext: DbContext
    {
        public DbSet<Applicant> Applicants => Set<Applicant>();
        public DbSet<Application> Applications => Set<Application>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Job> Jobs => Set<Job>();
        public DbSet<User> Users => Set<User>();

        public JobBoardDbContext(DbContextOptions<JobBoardDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobBoardDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
