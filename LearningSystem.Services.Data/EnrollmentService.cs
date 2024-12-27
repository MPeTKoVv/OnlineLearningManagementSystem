namespace LearningSystem.Services.Data
{
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class EnrollmentService : IEnrollmentService
    {
        private readonly LearningSystemDbContext dbContext;

        public EnrollmentService(LearningSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateEnrollmentAndReturnIdAsync(string userId, int courseId)
        {
            var enrollment = new Enrollment()
            {
                UserId = Guid.Parse(userId),
                CourseId = courseId,
                PaymentCompleted = true
            };

            await dbContext.Enrollments.AddAsync(enrollment);
            await dbContext.SaveChangesAsync();

            string enrollmentId = enrollment.Id.ToString();
            return enrollmentId;
        }
    }
}
