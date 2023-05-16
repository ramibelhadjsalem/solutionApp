using solution.Data.Entities;

namespace solutionApp.Data.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<IList<AppUser>> GetAllByRole(string role);


    }
}
