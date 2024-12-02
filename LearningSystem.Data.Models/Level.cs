namespace LearningSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Level;

    public class Level
    {
        public Level()
        {
            this.Courses = new HashSet<Course>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Course> Courses { get; set; }
    }
}