namespace LearningSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using LearningSystem.Web.ViewModels.Course;
    using LearningSystem.Web.Infrastructure.Extensions;
    using LearningSystem.Services.Data.Models;
    using LearningSystem.Services.Data.Interfaces;

    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ITeacherService teacherService;
        private readonly ICategoryService categoryService;
        private readonly IEnrollmentService enrollmentService;
        private readonly IApplicationUserService applicationUserService;

        public CourseController(ICourseService courseService,
                                ITeacherService teacherService,
                                ICategoryService categoryService,
                                IEnrollmentService enrollmentService,
                                IApplicationUserService applicationUserService)
        {
            this.courseService = courseService;
            this.teacherService = teacherService;
            this.categoryService = categoryService;
            this.enrollmentService = enrollmentService;
            this.applicationUserService = applicationUserService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllCoursesQueryModel queryModel)
        {
            AllCoursesFilteredAndPagedServiceModel serviceModel =
                await courseService.AllAsync(queryModel);

            queryModel.Courses = serviceModel.Courses;
            queryModel.TotalCourses = serviceModel.TotalCourses;
            queryModel.Categories = await categoryService.AllCategoryNamesAsync();

            return View(queryModel);
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
                formModel.Categories = await categoryService.AllCategoriesAsync();

                return View(formModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> TeacherCourses()
        {
            var teacherExists = await teacherService.TeacherExistByUserId(this.User.GetId()!);
            if (!teacherExists)
            {
                return RedirectToAction("Become", "Teacher");
            }

            string teacherId = await teacherService.GetTeacherIdByUserId(this.User.GetId()!);
            var courses = await courseService.GetByTeacherIdAsync(teacherId);

            return View(courses);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var courseExists = await courseService.ExistsByIdAsync(id);
            if (!courseExists)
            {
                TempData[ErrorMessage] = "The given course does not exists!";

                return RedirectToAction("All", "Course");
            }

            var course = await courseService.GetDetailsByIdAsync(id);
            return View(course);
        }

        [HttpGet]
        public async Task<IActionResult> Enroll(int courseId)
        {
            var courseExists = await courseService.ExistsByIdAsync(courseId);
            if (!courseExists)
            {
                TempData[ErrorMessage] = "The given course does not exist!";
                return RedirectToAction("All");
            }

            string userId = this.User.GetId()!;
            var isEnrolledByTheUser = await applicationUserService.CourseIsEnrolledByIdAsync(userId, courseId);
            if (isEnrolledByTheUser)
            {
                TempData[ErrorMessage] = "The given course is already enrolled!";
                return RedirectToAction("EnrolledCourses");
            }

            try
            {
                string enrollmentId = await enrollmentService.CreateEnrollmentAndReturnIdAsync(userId, courseId);
                await applicationUserService.AddEnrollmentAsync(userId, enrollmentId);
                await courseService.AddEnrollmentAsync(courseId, enrollmentId);

                TempData[SuccessMessage] = "Course enrolled successfully!";
                return RedirectToAction("EnrolledCourses");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add course! Please try again later or contact administrator!");

                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> EnrolledCourses()
        {
            string userId = this.User.GetId()!;

            var courses = await courseService.GetEnrolledCoursesByUserIdAsync(userId);

            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var courseExists = await courseService.ExistsByIdAsync(id);
            if (!courseExists)
            {
                TempData[ErrorMessage] = "The given course does not exist!";

                return RedirectToAction("All", "Course");
            }

            string userId = this.User.GetId()!;
            var isUserTeacher = await teacherService.TeacherExistByUserId(userId);
            if (!isUserTeacher)
            {
                TempData[ErrorMessage] = "In order to edit a course you have to be a teacher!";
                return RedirectToAction("Index", "Home");
            }

            string teacherId = await teacherService.GetTeacherIdByUserId(userId);
            var isTeachersCourse = await teacherService.IsTeachersCourseByUserIdAsync(teacherId);
            if (!isTeachersCourse)
            {
                TempData[ErrorMessage] = "You can only edit your courses!";
                return RedirectToAction("TeachersCourses");
            }

            try
            {
                var formModel = await courseService.GetCourseForEditByIdAsync(id);
                formModel.Categories = await categoryService.AllCategoriesAsync();

                return View(formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add course! Please try again later or contact administrator!");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CourseFormModel formModel)
        {
            var courseExists = await courseService.ExistsByIdAsync(id);
            if (!courseExists)
            {
                TempData[ErrorMessage] = "The given course does not exist!";

                return RedirectToAction("All", "Course");
            }

            string userId = this.User.GetId()!;

            var isUserTeacher = await teacherService.TeacherExistByUserId(userId);
            if (!isUserTeacher)
            {
                TempData[ErrorMessage] = "You must be a teacher in order to edit course!";
                return RedirectToAction("Become", "Teacher");
            }

            string teacherId = await teacherService.GetTeacherIdByUserId(userId);
            var isTeachersCourse = await teacherService.IsTeachersCourseByUserIdAsync(teacherId);
            if (!isTeachersCourse)
            {
                TempData[ErrorMessage] = "You can only edit your courses!";
                return RedirectToAction("TeachersCourses");
            }

            var categoryExists = await categoryService.ExistsByIdAsync(formModel.CategoryId);
            if (!categoryExists)
            {
                this.ModelState.AddModelError(nameof(formModel.CategoryId), "Selected category does not exist!");
            }

            if (formModel.StartDate <= DateTime.Now)
            {
                ModelState.Remove(nameof(formModel.StartDate));
            }

            if (!ModelState.IsValid)
            {
                formModel.Categories = await categoryService.AllCategoriesAsync();

                return View(formModel);
            }

            try
            {
                await courseService.EditByIdAsync(id, formModel);

            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add course! Please try again later or contact administrator!");
                formModel.Categories = await categoryService.AllCategoriesAsync();

                return View(formModel);
            }

            TempData[SuccessMessage] = "Course was edited successfully!";
            return RedirectToAction("Details", "Course", new { id });
        }
    }
}
