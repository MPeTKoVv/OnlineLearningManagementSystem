namespace LearningSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Category;

    public class Category
    {
        public Category()
        {
            Courses = new HashSet<Course>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(IconUrlMaxLength)]
        public string IconUrl { get; set; } = null!;

        public virtual ICollection<Course> Courses { get; set; }
    }
}