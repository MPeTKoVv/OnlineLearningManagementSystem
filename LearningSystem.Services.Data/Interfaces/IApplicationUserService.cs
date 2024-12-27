using LearningSystem.Web.ViewModels.Course;

namespace LearningSystem.Services.Data.Interfaces
{
    public interface IApplicationUserService
    {
        Task<string> GetUserFullNameByEmail(string userId);

        Task<IEnumerable<CourseViewModel>> GetEnrolledCoursesByUserId(string id);

        Task AddEnrollmentAsync(string userId, string enrollmentId);

        Task<bool> CourseIsEnrolledByIdAsync(string userId, int courseId);
    }
}
