namespace LearningSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Course;

    public class Course
    {
        public Course()
        {
            Lessons = new HashSet<Lesson>();
            Enrollments = new HashSet<Enrollment>();
            Ratings = new HashSet<Rating>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;
        public double DurationInHours { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        public Guid TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; } = null!;

        [Required]
        [MaxLength(LanguageMaxLength)]
        public string Language { get; set; } = null!;

        public Level Level { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime ReleaseDate { get; set; }

        public decimal Price { get; set; }

        public double Rating { get; set; }

        public bool OffersCertificate { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; } = null!;
        public virtual ICollection<Enrollment> Enrollments { get; set; } = null!;
        public virtual ICollection<Rating> Ratings { get; set; } = null!;

    }
}
