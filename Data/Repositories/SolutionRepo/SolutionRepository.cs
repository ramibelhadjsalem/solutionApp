using Microsoft.EntityFrameworkCore;
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

        public async Task<Solution> getInfo(int id) => await _context.Solutions
                    .Include(x => x.Reclamation)
                    .Where(x => x.Enable)
                    .FirstOrDefaultAsync(x => x.Id == id);
    }
}
