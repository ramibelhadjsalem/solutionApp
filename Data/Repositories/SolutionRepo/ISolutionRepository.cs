using solutionApp.Data.Entities;

namespace solutionApp.Data.Repositories.SolutionRepo
{
    public interface ISolutionRepository :IBaseRepository<Solution>
    {

        Task<Solution> getInfo(int id);
    }
}
