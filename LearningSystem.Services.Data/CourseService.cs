namespace LearningSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using System.Threading.Tasks;

    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Data.Models.Enums;
    using LearningSystem.Services.Data.Interfaces;
    using LearningSystem.Services.Data.Models;
    using LearningSystem.Web.ViewModels.Course;
    using LearningSystem.Web.ViewModels.Course.Enum;

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
                .Where(c => c.IsActive)
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
                    .OrderByDescending(h => h.CreatedOn),
                CourseSorting.Oldest => coursesQuery
                    .OrderBy(h => h.CreatedOn),
                CourseSorting.PriceAscending => coursesQuery
                    .OrderBy(h => h.Price),
                CourseSorting.PriceDescending => coursesQuery
                    .OrderByDescending(h => h.Price),
                CourseSorting.HighestRated => coursesQuery
                .OrderByDescending(h => h.Rating),
                _ => coursesQuery
                    .OrderByDescending(h => h.CreatedOn)
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
            Course course = new Course()
            {
                Name = formModel.Name,
                Description = formModel.Description,
                DurationInHours = formModel.DurationInHours,
                CategoryId = formModel.CategoryId,
                TeacherId = Guid.Parse(teacherId),
                Language = formModel.Language,
                Level = formModel.Level,
                StartDate = formModel.StartDate,
                EndDate = formModel.EndDate,
                Price = formModel.Price,
                OffersCertificate = formModel.OffersCertificate,
            };

            await dbContext.Courses.AddAsync(course);
            await dbContext.SaveChangesAsync();

            return course.Id;
        }

        public async Task<string> EnrollCourseAndReturnEntrollmentIdAsync(int id, string userId)
        {
            var course = await dbContext
                .Courses
                .FirstAsync(c => c.Id == id);

            var entrollment = new Enrollment()
            {
                UserId = Guid.Parse(userId),
                CourseId = id
            };

            course.Enrollments.Add(entrollment);

            await dbContext.Enrollments.AddAsync(entrollment);
            await dbContext.SaveChangesAsync();

            return entrollment.Id.ToString();

        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            var exists = await dbContext
                .Courses
                .AnyAsync(c => c.Id == id /*&& c.IsActive*/);

            return exists;
        }

        public async Task<CourseViewModel> GetDetailsByIdAsync(int id)
        {
            var course = await dbContext
                .Courses
                .Include(c => c.Category)
                .FirstAsync(c => c.Id == id);

            return new CourseDetailsViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                ImageUrl = course.Category.IconUrl,
                CategoryName = course.Category.Name,
                Price = course.Price,
                Level = course.Level.ToString(),
                OffersCertificate = course.OffersCertificate,
            };
        }

        public async Task<IEnumerable<CourseViewModel>> GetByTeacherIdAsync(string teacherId)
        {
            var courses = await dbContext
                .Courses
                .Where(c => c.TeacherId.ToString() == teacherId && c.IsActive)
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
                .Where(c => c.IsActive)
                .OrderByDescending(c => c.CreatedOn)
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

        public async Task<IEnumerable<CourseViewModel>> GetEnrolledCoursesByUserIdAsync(string userId)
        {
            var user = await dbContext
                .Users
                .Include(u => u.Enrollments)
                .ThenInclude(e => e.Course)
                .ThenInclude(c => c.Category)
                .FirstAsync(u => u.Id.ToString() == userId);


            var courses = user
                .Enrollments
                .Select(e => new CourseViewModel
                {
                    Id = e.Course.Id,
                    Name = e.Course.Name,
                    ImageUrl = e.Course.Category.IconUrl,
                    CategoryName = e.Course.Category.Name,
                    Price = e.Course.Price,
                    Level = e.Course.Level.ToString(),
                    OffersCertificate = e.Course.OffersCertificate,
                })
                .ToArray();

            return courses;
        }

        public async Task AddEnrollmentAsync(int courseId, string enrollmentId)
        {
            var enrollment = await dbContext
                .Enrollments
                .FirstAsync(e => e.Id.ToString() == enrollmentId);

            var course = await dbContext
                .Courses
                .FirstAsync(c => c.Id == courseId);

            course.Enrollments.Add(enrollment);

            await dbContext.SaveChangesAsync();
        }

        public async Task<CourseFormModel> GetCourseForEditByIdAsync(int courseId)
        {
            var course = await dbContext
                .Courses
                .FirstAsync(c => c.Id == courseId);

            return new CourseFormModel
            {
                Name = course.Name,
                Description = course.Description,
                DurationInHours = course.DurationInHours,
                CategoryId = course.CategoryId,
                Level = course.Level,
                Language = course.Language,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                Price = course.Price,
                OffersCertificate = course.OffersCertificate,

            };
        }

        public async Task EditByIdAsync(int courseId, CourseFormModel formModel)
        {
            var course = await dbContext
                .Courses
                .FirstAsync(c => c.Id == courseId);

            course.Name = formModel.Name;
            course.Description = formModel.Description;
            course.DurationInHours = formModel.DurationInHours;
            course.CategoryId = formModel.CategoryId;
            course.Level = formModel.Level;
            course.Language = formModel.Language;
            course.StartDate = formModel.StartDate;
            course.EndDate = formModel.EndDate;
            course.Price = formModel.Price;
            course.OffersCertificate = formModel.OffersCertificate;

            await dbContext.SaveChangesAsync();
        }

        public async Task<CourseDeleteViewModel> GetCourseForDeleteByIdAsync(int id)
        {
            var course = await dbContext
                .Courses
                .Include(c => c.Category)
                .FirstAsync(c => c.Id == id);

            var viewModel = new CourseDeleteViewModel()
            {
                Id = course.Id,
                Name = course.Name,
                ImageUrl = course.Category.IconUrl,
                CategoryName = course.Category.Name,
                StartDate = course.StartDate,
                Description = course.Description,
            };

            return viewModel;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var course = await dbContext
                .Courses
                .FirstAsync(c => c.Id == id);

            course.IsActive = false;

            await dbContext.SaveChangesAsync();
        }
    }
}
