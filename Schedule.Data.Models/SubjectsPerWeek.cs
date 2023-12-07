using System.ComponentModel.DataAnnotations;

namespace Schedule.Data.Models
{
    public class SubjectsPerWeek
    {
        public SubjectsPerWeek()
        {
            this.Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        public string SubjectName { get; set; } = null!;
        public int HourSPerWeek { get; set; }
    }
}
