namespace LearningSystem.Web.ViewModels.Course
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
        
        public string CategoryName { get; set; } = null!;

        public decimal Price { get; set; }

        public string Level { get; set; } = null!;

        public bool OffersCertificate { get; set; }
    }

}
