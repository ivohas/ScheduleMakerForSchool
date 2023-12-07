using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedule.Data.Models
{
    public class Consultation
    {
        public Consultation()
        {
            this.Id = Guid.NewGuid();
            this.UserConsultations = new List<UserConsultation>();
        }

        [Key]
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; }
        public Guid SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }
        public string Day { get; set; }
        public TimeSpan StartHour { get; set; }
        public string TimeLenght { get; set; }
        public ICollection<UserConsultation> UserConsultations { get; set; }
    }
}
