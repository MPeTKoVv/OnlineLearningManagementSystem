namespace LearningSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using LearningSystem.Data;
    using LearningSystem.Services.Data.Interfaces;

    public class TeacherService : ITeacherService
    {
        private readonly LearningSystemDbContext dbContext;

        public TeacherService(LearningSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> TeacherExistByUserId(string userId)
        {
            bool teacherExist = await this.dbContext
                .Teachers
                .AnyAsync(t => t.UserId.ToString() == userId);

            return teacherExist;
        }
    }
}
