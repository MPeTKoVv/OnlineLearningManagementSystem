namespace LearningSystem.Services.Data.Interfaces
{
    public interface IEnrollmentService
    {
        Task<string> CreateEnrollmentAndReturnIdAsync(string userId, int courseId);

    }
}
