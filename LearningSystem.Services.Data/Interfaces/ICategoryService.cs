namespace LearningSystem.Services.Data.Interfaces
{
    using LearningSystem.Web.ViewModels.Category;

    public interface ICategoryService
    {
        Task<IEnumerable<CourseSelectCategoryFormModel>> AllCategoriesAsync();

        Task<bool> ExistsByIdAsync(int id);
    }
}
