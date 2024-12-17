namespace LearningSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using System.Threading.Tasks;
    
    using LearningSystem.Data;
    using LearningSystem.Services.Data.Interfaces;

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly LearningSystemDbContext dbContext;

        public ApplicationUserService(LearningSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> GetUserFullNameByEmail(string email)
        {
            var user = await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }
    }
}
