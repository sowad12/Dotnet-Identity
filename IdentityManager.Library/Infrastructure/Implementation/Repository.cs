using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using IdentityManager.Library.Models.Interface;

namespace IdentityManager.Library.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        //private readonly IAuthenticatedUser _authenticatedUser;

        public Repository(IDataContext dataContext /*IAuthenticatedUser authenticatedUser*/)
        {
            DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
           // _authenticatedUser = authenticatedUser;
        }

        protected IDataContext DataContext { get; }


        public virtual async ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            //entity.CreatedBy = entity.CreatedBy.HasValue ? entity.CreatedBy : _authenticatedUser?.Id;
           // entity.UpdatedBy = entity.UpdatedBy.HasValue ? entity.UpdatedBy : _authenticatedUser?.Id;
            return await DataContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        }

        public virtual EntityEntry<TEntity> Add(TEntity entity)
        {
           // entity.CreatedBy = entity.CreatedBy.HasValue ? entity.CreatedBy : _authenticatedUser?.Id;
           // entity.UpdatedBy = entity.UpdatedBy.HasValue ? entity.UpdatedBy : _authenticatedUser?.Id;
            return DataContext.Set<TEntity>().Add(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            await DataContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            DataContext.Set<TEntity>().AddRange(entities);
        }

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return DataContext.Set<TEntity>().Where(predicate);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return DataContext.Set<TEntity>().Any(predicate);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DataContext.Set<TEntity>().AnyAsync(predicate);
        }

        public virtual TEntity Remove(TEntity entity)
        {
            return DataContext.Set<TEntity>().Remove(entity).Entity;
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            DataContext.Set<TEntity>().RemoveRange(entities);
        }

        public virtual void RemoveRange(params TEntity[] entities)
        {
            DataContext.Set<TEntity>().RemoveRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            //entity.UpdatedBy = _authenticatedUser?.Id;
            DataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
           // entity.UpdatedBy = _authenticatedUser?.Id;
            DataContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public virtual async Task<int> CountAsync()
        {
            return await DataContext.Set<TEntity>()
                .AsNoTracking()
                .CountAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DataContext.Set<TEntity>()
                .AsNoTracking()
                .CountAsync(predicate);
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DataContext.Set<TEntity>().SingleOrDefault(predicate);
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DataContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DataContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await DataContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return DataContext.SaveChanges();
        }

        public virtual void SoftDelete(TEntity entity)
        {
            entity.IsDeleted = true;
            //entity.UpdatedBy = _authenticatedUser?.Id;
            DataContext.Entry(entity).State = EntityState.Modified;
        }

        //public IQueryable<TEntity> Match(ICriteria<TEntity> criteria, bool readOnly = true)
        //{
        //    if (readOnly)
        //    {
        //        criteria.Match(DataContext.Set<TEntity>().AsNoTracking());
        //    }
        //    return criteria.Match(DataContext.Set<TEntity>());
        //}

        //public IQueryable<TViewModel> Match<TViewModel>(IViewModelCriteria<TEntity, TViewModel> criteria, bool readOnly = true)
        //{
        //    if (readOnly)
        //    {
        //        criteria.Match(DataContext.Set<TEntity>().AsNoTracking());
        //    }
        //    return criteria.Match(DataContext.Set<TEntity>());
        //}

        public virtual async Task<bool> ExistAsync(long id)
        {
            return await DataContext.Set<TEntity>()
                .AsNoTracking()
                .CountAsync(x => x.Id == id) > 0;
        }

        public virtual bool Exist(long? id)
        {
            if (id != null)
            {
                return DataContext.Set<TEntity>()
                .AsNoTracking()
                .Count(x => x.Id == id) > 0;
            }
            return false;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DataContext.Set<TEntity>()
                .AsNoTracking()
                .Where(x => !x.IsDeleted);
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return DataContext.Set<TEntity>().AsQueryable();
        }

        public virtual async Task<TEntity> FirstOrDefaultUntrackedAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DataContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }
    }
}
