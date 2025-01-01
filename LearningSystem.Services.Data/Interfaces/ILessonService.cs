namespace LearningSystem.Services.Data.Interfaces
{
    using LearningSystem.Web.ViewModels.Lesson;

    public interface ILessonService
    {
        Task AddByCourseIdAndFormModelAsync(int courseId, LessonFormModel formModel);

        Task EditByIdAndFormModelAsync(int id, LessonFormModel formModel);

        Task<IEnumerable<LessonViewModel>> GetAllByCourseIdAsync(int courseId);

        Task<LessonViewModel> GetDetailsByIdAsync(int id);

        Task<LessonFormModel> GetForEditingByIdAsync(int id);
        
        Task<bool> ExistByIdAsync(int id);

        Task<bool> IsTeachersLessonByIdAndCourseIdsync(string teacherId, int id);

        Task<DeleteLessonViewModel> GetForDeleteByIdAsync(int id);
        
        Task DeleteByIdAndViewModelAsync(int id, DeleteLessonViewModel viewModel);
    }
}
