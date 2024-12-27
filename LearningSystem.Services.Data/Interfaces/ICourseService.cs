namespace LearningSystem.Services.Data.Interfaces
{
    using LearningSystem.Web.ViewModels.Course;
    using LearningSystem.Services.Data.Models;

    public interface ICourseService
    {
        Task<int> CreateAndReturnIdAsync(CourseFormModel formModel, string teacherId);

        Task<AllCoursesFilteredAndPagedServiceModel> AllAsync(AllCoursesQueryModel queryModel);

        Task<IEnumerable<CourseViewModel>> GetByTeacherIdAsync(string teacherId);

        Task<IEnumerable<CourseViewModel>> TakeLastFour();

        Task<CourseViewModel> GetDetailsByIdAsync(int id);

        Task<bool> ExistsByIdAsync(int id);

        Task<IEnumerable<CourseViewModel>>GetEnrolledCoursesByUserIdAsync(string userId);

        Task AddEnrollmentAsync(int courseId, string enrollmentId);
    }
}
