using Schedule.Web.ViewModels.Schedule;

namespace Schedule.Services.Data.Interfaces
{
    public interface IScheduleService
    {
        Task ClearAllClassesInTheDb();
        Task ClearAllSubjectsInTheDbAsync();
        Task ClearAllTeachersAsync();
        Task<ClassesViewModel> GetAllClassesAsync();
        Task<ICollection<string>> GetAllSubjectsAsync();
        Task<TeacherViewModel> GetAllTeachersAsync();
        Task RecordClassesInDbAsync(ClassesViewModel classModel);
        Task RecordSubjectInfoAsync(SubjectViewModel viewModel);
        Task RecordTeachersInfoAsync(TeacherViewModel info);
    }
}
