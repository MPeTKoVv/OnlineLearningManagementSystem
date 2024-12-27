namespace LearningSystem.Web.ViewModels.Course
{
    using System.ComponentModel.DataAnnotations;

    using LearningSystem.Data.Models.Enums;
    using LearningSystem.Web.ViewModels.Category;
    using LearningSystem.Web.ViewModels.Course.Validators;

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

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "The Start Date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "The date format is invalid. Please use the format mm-dd-yyyy --:-- --.")]
        [CustomValidation(typeof(DateValidator), nameof(DateValidator.ValidateStartDate))]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "The End Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "The date format is invalid. Please use the format mm-dd-yyyy.")]
        [CustomValidation(typeof(DateValidator), nameof(DateValidator.ValidateEndDate))]
        public DateTime EndDate { get; set; }

        [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        [Display(Name = "Offers Certificate")]
        public bool OffersCertificate { get; set; }

        public IEnumerable<CourseSelectCategoryFormModel> Categories { get; set; } = null!;
    }
}