using Schedule.Data.Models;

namespace Schedule.Web.ViewModels.Schedule
{
    public class TeacherViewModel
    {
        public TeacherViewModel()
        {
            this.Teachers = new List<TeacherDetailsViewModel>();
        }

        public List<TeacherDetailsViewModel> Teachers { get; set; }
    }
}
