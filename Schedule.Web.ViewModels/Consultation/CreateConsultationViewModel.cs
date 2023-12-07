namespace Schedule.Web.ViewModels.Consultation
{
    public class CreateConsultationViewModel
    {
        public CreateConsultationViewModel()
        {
            this.Consultations = new List<CreateDetailsConsultationViewModel>();
        }
        public List<CreateDetailsConsultationViewModel> Consultations { get; set; }
    }
}
