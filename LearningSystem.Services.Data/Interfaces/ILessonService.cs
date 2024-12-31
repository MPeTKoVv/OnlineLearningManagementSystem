namespace LearningSystem.Services.Data.Interfaces
{
    using LearningSystem.Web.ViewModels.Lesson;

    public interface ILessonService
    {
        Task AddByCourseIdAndFormModelAsync(int courseId, LessonFormModel formModel);

        Task<IEnumerable<LessonViewModel>> GetAllByCourseIdAsync(int courseId);
    }
}
