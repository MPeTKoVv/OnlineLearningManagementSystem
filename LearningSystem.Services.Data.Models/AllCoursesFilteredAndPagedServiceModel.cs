namespace LearningSystem.Services.Data.Models
{
    using LearningSystem.Web.ViewModels.Course;
 
    public class AllCoursesFilteredAndPagedServiceModel
    {
        public AllCoursesFilteredAndPagedServiceModel()
        {
            Courses = new HashSet<CourseViewModel>();
        }

        public int TotalCourses { get; set; }

        public IEnumerable<CourseViewModel> Courses { get; set; }
    }
}
