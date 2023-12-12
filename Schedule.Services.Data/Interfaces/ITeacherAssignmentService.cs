using Schedule.Web.ViewModels.Schedule;

namespace Schedule.Services.Data.Interfaces
{
    public interface ITeacherAssignmentService
    {
        Task<string> GiveATeacherClasses(ClassesViewModel classes, TeacherViewModel teachers);
    }
}
