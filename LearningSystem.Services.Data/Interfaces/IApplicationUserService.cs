namespace LearningSystem.Services.Data.Interfaces
{
    public interface IApplicationUserService
    {
        Task<string> GetUserFullName(string userId);
    }
}
