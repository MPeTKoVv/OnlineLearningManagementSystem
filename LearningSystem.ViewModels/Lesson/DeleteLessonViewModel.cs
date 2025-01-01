namespace LearningSystem.Web.ViewModels.Lesson
{
    public class DeleteLessonViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public int CourseId { get; set; }
    }
}
