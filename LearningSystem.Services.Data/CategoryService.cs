namespace LearningSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using LearningSystem.Data;
    using LearningSystem.Services.Data.Interfaces;
    using LearningSystem.Web.ViewModels.Category;

    public class CategoryService : ICategoryService
    {
        private readonly LearningSystemDbContext dbContext;

        public CategoryService(LearningSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CourseSelectCategoryFormModel>> AllCategoriesAsync()
        {
            var categoires = await dbContext
                .Categories
                .Select(c=> new CourseSelectCategoryFormModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

            return categoires;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            var result = await dbContext
                .Categories
                .AnyAsync(c => c.Id == id);

            return result;
        }
    }
}
