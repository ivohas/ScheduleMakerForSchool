using Schedule.Data.Models;

namespace Schedule.Web.ViewModels.Schedule
{
    public class SubjectViewModel
    {
        public SubjectViewModel()
        {
            this.Names = new List<string>();
        }
        public List<string> Names { get; set; } = null!;
    }
}
