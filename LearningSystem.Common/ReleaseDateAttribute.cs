namespace LearningSystem.Common
{
    using System.ComponentModel.DataAnnotations;

    public class ReleaseDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue.Date < DateTime.Today)
                {
                    return new ValidationResult("Release date cannot be earlier than today.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
