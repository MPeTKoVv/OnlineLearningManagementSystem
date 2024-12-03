namespace LearningSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using LearningSystem.Data.Models;

    public class SeedCategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(GenerateCategories());
        }

        private Category[] GenerateCategories()
        {
            ICollection<Category> categories = new HashSet<Category>();

            Category category;

            category = new Category()
            {
                Id = 1,
                Name = "Programming",
                IconUrl = "https://www.reshot.com/preview-assets/icons/6WRACM8V2S/code-learning-6WRACM8V2S.svg"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 2,
                Name = "Design",
                IconUrl = "https://www.reshot.com/preview-assets/icons/U8KFG2YBNS/modern-art-U8KFG2YBNS.svg"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 3,
                Name = "Marketing",
                IconUrl = "https://www.reshot.com/preview-assets/icons/ZMDWAS6837/educational-marketing-ZMDWAS6837.svg"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 4,
                Name = "Business",
                IconUrl = "https://www.reshot.com/preview-assets/icons/BTWNSGCXD5/economic-education-BTWNSGCXD5.svg"
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}