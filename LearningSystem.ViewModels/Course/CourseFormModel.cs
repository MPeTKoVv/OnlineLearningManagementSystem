namespace LearningSystem.Web.ViewModels.Course
{
    using System.ComponentModel.DataAnnotations;
    using LearningSystem.Common;
    using LearningSystem.Data.Models;
    using LearningSystem.Web.ViewModels.Category;
    using static LearningSystem.Common.EntityValidationConstants.Course;

    public class CourseFormModel
    {
        public CourseFormModel()
        {
            Categories = new HashSet<CourseSelectCategoryFormModel>();
        }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Display(Name = "Duration (in hours)")]
        public double DurationInHours { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Level Level { get; set; }

        [Required]
        [StringLength(LanguageMaxLength, MinimumLength = LanguageMinLength)]
        public string Language { get; set; } = null!;

        [Display(Name = "Release Date")]

        public DateTime ReleaseDate { get; set; }

        [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        [Display(Name = "Offers Certificate")]
        public bool OffersCertificate { get; set; }

        public IEnumerable<CourseSelectCategoryFormModel> Categories { get; set; } = null!;
    }
}