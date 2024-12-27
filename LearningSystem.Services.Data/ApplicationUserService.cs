namespace LearningSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using System.Threading.Tasks;

    using LearningSystem.Data;
    using LearningSystem.Services.Data.Interfaces;
    using LearningSystem.Web.ViewModels.Course;

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly LearningSystemDbContext dbContext;

        public ApplicationUserService(LearningSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> GetUserFullNameByEmail(string email)
        {
            var user = await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }

        public async Task<IEnumerable<CourseViewModel>> GetEnrolledCoursesByUserId(string id)
        {
            var user = await dbContext
                .Users
                .FirstAsync(u => u.Id.ToString() == id);

            var courses = user.Enrollments
                .Select(c => new CourseViewModel
                {
                    Id = c.Course.Id,
                    Name = c.Course.Name,
                    ImageUrl = c.Course.Category.IconUrl,
                    CategoryName = c.Course.Category.Name,
                    Price = c.Course.Price,
                    Level = c.Course.Level.ToString(),
                    OffersCertificate = c.Course.OffersCertificate,
                })
                .ToArray();

            return courses;
        }

        public async Task AddEnrollmentAsync(string userId, string enrollmentId)
        {
            var enrollment = await dbContext
                .Enrollments
                .FirstAsync(e => e.Id.ToString() == enrollmentId);

            var user = await dbContext
                .Users
                .FirstAsync(u => u.Id.ToString() == userId);

            user.Enrollments.Add(enrollment);

            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> CourseIsEnrolledByIdAsync(string userId, int courseId)
        {
            var user = await dbContext
                .Users
                .Include(u=>u.Enrollments)
                .FirstAsync(u => u.Id.ToString() == userId);

            var isEnrolled = user
                .Enrollments
                .Any(e => e.CourseId == courseId);

            return isEnrolled;
        }
    }
}
