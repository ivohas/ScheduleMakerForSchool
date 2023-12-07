using BookFindingAndRatingSystem.Data.Models;
using Schedule.Data.Models;
using Schedule.Web.ViewModels.ApplicationUsers;
using Schedule.Web.ViewModels.Consultation;

namespace Schedule.Services.Data.Interfaces
{
    public interface IConsultaionService
    {
        Task AddConsultataionToTheUser(Consultation consultation, string? v);
        Task ClearDatabase();
        Task<List<Consultation>> GetAllConsultations();
        Task<List<Consultation>> GetAllMyConsultationsAsync(string? v);
        Task<ApplicationUserViewModel[]> GetAllParticipantceForAConsultationByIdAsync(Guid id);
        Task<Consultation> GetConsultationByIdAsync(Guid id);
        Task RecordInfoInTheDb(CreateConsultationViewModel info);
        Task RemoveConsultationFromUser(string? v, Consultation consultation);
        Task<Dictionary<string, List<Consultation>>> ScheduleTheConsultationByDays(List<Consultation> consultations);
    }
}
