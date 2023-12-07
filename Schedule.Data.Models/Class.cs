using BookFindingAndRatingSystem.Data.Models;

namespace Schedule.Data.Models
{
    public class Class
    {
        public Class()
        {
            this.Students = new List<ApplicationUser>();
            this.SubjectsPerWeeks = new List<SubjectsPerWeek>();
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<SubjectsPerWeek> SubjectsPerWeeks { get; set; }
        public ICollection<ApplicationUser> Students { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
}
