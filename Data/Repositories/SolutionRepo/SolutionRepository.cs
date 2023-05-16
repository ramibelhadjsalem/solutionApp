using solution.Data;
using solutionApp.Data.Entities;

namespace solutionApp.Data.Repositories.SolutionRepo
{
    public class SolutionRepository : BaseRespository<Solution>, ISolutionRepository
    {
        private readonly ApplicationContext _context;

        public SolutionRepository(ApplicationContext dbContextFactory) : base(dbContextFactory)
        {
            _context = dbContextFactory;
        }
    }
}
