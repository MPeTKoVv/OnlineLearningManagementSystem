namespace LearningSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Enrollment
    {
        public Enrollment()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;

        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;

        public DateTime EnrolledAt { get; set; } = DateTime.Now;

        public bool PaymentCompleted { get; set; }

        public Guid? RatingId { get; set; }
        public virtual Rating? Rating { get; set; }
    }
}