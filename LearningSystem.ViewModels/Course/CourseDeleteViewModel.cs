namespace LearningSystem.Web.ViewModels.Course
{
    public class CourseDeleteViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public DateTime StartDate { get; set; }
        
        public string Description { get; set; } = null!;
    }
}
