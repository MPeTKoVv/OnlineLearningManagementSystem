namespace LearningSystem.Services.Data.Interfaces
{
    using LearningSystem.Web.ViewModels.Teacher;

    public interface ITeacherService
    {
        Task<bool> TeacherExistByUserId(string userId);

        Task<bool> TeacherExistByPhoneNumber(string phoneNumber);

        Task Create(string userId, BecomeTeacherFormModel formModel);

        Task<string> GetTeacherIdByUserId(string userId);

        Task<bool> IsTeachersCourseByUserIdAsync(string teacherId);
    }
}
