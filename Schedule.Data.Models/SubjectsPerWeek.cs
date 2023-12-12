using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int HoursPerWeek { get; set; }
        public Guid ClassId { get; set; }
        [ForeignKey(nameof(ClassId))] // Foreign key property
        public Class Class { get; set; }
    }
}
