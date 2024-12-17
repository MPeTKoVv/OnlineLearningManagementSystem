namespace LearningSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using static Common.NotificationMessagesConstants;
    using LearningSystem.Web.ViewModels.Teacher;
    using LearningSystem.Web.Infrastructure.Extensions;
    using LearningSystem.Services.Data.Interfaces;

    public class TeacherController : Controller
    {
        private readonly ITeacherService teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            this.teacherService = teacherService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string userId = this.User.GetId()!;

            var isUserTeacher = await teacherService.TeacherExistByUserId(userId);
            if (isUserTeacher)
            {
                TempData[ErrorMessage] = "You are already a teacher!";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeTeacherFormModel formModel)
        {
            string userId = this.User.GetId()!;

            var isUserTeacher = await teacherService.TeacherExistByUserId(userId);
            if (isUserTeacher)
            {
                TempData[ErrorMessage] = "You are already a teacher!";
                return RedirectToAction("Index", "Home");
            }

            var isPhoneNumberTaken = await teacherService.TeacherExistByPhoneNumber(formModel.PhoneNumber);
            if (isPhoneNumberTaken)
            {
                ModelState.AddModelError(nameof(formModel.PhoneNumber), "Teacher with the provided phone number already exists!");
            }

            if (!ModelState.IsValid)
            {
                return View(formModel);
            }

            try
            {
                await teacherService.Create(userId, formModel);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unexpected error occurred while registering you as a teacher! Please try again later or contact administrator.";

                return RedirectToAction("Index", "Home");
            }

            TempData[SuccessMessage] = "You successfully become a Teacher!";

            return RedirectToAction("Index", "Home");
        }
    }
}