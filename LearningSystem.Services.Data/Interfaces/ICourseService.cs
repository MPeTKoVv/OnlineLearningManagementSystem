namespace LearningSystem.Services.Data.Interfaces
{
    using LearningSystem.Web.ViewModels.Course;

    public interface ICourseService
    {
        Task<int> CreateAndReturnIdAsync(CourseFormModel formModel, string teacherId);
    }
}
