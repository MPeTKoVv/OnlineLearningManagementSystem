namespace LearningSystem.Web.Controllers
{
    using System.Diagnostics;
    
    using Microsoft.AspNetCore.Mvc;
    
    using LearningSystem.Web.ViewModels;
    using LearningSystem.Services.Data.Interfaces;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseService courseService;

        public HomeController(ILogger<HomeController> logger, ICourseService courseService)
        {
            _logger = logger;
            this.courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await courseService.TakeLastFour();
            return View(courses);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
