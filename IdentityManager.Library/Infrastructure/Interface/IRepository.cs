using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IdentityManager.Library.Models.Interface;

namespace IdentityManager.Library.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        EntityEntry<TEntity> Add(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        void AddRange(IEnumerable<TEntity> entities);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        void RemoveRange(params TEntity[] entities);

        void Update(TEntity entity);

        Task UpdateAsync(TEntity entity);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        Task<bool> ExistAsync(long id);

        bool Exist(long? id);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultUntrackedAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> SaveChangesAsync();

        int SaveChanges();

        //IQueryable<TEntity> Match(ICriteria<TEntity> criteria, bool readOnly = true);

        //IQueryable<TViewModel> Match<TViewModel>(IViewModelCriteria<TEntity, TViewModel> criteria, bool readOnly = true);

        void SoftDelete(TEntity entity);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> AsQueryable();
    }


    public interface IRepository
    {

    }
}
