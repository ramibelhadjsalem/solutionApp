using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using solution.Data;
using solution.Data.Entities;

namespace solutionApp.Data.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(ApplicationContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IList<AppUser>> GetAllByRole(string role)
        {
            return await _userManager.GetUsersInRoleAsync(role);
        }
    }
}
