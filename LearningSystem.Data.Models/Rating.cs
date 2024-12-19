namespace LearningSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Rating
    {
        [Key]
        public Guid Id { get; set; }

        public int Stars { get; set; }

        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;

        public Guid EnrollmentId { get; set; }
        public virtual Enrollment Enrollment { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}