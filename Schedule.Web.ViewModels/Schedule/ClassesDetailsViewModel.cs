namespace Schedule.Web.ViewModels.Schedule
{
    public class ClassesDetailsViewModel
    {
        public ClassesDetailsViewModel()
        {
            this.SubjectPerWeeks = new List<SubjectPerWeekViewModel>();
        }
        public string Name { get; set; } = null!;
        public List<SubjectPerWeekViewModel> SubjectPerWeeks { get; set; }        
    }
}
