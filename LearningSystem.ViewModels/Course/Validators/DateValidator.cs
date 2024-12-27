namespace LearningSystem.Web.ViewModels.Course.Validators
{
    using System.ComponentModel.DataAnnotations;

    public class DateValidator
    {
        public static ValidationResult? ValidateStartDate(DateTime startDate, ValidationContext context)
        {
            if (startDate < DateTime.Now)
            {
                return new ValidationResult("The Start Date cannot be in the past.");
            }

            return ValidationResult.Success;
        }

        public static ValidationResult? ValidateEndDate(DateTime endDate, ValidationContext context)
        {
            var instance = context.ObjectInstance as CourseFormModel;
            if (instance == null || endDate <= instance.StartDate)
            {
                return new ValidationResult("The End Date must be after the start date.");
            }

            return ValidationResult.Success;
        }
    }
}
