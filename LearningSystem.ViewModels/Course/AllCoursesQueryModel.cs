namespace LearningSystem.Web.ViewModels.Course
{
    using LearningSystem.Data.Models.Enums;
    using LearningSystem.Web.ViewModels.Course.Enum;
    using System.ComponentModel.DataAnnotations;
    using static LearningSystem.Common.GeneralApplicationConstants;

    public class AllCoursesQueryModel
    {
        public AllCoursesQueryModel()
        {
            CurrentPage = DefaultPage;
            CoursesPerPage = EntitiesPerPage;

            Categories = new HashSet<string>();
            Courses = new HashSet<CourseViewModel>();

        }
        public string? Category { get; set; }


        [Display(Name = "Search by word")]
        public string? SearchString { get; set; }

        public Level Level { get; set; }

        [Display(Name = "Sort Courses By")]
        public CourseSorting CourseSorting { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Show Courses On Page")]
        public int CoursesPerPage { get; set; }

        public int TotalCourses { get; set; }

        public IEnumerable<string> Categories { get; set; }


        public IEnumerable<CourseViewModel> Courses { get; set; }
    }

}

