using BookFindingAndRatingSystem.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedule.Data.Models
{
    public class UserConsultation
    {
        public Guid ApplicationUserID { get; set; }
        [ForeignKey(nameof(ApplicationUserID))]
        public ApplicationUser ApplicationUser { get; set; }
        public Guid ConsultationID { get; set; }
        [ForeignKey(nameof(ConsultationID))]
        public Consultation Consultation { get; set; }
    }
}
