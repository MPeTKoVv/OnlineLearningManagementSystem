namespace LearningSystem.Services.Data
{
    using System.Threading.Tasks;

    using LearningSystem.Data;
    using LearningSystem.Web.ViewModels.Course;
    using LearningSystem.Services.Data.Interfaces;
    using LearningSystem.Data.Models;

    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext dbContext;

        public CourseService(LearningSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> CreateAndReturnIdAsync(CourseFormModel formModel, string teacherId)
        {
            var course = new Course()
            {
                Name = formModel.Name,
                Description = formModel.Description,
                DurationInHours = formModel.DurationInHours,
                CategoryId = formModel.CategoryId,
                TeacherId = Guid.Parse(teacherId),
                Language = formModel.Language,
                Level = formModel.Level,
                ReleaseDate = formModel.ReleaseDate,
                Price = formModel.Price,
                OffersCertificate = formModel.OffersCertificate,
            };

            await dbContext.Courses.AddAsync(course);
            await dbContext.SaveChangesAsync();

            return course.Id;
        }
    }
}
