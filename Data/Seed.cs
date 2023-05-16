using Microsoft.AspNetCore.Identity;
using solution.Data.Entities;

namespace solution.Data
{
    public class Seed
    {
        public static async Task SeedDataRoles(RoleManager<AppRole> roleManager)
        {
            var roles = new List<AppRole>
            {
                new AppRole{Name ="User"},
                new AppRole{Name = "Technicien"}
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
