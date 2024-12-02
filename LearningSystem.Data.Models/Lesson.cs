namespace LearningSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Lesson;

    public class Lesson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Content { get; set; } = null!;


        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;

        public int Order { get; set; }

        // Indicates if this lesson is available as a free preview
        public bool IsPreview { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int DurationInMinutes { get; set; } 
    }
}
