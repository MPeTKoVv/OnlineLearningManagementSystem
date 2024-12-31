namespace LearningSystem.Web.ViewModels.Lesson
{
    using System.ComponentModel.DataAnnotations;

    using static LearningSystem.Common.EntityValidationConstants.Lesson;

    public class LessonFormModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; } = null!;

        [Range(typeof(int), DurationMinValue, DurationMaxValue)]
        [Display(Name = "Duration (in minutes)")]
        public int DurationInMinutes { get; set; }

        public int CourseId { get; set; }
    }
}