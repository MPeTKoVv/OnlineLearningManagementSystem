﻿namespace LearningSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using LearningSystem.Services.Data.Interfaces;
    using LearningSystem.Web.ViewModels.Lesson;
    using LearningSystem.Web.Infrastructure.Extensions;

    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class LessonController : Controller
    {
        private readonly ILessonService lessonService;
        private readonly ICourseService courseService;
        private readonly ITeacherService teacherService;

        public LessonController(ILessonService lessonService,
                                ICourseService courseService,
                                ITeacherService teacherService)
        {
            this.lessonService = lessonService;
            this.courseService = courseService;
            this.teacherService = teacherService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> AllInCourse(int courseId)
        {
            var courseExist = await courseService.ExistsByIdAsync(courseId);
            if (!courseExist)
            {
                TempData[ErrorMessage] = "The given course does not exist!";

                return RedirectToAction("All", "Course");
            }

            var lessons = new AllLessonsInCourseViewModel()
            {
                CourseId = courseId,
                Lessons = await lessonService.GetAllByCourseIdAsync(courseId)
            };

            return View(lessons);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int courseId)
        {
            var courseExist = await courseService.ExistsByIdAsync(courseId);
            if (!courseExist)
            {
                TempData[ErrorMessage] = "The given course does not exist!";

                return RedirectToAction("All", "Course");
            }

            string userId = this.User.GetId()!;
            var isUserTeacher = await teacherService.TeacherExistByUserId(userId);
            if (!isUserTeacher)
            {
                TempData[ErrorMessage] = "You must be a teacher in order to add lesson!";

                return RedirectToAction("Become", "Teacher");
            }

            string teacherId = await teacherService.GetTeacherIdByUserId(this.User.GetId()!);
            var isTeachersCourse = await teacherService.IsTeachersCourseByIdAndCourseIdsync(teacherId, courseId);
            if (!isTeachersCourse)
            {
                TempData[ErrorMessage] = "You can only add lessons to your courses!";

                return RedirectToAction("TeacherCourses", "Course");
            }

            var formModel = new LessonFormModel();

            return View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int courseId, LessonFormModel formModel)
        {
            var courseExist = await courseService.ExistsByIdAsync(courseId);
            if (!courseExist)
            {
                TempData[ErrorMessage] = "The given course does not exist!";

                return RedirectToAction("All", "Course");
            }

            string userId = this.User.GetId()!;
            var isUserTeacher = await teacherService.TeacherExistByUserId(userId);
            if (!isUserTeacher)
            {
                TempData[ErrorMessage] = "You must be a teacher in order to add lesson!";

                return RedirectToAction("Become", "Teacher");
            }

            string teacherId = await teacherService.GetTeacherIdByUserId(this.User.GetId()!);
            var isTeachersCourse = await teacherService.IsTeachersCourseByIdAndCourseIdsync(teacherId, courseId);
            if (!isTeachersCourse)
            {
                TempData[ErrorMessage] = "You can only add lessons to your courses!";

                return RedirectToAction("TeacherCourses", "Course");
            }

            if (!ModelState.IsValid)
            {
                return View(formModel);
            }

            try
            {
                await lessonService.AddByCourseIdAndFormModelAsync(courseId, formModel);

                TempData[SuccessMessage] = "Lesson was added successfully!";
                return RedirectToAction("AllInCourse", new { courseId });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add course! Please try again later or contact administrator!");

                return View(formModel);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var lessonExist = await lessonService.ExistByIdAsync(id);
            if (!lessonExist)
            {
                TempData[ErrorMessage] = "The given lesson does not exist!";

                return RedirectToAction("TeacherCourses", "Course");
            }

            var lesson = await lessonService.GetDetailsByIdAsync(id);

            return View(lesson);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var lessonExist = await lessonService.ExistByIdAsync(id);
            if (!lessonExist)
            {
                TempData[ErrorMessage] = "The given lesson does not exist!";

                return RedirectToAction("TeacherCourses", "Course");
            }

            string userId = this.User.GetId()!;
            var isUserTeacher = await teacherService.TeacherExistByUserId(userId);
            if (!isUserTeacher)
            {
                TempData[ErrorMessage] = "You must be a teacher in order to edit lesson!";

                return RedirectToAction("Become", "Teacher");
            }

            string teacherId = await teacherService.GetTeacherIdByUserId(this.User.GetId()!);
            var isTeachersLesson = await lessonService.IsTeachersLessonByIdAndCourseIdsync(teacherId, id);
            if (!isTeachersLesson)
            {
                TempData[ErrorMessage] = "You can only edit lessons that are included in your courses!";

                return RedirectToAction("TeacherCourses", "Course");
            }

            var formModel = await lessonService.GetForEditingByIdAsync(id);

            return View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, LessonFormModel formModel)
        {
            var lessonExist = await lessonService.ExistByIdAsync(id);
            if (!lessonExist)
            {
                TempData[ErrorMessage] = "The given lesson does not exist!";

                return RedirectToAction("TeacherCourses", "Course");
            }

            string userId = this.User.GetId()!;
            var isUserTeacher = await teacherService.TeacherExistByUserId(userId);
            if (!isUserTeacher)
            {
                TempData[ErrorMessage] = "You must be a teacher in order to edit lesson!";

                return RedirectToAction("Become", "Teacher");
            }

            string teacherId = await teacherService.GetTeacherIdByUserId(this.User.GetId()!);
            var isTeachersLesson = await lessonService.IsTeachersLessonByIdAndCourseIdsync(teacherId, id);
            if (!isTeachersLesson)
            {
                TempData[ErrorMessage] = "You can only edit lessons that are included in your courses!";

                return RedirectToAction("TeacherCourses", "Course");
            }

            if (!ModelState.IsValid)
            {
                return View(formModel);
            }

            try
            {
                await lessonService.EditByIdAndFormModelAsync(id, formModel);

                TempData[SuccessMessage] = "The lesson was edited successfully";
                return RedirectToAction("AllInCourse", new { courseId = formModel.CourseId });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add course! Please try again later or contact administrator!");
                
                return View(formModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var lessonExist = await lessonService.ExistByIdAsync(id);
            if (!lessonExist)
            {
                TempData[ErrorMessage] = "The given lesson does not exist!";

                return RedirectToAction("TeacherCourses", "Course");
            }

            string userId = this.User.GetId()!;
            var isUserTeacher = await teacherService.TeacherExistByUserId(userId);
            if (!isUserTeacher)
            {
                TempData[ErrorMessage] = "You must be a teacher in order to delete lesson!";

                return RedirectToAction("Become", "Teacher");
            }

            string teacherId = await teacherService.GetTeacherIdByUserId(this.User.GetId()!);
            var isTeachersLesson = await lessonService.IsTeachersLessonByIdAndCourseIdsync(teacherId, id);
            if (!isTeachersLesson)
            {
                TempData[ErrorMessage] = "You can only delete lessons that are included in your courses!";

                return RedirectToAction("TeacherCourses", "Course");
            }

            var viewModel = await lessonService.GetForDeleteByIdAsync(id);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, DeleteLessonViewModel viewModel)
        {
            var lessonExist = await lessonService.ExistByIdAsync(id);
            if (!lessonExist)
            {
                TempData[ErrorMessage] = "The given lesson does not exist!";

                return RedirectToAction("TeacherCourses", "Course");
            }

            string userId = this.User.GetId()!;
            var isUserTeacher = await teacherService.TeacherExistByUserId(userId);
            if (!isUserTeacher)
            {
                TempData[ErrorMessage] = "You must be a teacher in order to delete lesson!";

                return RedirectToAction("Become", "Teacher");
            }

            string teacherId = await teacherService.GetTeacherIdByUserId(this.User.GetId()!);
            var isTeachersLesson = await lessonService.IsTeachersLessonByIdAndCourseIdsync(teacherId, id);
            if (!isTeachersLesson)
            {
                TempData[ErrorMessage] = "You can only delete lessons that are included in your courses!";

                return RedirectToAction("TeacherCourses", "Course");
            }

            try
            {
                await lessonService.DeleteByIdAndViewModelAsync(id, viewModel);

                TempData[SuccessMessage] = "The lesson was deleted successfully";
                return RedirectToAction("AllInCourse", new { courseId = viewModel.CourseId });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add course! Please try again later or contact administrator!");

                return View(viewModel);
            }
        }
    }
}
