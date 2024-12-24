namespace LearningSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using System.Threading.Tasks;

    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Web.ViewModels.Course;
    using LearningSystem.Services.Data.Interfaces;
    using LearningSystem.Services.Data.Models;
    using LearningSystem.Web.ViewModels.Course.Enum;
    using LearningSystem.Data.Models.Enums;

    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext dbContext;

        public CourseService(LearningSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AllCoursesFilteredAndPagedServiceModel> AllAsync(AllCoursesQueryModel queryModel)
        {
            IQueryable<Course> coursesQuery = dbContext
                .Courses
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                coursesQuery = coursesQuery
                    .Where(c => c.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                coursesQuery = coursesQuery
                    .Where(c => EF.Functions.Like(c.Name, wildCard) ||
                                EF.Functions.Like(c.Category.Name, wildCard) ||
                                EF.Functions.Like(c.Description, wildCard));
            }

            coursesQuery = queryModel.Level switch
            {
                Level.Beginner => coursesQuery
                    .Where(c => c.Level == Level.Beginner),
                Level.Intermediate => coursesQuery
                    .Where(c => c.Level == Level.Intermediate),
                Level.Advanced => coursesQuery
                    .Where(c => c.Level == Level.Advanced),
                Level.Expert => coursesQuery
                    .Where(c => c.Level == Level.Expert),
                _ => coursesQuery
            };

            coursesQuery = queryModel.CourseSorting switch
            {
                CourseSorting.Newest => coursesQuery
                    .OrderByDescending(h => h.CreatedAt),
                CourseSorting.Oldest => coursesQuery
                    .OrderBy(h => h.CreatedAt),
                CourseSorting.PriceAscending => coursesQuery
                    .OrderBy(h => h.Price),
                CourseSorting.PriceDescending => coursesQuery
                    .OrderByDescending(h => h.Price),
                CourseSorting.HighestRated => coursesQuery
                .OrderByDescending(h => h.Rating),
                _ => coursesQuery
                    .OrderByDescending(h => h.CreatedAt)
            };

            IEnumerable<CourseViewModel> allCourses = await coursesQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.CoursesPerPage)
                .Take(queryModel.CoursesPerPage)
                .Select(c => new CourseViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImageUrl = c.Category.IconUrl,
                    CategoryName = c.Category.Name,
                    Price = c.Price,
                    Level = c.Level.ToString(),
                    OffersCertificate = c.OffersCertificate,
                })
                .ToArrayAsync();

            int totalCourses = coursesQuery.Count();

            return new AllCoursesFilteredAndPagedServiceModel()
            {
                TotalCourses = totalCourses,
                Courses = allCourses
            };
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

        public async Task<IEnumerable<CourseViewModel>> GetByTeacherIdAsync(string teacherId)
        {
            var courses = await dbContext
                .Courses
                .Where(c => c.TeacherId.ToString() == teacherId)
                .Select(c => new CourseViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImageUrl = c.Category.IconUrl,
                    CategoryName = c.Category.Name,
                    Price = c.Price,
                    Level = c.Level.ToString(),
                    OffersCertificate = c.OffersCertificate,
                })
                .ToArrayAsync();

            return courses;
        }

        public async Task<IEnumerable<CourseViewModel>> TakeLastFour()
        {
            var courses = await dbContext
                .Courses
                .Take(4)
                .Select(c => new CourseViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImageUrl = c.Category.IconUrl,
                    CategoryName = c.Category.Name,
                    Price = c.Price,
                    Level = c.Level.ToString(),
                    OffersCertificate = c.OffersCertificate,
                })
                .ToArrayAsync();

            return courses;
        }
    }
}
