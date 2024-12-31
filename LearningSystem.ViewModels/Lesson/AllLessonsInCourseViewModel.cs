namespace LearningSystem.Web.ViewModels.Lesson
{
    public class AllLessonsInCourseViewModel
    {
        public AllLessonsInCourseViewModel()
        {
            Lessons = new HashSet<LessonViewModel>();
        }

        public int CourseId { get; set; }

        public IEnumerable<LessonViewModel> Lessons { get; set; }
    }
}
