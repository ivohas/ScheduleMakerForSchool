namespace Schedule.Web.ViewModels
{
    public class AllQueryViewModel
    {
        public AllQueryViewModel()
        {
            this.Subjects = new List<string>();
            this.Teachers = new List<string>();
        }
        public List<string> Subjects { get; set; }
        public List<string> Teachers { get; set; }
    }
}
