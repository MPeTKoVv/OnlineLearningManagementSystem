namespace LearningSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using LearningSystem.Data;
    using LearningSystem.Services.Data.Interfaces;
    using LearningSystem.Web.ViewModels.Teacher;
    using LearningSystem.Data.Models;

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

        public async Task<bool> TeacherExistByPhoneNumber(string phoneNumber)
        {
            bool teacherExist = await this.dbContext
                .Teachers
                .AnyAsync(t => t.PhoneNumber == phoneNumber);

            return teacherExist;
        }

        public async Task Create(string userId, BecomeTeacherFormModel formModel)
        {
            var teacher = new Teacher()
            {
                UserId = Guid.Parse(userId),
                PhoneNumber = formModel.PhoneNumber,
                Bio = formModel.Bio,
            };

            await dbContext.Teachers.AddAsync(teacher);
            await dbContext.SaveChangesAsync();
        }

    }
}
