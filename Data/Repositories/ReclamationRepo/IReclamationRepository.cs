using solutionApp.Data.Entities;
using solutionApp.Models;

namespace solutionApp.Data.Repositories.ReclamationRepo
{
    public interface IReclamationRepository : IBaseRepository<Reclamation>
    {
        public Task<IList<Reclamation>> getAllWithParams(Filter filter);
        public Task<Reclamation> getRecalamationDeatils(int id);
    }
}
