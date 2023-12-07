using BookFindingAndRatingSystem.Data.Models;
using Schedule.Data.Models;

namespace Schedule.Web.ViewModels.Schedule
{
    public class ClassesViewModel
    {
        public ClassesViewModel()
        {
            this.Classes = new List<ClassesDetailsViewModel>();
        }
        public List<ClassesDetailsViewModel> Classes { get; set; }
    }
}
