namespace LearningSystem.Services.Data.Interfaces
{
    public interface IApplicationUserService
    {
        Task<string> GetUserFullNameByEmail(string userId);
    }
}
