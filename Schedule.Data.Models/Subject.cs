using System.ComponentModel.DataAnnotations.Schema;

namespace Schedule.Data.Models
{
    public class Subject
    {
        public Subject()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        
        public List<Teacher> Teacher { get; set; }
    }
}
