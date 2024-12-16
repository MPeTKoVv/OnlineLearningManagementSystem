namespace LearningSystem.Web.ViewModels.Teacher
{
    using System.ComponentModel.DataAnnotations;

    public class BecomeTeacherFormModel
    {
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Biography is required.")]
        [StringLength(500, ErrorMessage = "Biography cannot exceed 500 characters.")]
        public string Biography { get; set; } = null!;
    }
}
