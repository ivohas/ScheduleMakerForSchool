using BookFindingAndRatingSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Schedule.Data.Models;

namespace Schedule.Data
{
    public class ScheduleDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options)
           : base(options)
        {

        }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectsPerWeek> SubjectsPerWeeks { get; set; }
        public DbSet<Consultation> Consultations { get; set; }

        public DbSet<UserConsultation> UsersConsultation { get; set; }
        // db sets
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<Guid>>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey });

            modelBuilder.Entity<UserConsultation>()
                .HasKey(uc => new {uc.ApplicationUserID, uc.ConsultationID });
        }
    }
}
