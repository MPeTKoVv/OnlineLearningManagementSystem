using LearningSystem.Web.ViewModels.Course;

namespace LearningSystem.Services.Data.Interfaces
{
    public interface IApplicationUserService
    {
        Task<string> GetUserFullNameByEmail(string userId);

        Task<IEnumerable<CourseViewModel>> GetEnrolledCoursesByUserId(string id);
    }
}
