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

        public async Task<string> GetUserFullName(string userId)
        {
            var user = await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            string fullName = user.FirstName + " " + user.LastName;

            return fullName;
        }
    }
}
