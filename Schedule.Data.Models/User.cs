using Microsoft.AspNetCore.Identity;
using Schedule.Data.Models;
using System.ComponentModel.DataAnnotations;
namespace BookFindingAndRatingSystem.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.UserConsultations = new List<UserConsultation>();
        }
        
        public string? ImageUrl { get; set; }
        public ICollection<UserConsultation> UserConsultations { get; set; }
    }
}
