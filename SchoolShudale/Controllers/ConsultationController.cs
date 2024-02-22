using BookFindingAndRatingSystem.Data.Models;
using Library.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Schedule.Data.Models;
using Schedule.Services.Data.Interfaces;
using Schedule.Web.ViewModels.ApplicationUsers;
using Schedule.Web.ViewModels.Consultation;

namespace SchoolShudale.Controllers
{
    public class ConsultationController : BaseController
    {
        private readonly IConsultaionService _consultaionService;
        public ConsultationController(IConsultaionService consultaionService)
        {
            this._consultaionService = consultaionService;
        }
        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult Create()
        {
            CreateConsultationViewModel consultation = new CreateConsultationViewModel();
            return View(consultation);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateConsultationViewModel viewModel)
        {

            await this._consultaionService.ClearDatabase();
            var info = viewModel;
            await this._consultaionService.RecordInfoInTheDb(info);
            return RedirectToAction(nameof(Schedule));
        }
        [HttpGet]
        public async Task<IActionResult> Schedule()
        {
            // Change the view model
            List<Consultation> consultations = await this._consultaionService.GetAllConsultations();
            Dictionary<string, List<Consultation>> consultationSchedule = await this._consultaionService.ScheduleTheConsultationByDays(consultations);
            return View(consultationSchedule);
        }
        [HttpGet]
        public async Task<IActionResult> Participate(Guid Id)
        {
            // Get the book with a method to the 
            // Create Mine books

            Consultation consultation = await this._consultaionService.GetConsultationByIdAsync(Id);
            // Importanted Dublicate check 
            await this._consultaionService.AddConsultataionToTheUser(consultation, this.GetUserId());
            return RedirectToAction(nameof(Mine));

        }
        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<Consultation> myConsultations = await this._consultaionService.GetAllMyConsultationsAsync(this.GetUserId());
            return View(myConsultations);
        }
        [HttpGet]
        public async Task<IActionResult> Leave(Guid Id)
        {
            var consultation = await this._consultaionService.GetConsultationByIdAsync(Id);

            await this._consultaionService.RemoveConsultationFromUser(this.GetUserId(), consultation);
            return RedirectToAction(nameof(Mine));
        }
        // Only for the teachers to see the particicpants
        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Details(Guid Id) 
        {
            ApplicationUserViewModel[] usersInConsultation = await this._consultaionService.GetAllParticipantceForAConsultationByIdAsync(Id);
            return View(usersInConsultation);
        }
    }
}
