using Library.Controllers;
using Microsoft.AspNetCore.Mvc;
using Schedule.Services.Data.Interfaces;
using Schedule.Web.ViewModels.Schedule;

namespace SchoolShudale.Controllers
{
    public class CreateController : BaseController
    {
        private readonly IScheduleService _scheduleService;
        public CreateController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }
        // May delete in future
        [HttpGet]
        public IActionResult Subject()
        {
            var viewModel = new SubjectViewModel();
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Subject(SubjectViewModel viewModel)
        {
            await this._scheduleService.ClearAllSubjectsInTheDbAsync();
            var info = viewModel;
            await this._scheduleService.RecordSubjectInfoAsync(viewModel);
            return RedirectToAction(nameof(Teachers));
        }
        // Add delete row button for the subjects
        [HttpGet]
        public async Task<IActionResult> Teachers()
        {    
            var viewModel = new TeacherViewModel();
            return View(viewModel);

        }
        [HttpPost]
        public async Task<IActionResult> Teachers(TeacherViewModel viewModel)
        {
            await this._scheduleService.ClearAllSubjectsInTheDbAsync();
            await this._scheduleService.ClearAllTeachersAsync();
            var info = viewModel;
            await this._scheduleService.RecordTeachersInfoAsync(info);
            return RedirectToAction(nameof(Classes));
        }
        [HttpGet]
        public IActionResult Classes()
        {
            var classModel = new ClassesViewModel();
            return View(classModel);
        }
        [HttpPost]
        // Record data in the db
        public async Task<IActionResult> Classes(ClassesViewModel classModel)
        {
            await _scheduleService.ClearAllClassesInTheDb();
            await _scheduleService.RecordClassesInDbAsync(classModel);
            return RedirectToAction("Index", "TeacherAssignment");

        }

        [HttpGet]
        public async Task<IActionResult> TeacherAssignment()
        {

            TeacherViewModel teachers = await this._scheduleService.GetAllTeachersAsync();
            ClassesViewModel classes = await this._scheduleService.GetAllClassesAsync();
            return View();
        }
    }
}
