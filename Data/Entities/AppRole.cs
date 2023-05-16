using Microsoft.AspNetCore.Identity;

namespace solution.Data.Entities
{
    public class AppRole : IdentityRole<int>
    {

        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
