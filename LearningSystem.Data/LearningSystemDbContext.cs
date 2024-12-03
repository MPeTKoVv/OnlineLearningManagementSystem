namespace LearningSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using LearningSystem.Data.Models;
    using LearningSystem.Data.Configurations;

    public class LearningSystemDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly bool seedDb;

        public LearningSystemDbContext(DbContextOptions<LearningSystemDbContext> options, bool seedDb = true)
            : base(options)
        {
            this.seedDb = seedDb;
        }

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Course> Courses { get; set; } = null!;

        public DbSet<Enrollment> Enrollments { get; set; } = null!;
        
        public DbSet<Lesson> Lessons { get; set; } = null!;
        
        public DbSet<Teacher> Teachers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CourseEntityConfiguration());
            builder.ApplyConfiguration(new EnrollmentEntityConfiguration());
            builder.ApplyConfiguration(new LessonEntityConfiguration());

            //if (this.seedDb)
            //{
            //    builder.ApplyConfiguration(new SeedCategoryEntityConfiguration());
            //}

            base.OnModelCreating(builder);
        }
    }
}
