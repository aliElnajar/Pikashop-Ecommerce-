using System.Linq.Expressions;

namespace PikaShop.Data.Contracts.Repositories
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class
    {
        // Naming Convention
        // Select... => Get...
        // SelectBy{instance attribute} => GetBy{instance attribute}
        // SelectBy{Other instance attribute} => GetBy{Other instance attribute}
        // ex: SelectCategoryByDeparmentName => GetCategoryByDeparmentName
        // when implementing this interface :Get{EntityName}{target}
        // GetInclude(string relationship)
        // Update... => Update...
        // Delete... => Delete...
        // DeleteRange

        IQueryable<TEntity> GetSet();

        void Create(TEntity entity, string username = "system");

        void CreateRange(IEnumerable<TEntity> entities, string username = "system");

        IQueryable<TEntity> GetAll();

        TEntity? GetById(TKey id);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Update(TEntity entity);

        void UpdateAudit(TEntity entity, string username = "system");

        void UpdateAuditById(TKey id, string username = "system");

        void Delete(TEntity entity);

        void DeleteById(TKey id);
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
