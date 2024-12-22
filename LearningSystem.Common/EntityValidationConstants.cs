namespace LearningSystem.Common
{
    public static class EntityValidationConstants
    {
        public class ApplicationUser
        {
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;

            public const int FirstNameMinLength = 1;
            public const int FirstNameMaxLength = 20;

            public const int LastNameMinLength = 1;
            public const int LastNameMaxLength = 20;

            public const int ProfilePictureUrlMinLength = 10;
            public const int ProfilePictureUrlMaxLength = 256;
        }

        public class Course
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 500;

            public const int LanguageMinLength = 2;
            public const int LanguageMaxLength = 15;

            public const string PriceMinValue = "0";
            public const string PriceMaxValue = "10000";
        }

        public class Lesson
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 50;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 200;

            public const int ContentMinLength = 10;
            public const int ContentMaxLength = 2048;
        }

        public class Teacher
        {
            public const int BioMinLength = 5;
            public const int BioMaxLength = 500;

            public const int PhoneNumberMinLength = 7;
            public const int PhoneNumberMaxLength = 15;
        }

        public class Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;

            public const int IconUrlMinLength = 10;
            public const int IconUrlMaxLength = 256;
        }

        public class Level
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;
        }
    }
}
