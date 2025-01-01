namespace LearningSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using System.Threading.Tasks;
    using System.Collections.Generic;

    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using Interfaces;
    using LearningSystem.Web.ViewModels.Lesson;
    using System.Runtime.InteropServices;
    using Microsoft.EntityFrameworkCore.Diagnostics;

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

        public async Task EditByIdAndFormModelAsync(int id, LessonFormModel formModel)
        {
            var lesson = await dbContext
                .Lessons
                .FirstAsync(l => l.Id == id);

            lesson.Title = formModel.Title;
            lesson.Description = formModel.Description;
            lesson.Content = formModel.Content;
            lesson.DurationInMinutes = formModel.DurationInMinutes;

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

        public async Task<LessonViewModel> GetDetailsByIdAsync(int id)
        {
            var lesson = await dbContext
                .Lessons
                .FirstAsync(l => l.Id == id);

            var details = new LessonViewModel()
            {
                Id = lesson.Id,
                Title = lesson.Title,
                CourseId = lesson.CourseId,
            };

            return details;
        }

        public async Task<LessonFormModel> GetForEditingByIdAsync(int id)
        {
            var lesson = await dbContext
                .Lessons
                .FirstAsync(l => l.Id == id);

            return new LessonFormModel
            {
                Title = lesson.Title,
                Description = lesson.Description,
                Content = lesson.Content,
                DurationInMinutes = lesson.DurationInMinutes,
                CourseId = lesson.CourseId
            };
        }

        public async Task<bool> ExistByIdAsync(int id)
        {
            var result = await dbContext
                .Lessons
                .AnyAsync(l => l.Id == id);

            return result;
        }

        public async Task<bool> IsTeachersLessonByIdAndCourseIdsync(string teacherId, int id)
        {
            int courseId = dbContext
                .Lessons
                .First(l => l.Id == id)
                .CourseId;

            var teacher = await dbContext
                .Teachers
                .Include(t => t.Courses)
                .FirstAsync(t => t.Id.ToString() == teacherId);

            var result = teacher.Courses.Any(c => c.Id == courseId);

            return result;
        }
    }
}
