﻿namespace LearningSystem.Data.Models
{
    using static Common.EntityValidationConstants.Teacher;

    using System.ComponentModel.DataAnnotations;

    public class Teacher
    {
        public Teacher()
        {
            Id = Guid.NewGuid();

            Courses = new HashSet<Course>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [MaxLength(BioMaxLength)]
        public string Bio { get; set; } = null!;

        public ICollection<Course> Courses { get; set; }
    }
}
