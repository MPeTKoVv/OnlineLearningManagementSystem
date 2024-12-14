namespace LearningSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    using static Common.EntityValidationConstants.ApplicationUser;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
            Occupation = Occupation.Student;

            Enrollments = new HashSet<Enrollment>();
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [MaxLength(ProfilePictureUrlMaxLength)]
        public string? ProfilePictureUrl { get; set; }

        public Gender Gender { get; set; }

        public Occupation Occupation { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
