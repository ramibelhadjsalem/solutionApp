using Microsoft.EntityFrameworkCore;
using solution.Data;
using solutionApp.Data.Entities;
using solutionApp.Models;

namespace solutionApp.Data.Repositories.ReclamationRepo
{
    public class ReclamationRepository  : BaseRespository<Reclamation>, IReclamationRepository
    {
        public ApplicationContext _context { get; }
        public ReclamationRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<Reclamation>> getAllWithParams(Filter filter)
        {
         /*   var query = _context.Reclamations.AsQueryable();*/
            var query = _context.Reclamations
                .Where(x=>x.Enable)
                .OrderByDescending(x=>x.CreatedAt)
                .Include(x=>x.User)
                .AsQueryable();
            query = query.OrderByDescending(x => x.CreatedAt);
            query = query.Include(x=>x.User);

            if(filter.UserId != null) query = query.Where(x => x.UserId == filter.UserId);

            if (!string.IsNullOrEmpty(filter.status))
            {
                var status = Enum.Parse<ReclamationStatus>(filter.status);
                query = query.Where(x => x.Status == status);
            }


            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<Reclamation?> getRecalamationDeatils(int id)
        {
           return await _context.Reclamations
                   .Where(x => x.Enable)
                   .Include(x=> x.User)
                   .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}
