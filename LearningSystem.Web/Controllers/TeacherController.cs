namespace LearningSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Become()
        {
            @TempData["SuccessMessage"] = "bravoooooooo";
            @TempData["ErrorMessage"] = "opaaaaaaaaaaaaaaa";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeTeacherFormModel formModel)
        {
            var isUserTeacher = await this.teacherService.TeacherExistByUserId(this.User.GetId()!);


            if (!ModelState.IsValid)
            {
                return View(formModel);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
