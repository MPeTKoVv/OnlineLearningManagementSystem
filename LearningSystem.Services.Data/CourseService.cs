namespace LearningSystem.Services.Data
{
    using LearningSystem.Data;
    using LearningSystem.Services.Data.Interfaces;
 
    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext dbContext;

        public CourseService(LearningSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


    }
}
