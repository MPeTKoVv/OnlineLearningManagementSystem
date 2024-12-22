namespace LearningSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LearningSystem.Web.ViewModels.Course;
    using LearningSystem.Web.Infrastructure.Extensions;
    using LearningSystem.Services.Data.Interfaces;
    using static Common.NotificationMessagesConstants;

    public class CourseController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ITeacherService teacherService;
        private readonly ICategoryService categoryService;

        public CourseController(ICourseService courseService, ITeacherService teacherService, ICategoryService categoryService)
        {
            this.courseService = courseService;
            this.teacherService = teacherService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            string userId = this.User.GetId()!;

            var isUserTeacher = await teacherService.TeacherExistByUserId(userId);
            if (!isUserTeacher)
            {
                TempData[ErrorMessage] = "In order to add a course you have to be a teacher!";
                return RedirectToAction("Index", "Home");
            }

            var formModel = new CourseFormModel()
            {
                Categories = await categoryService.AllCategoriesAsync()
            };

            return View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CourseFormModel formModel)
        {
            string userId = this.User.GetId()!;

            var isUserTeacher = await teacherService.TeacherExistByUserId(userId);
            if (!isUserTeacher)
            {
                TempData[ErrorMessage] = "You must be a teacher in order to add course!";
                return RedirectToAction("Become", "Teacher");
            }

            var categoryExists = await categoryService.ExistsByIdAsync(formModel.CategoryId);
            if (!categoryExists)
            {
                this.ModelState.AddModelError(nameof(formModel.CategoryId), "Selected category does not exist!");
            }

            var releaseDate = formModel.ReleaseDate;
            if (releaseDate < DateTime.Now)
            {
                this.ModelState.AddModelError(nameof(formModel.ReleaseDate), "Selected date does not exist!");
            }

            if (!ModelState.IsValid)
            {
                formModel.Categories = await categoryService.AllCategoriesAsync();

                return View(formModel);
            }

            try
            {
                string teacherId = await teacherService.GetTeacherIdByUserId(userId);

                await courseService.CreateAndReturnIdAsync(formModel, teacherId);

                TempData[SuccessMessage] = "Course was added successfully!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add course! Please try again later or contact administrator!");
                formModel.Categories = await this.categoryService.AllCategoriesAsync();

                return View(formModel);
            }
        }
    }
}
