namespace LearningSystem.Web.ViewModels.Teacher
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Teacher;

    public class BecomeTeacherFormModel
    {
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Bio is required.")]
        [StringLength(500, MinimumLength = BioMinLength, ErrorMessage = "Bio cannot exceed 500 characters.")]
        public string Bio { get; set; } = null!;
    }
}
