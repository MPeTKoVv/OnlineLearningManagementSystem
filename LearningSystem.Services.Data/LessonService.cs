namespace LearningSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using System.Threading.Tasks;
    using System.Collections.Generic;

    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using Interfaces;
    using LearningSystem.Web.ViewModels.Lesson;

    public class LessonService : ILessonService
    {
        private readonly LearningSystemDbContext dbContext;

        public LessonService(LearningSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddByCourseIdAndFormModelAsync(int courseId, LessonFormModel formModel)
        {
            var lesson = new Lesson
            {
                Title = formModel.Title,
                Description = formModel.Description,
                Content = formModel.Content,
                DurationInMinutes = formModel.DurationInMinutes,
                CourseId = courseId,
            };

            await dbContext.Lessons.AddAsync(lesson);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<LessonViewModel>> GetAllByCourseIdAsync(int courseId)
        {
            var lessons = await dbContext
                .Lessons
                .Where(l => l.CourseId == courseId)
                .Select(l => new LessonViewModel
                {
                    Id = l.Id,
                    Title = l.Title,
                    CourseId = l.CourseId
                })
                .ToArrayAsync();

            return lessons;
        }
    }
}
