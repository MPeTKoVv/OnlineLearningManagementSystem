namespace LearningSystem.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using LearningSystem.Data.Models;
    using LearningSystem.Data.Configurations;

    public class LearningSystemDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public LearningSystemDbContext(DbContextOptions<LearningSystemDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Course> Courses { get; set; } = null!;

        public DbSet<Enrollment> Enrollments { get; set; } = null!;

        public DbSet<Lesson> Lessons { get; set; } = null!;

        public DbSet<Teacher> Teachers { get; set; } = null!;

        public DbSet<Rating> Ratings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CourseEntityConfiguration());
            builder.ApplyConfiguration(new EnrollmentEntityConfiguration());
            builder.ApplyConfiguration(new LessonEntityConfiguration());
            builder.ApplyConfiguration(new CategoryEntityConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
