namespace LearningSystem.Web.ViewModels.Lesson
{
    public class LessonViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public int CourseId { get; set; }
    }
}
