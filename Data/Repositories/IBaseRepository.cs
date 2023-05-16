using solutionApp.Data.Entities;

namespace solutionApp.Data.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IList<TEntity>> GetAll();
        Task<TEntity?> Get(int id);

        Task<TEntity> Create(TEntity obj);
        Task<TEntity> Update(TEntity obj);
        Task Delete(int id);
    }
}
