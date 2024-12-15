namespace LearningSystem.Services.Data.Interfaces
{
    public interface ITeacherService
    {
        Task<bool> TeacherExistByUserId(string userId);
    }
}
