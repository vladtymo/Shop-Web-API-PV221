using Ardalis.Specification;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        //IEnumerable<TEntity> Get(
        //    Expression<Func<TEntity, bool>> filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    string includeProperties = "");
        TEntity GetById(object id);
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);

        // get items by specification
        Task<TEntity?> GetItemBySpec(ISpecification<TEntity> specification);
        Task<IEnumerable<TEntity>> GetListBySpec(ISpecification<TEntity> specification);

        void Save();
    }
}
