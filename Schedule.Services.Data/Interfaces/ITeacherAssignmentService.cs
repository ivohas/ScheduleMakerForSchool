using Schedule.Web.ViewModels.Schedule;
using Schedule.Web.ViewModels.TeacherAssignment;

namespace Schedule.Services.Data.Interfaces
{
    public interface ITeacherAssignmentService
    {
        Task<TeacherAssignmentViewModel> GiveATeacherClassesAsync(ClassesViewModel classes, TeacherViewModel teachers);
    }
}
