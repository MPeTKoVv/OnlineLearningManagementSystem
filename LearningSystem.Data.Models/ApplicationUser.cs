namespace LearningSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.ApplicationUser;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();

            Enrollments = new HashSet<Enrollment>();
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;
        
        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public bool Sex { get; set; }

        [Required]
        [MaxLength(IconUrlMaxLength)]
        public string IconUrl { get; set; } = null!;

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
