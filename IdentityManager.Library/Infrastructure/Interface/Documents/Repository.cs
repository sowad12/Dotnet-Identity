using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IdentityManager.Library.Models.Interface;


namespace IdentityManager.Library.Infrastructure.Interface.Documents
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected DbContext Context;
        private bool _disposed = false;
        public Repository(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //public Repository() : this(new ClubeezDatabaseContext())
        //{

        //}

        protected DbSet<T> DbSet // Entity corresponding Database Table
        {
            get { return Context.Set<T>(); }
        }


        #region IDisposable Members

        ~Repository()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (Context != null)
                    {
                        Context.Dispose();
                        Context = null;
                    }
                }
            }
            _disposed = true;
        }

        #endregion

        #region LINQ QUERY

        public virtual void Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            DbSet.Add(item);
        }
        public async Task<T> AddAsync(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            try
            {
                var result = await DbSet.AddAsync(item);
                return result.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void AddRange(IEnumerable<T> items)
        {
            DbSet.AddRange(items);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> items)
        {
            await DbSet.AddRangeAsync(items);
        }

        public virtual void Remove(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            Attach(item); //attach item if not exist
            DbSet.Remove(item); //set as "removed"
        }

        public virtual void RemoveRange(IEnumerable<T> items)
        {
            //Attach(items); //attach item if not exist
            DbSet.RemoveRange(items); //set as "removed"
        }

        public virtual void Modify(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var entry = Context.Entry(item);
            DbSet.Attach(item);
            entry.State = EntityState.Modified;
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual T GetUntracked(Expression<Func<T, bool>> predicate)
        {
            return DbSet.AsNoTracking().FirstOrDefault(predicate);
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.FirstOrDefaultAsync(predicate);
        }
        public virtual async Task<T> GetUntrackedAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbSet;
        }

        public virtual IQueryable<T> All()
        {
            return DbSet.AsQueryable();
        }

        public virtual IQueryable<T> AllUntracked()
        {
            return DbSet.AsQueryable().AsNoTracking();
        }

        public T Create(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            DbSet.Add(item);
            var result = SaveChanges();
            //return result > 0 ? item : null;
            return item;
        }

        public int Update(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var entry = Context.Entry(item);
            DbSet.Attach(item);
            entry.State = EntityState.Modified;
            return SaveChanges();
        }

        public int Update(Expression<Func<T, bool>> predicate)
        {
            var records = FindAll(predicate);
            if (!records.Any()) throw new Exception("Object not found.");

            foreach (var record in records)
            {
                var entry = Context.Entry(record);
                DbSet.Attach(record);
                entry.State = EntityState.Modified;
            }
            return SaveChanges();
        }

        public int Delete(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            DbSet.Remove(item);
            return SaveChanges();
        }

        public int Delete(Expression<Func<T, bool>> predicate)
        {
            var records = FindAll(predicate);
            if (!records.Any()) throw new Exception("Object not found.");

            foreach (var record in records)
            {
                DbSet.Remove(record);
            }
            return SaveChanges();
        }

        public int Count
        {
            get { return DbSet.Count(); }
        }

        public long LongCount
        {
            get { return DbSet.LongCount(); }
        }

        public int CountFunc(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Count(predicate);
        }

        public long LongCountFunc(Expression<Func<T, bool>> predicate)
        {
            return DbSet.LongCount(predicate);
        }

        public bool IsExist(Expression<Func<T, bool>> predicate)
        {
            var count = DbSet.Count(predicate);
            return count > 0;
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return DbSet.First(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return DbSet.FirstOrDefault(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Max(Expression<Func<T, string>> predicate)
        {
            return DbSet.Max(predicate);
        }

        public string MaxFunc(Expression<Func<T, string>> predicate, Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where).Max(predicate);
        }

        public string Min(Expression<Func<T, string>> predicate)
        {
            return DbSet.Min(predicate);
        }

        public string MinFunc(Expression<Func<T, string>> predicate, Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where).Min(predicate);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<T> WhereUntracked(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).AsNoTracking();
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable();
        }

        #endregion

        #region DATABSE TRANSACTION

        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            Context.Entry(item).State = EntityState.Unchanged;
        }

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            //this operation also attach item in object state manager
            Context.Entry(item).State = EntityState.Modified;
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            //if it is not attached, attach original and set current values
            Context.Entry(original).CurrentValues.SetValues(current);
        }

        public int SaveChanges()
        {
            try
            {
                ApplyCommonTask();
                return Context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ApplyCommonTask()
        {
            List<EntityEntry> copyChangeList = Context.ChangeTracker.Entries().ToList();
            foreach (var entry in copyChangeList)
            {
                if (entry.State == EntityState.Unchanged)
                {
                    continue;
                }
                IBaseEntity entity = entry.Entity as IBaseEntity;

                if (entity == null)
                {
                    continue;
                }
                if (entry.State == EntityState.Deleted)
                {
                    entity.OnDelete();
                }
                if (entry.State == EntityState.Added)
                {
                    //if (((IEntity)entry.Entity).AutoIdGeneration)
                    //{
                    //    entity.Id = NumberUtilities.GetUniqueNumber();
                    //}
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.UpdatedAt = DateTime.UtcNow;
                    entity.OnCreate();
                }
                if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                    entity.OnUpdate();
                }
            }
        }

        public void SaveAndRefreshChanges()
        {
            bool saveFailed;

            do
            {
                try
                {
                    SaveChanges();
                    saveFailed = false;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList().ForEach(entry =>
                    {
                        entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    });
                }
            } while (saveFailed);
        }

        public void RollbackChanges()
        {
            Context.ChangeTracker.Entries().ToList()
                .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        #endregion

        #region LINQ ASYNC

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<int> UpdateAsync(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            var entry = Context.Entry(item);
            DbSet.Attach(item);
            entry.State = EntityState.Modified;
            return await SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Expression<Func<T, bool>> predicate)
        {
            var records = await FindAllAsync(predicate);
            if (!records.Any())
            {
                throw new Exception("Object not found during update operation.");
            }
            foreach (var record in records)
            {
                var entry = Context.Entry(record);

                DbSet.Attach(record);

                entry.State = EntityState.Modified;
            }
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T t)
        {
            DbSet.Remove(t);
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var records = await DbSet.Where(predicate).ToListAsync();
            if (!records.Any())
            {
                throw new Exception("Object not found during delete operation.");
            }
            foreach (var record in records)
            {
                DbSet.Remove(record);
            }

            return await SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await DbSet.CountAsync();
        }

        public async Task<long> LongCountAsync()
        {
            return await DbSet.LongCountAsync();
        }

        public async Task<int> CountFuncAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.CountAsync(predicate);
        }

        public async Task<long> LongCountFuncAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.LongCountAsync(predicate);
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.FirstAsync(predicate);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<string> MaxAsync(Expression<Func<T, string>> predicate)
        {
            return await DbSet.MaxAsync(predicate);
        }

        public async Task<string> MaxFuncAsync(Expression<Func<T, string>> predicate, Expression<Func<T, bool>> where)
        {
            return await DbSet.Where(where).MaxAsync(predicate);
        }

        public async Task<string> MinAsync(Expression<Func<T, string>> predicate)
        {
            return await DbSet.MinAsync(predicate);
        }

        public async Task<string> MinFuncAsync(Expression<Func<T, string>> predicate, Expression<Func<T, bool>> where)
        {
            return await DbSet.Where(where).MinAsync(predicate);
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate)
        {
            var count = await DbSet.CountAsync(predicate);
            return count > 0;
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                ApplyCommonTask();
                return await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region SQL RAW QUERY

        public List<T> RunGetQuery(string query, Func<DbDataReader, T> map)
        {
            using (var command = Context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                Context.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    var entities = new List<T>();

                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }

                    return entities;
                }
            }
        }



        #endregion
    }
}
