namespace Schedule.Web.ViewModels.Consultation
{
    public class CreateDetailsConsultationViewModel
    {
        public string TeacherName { get; set; }
        public string SubjectName { get; set; }
        public string Day { get; set; }

        public TimeSpan StartHour { get; set; }
        public string TimeLapse { get; set; }
    }
}
