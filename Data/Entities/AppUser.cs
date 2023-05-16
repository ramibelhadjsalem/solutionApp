using Microsoft.AspNetCore.Identity;
using solutionApp.Data.Entities;

namespace solution.Data.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string Img { get; set; } = "/images/617a4f17c8e72.png";
        public ICollection<AppUserRole> UserRoles { get; set; }

        public ICollection<Reclamation> Reclamations { get; set; }

    }
    
    
}
