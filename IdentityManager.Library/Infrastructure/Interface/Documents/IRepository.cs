using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManager.Library.Infrastructure.Interface.Documents
{
    public interface IRepository<T> : IDisposable where T : class
    {
        #region LINQ QUERY

        int Count { get; }
        long LongCount { get; }
        void Add(T item);
        Task<T> AddAsync(T item);
        void AddRange(IEnumerable<T> items);
        Task AddRangeAsync(IEnumerable<T> items);
        void Remove(T item);
        void RemoveRange(IEnumerable<T> items);
        void Modify(T item);
        T Get(Expression<Func<T, bool>> predicate);
        T GetUntracked(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetUntrackedAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> All();
        IQueryable<T> AllUntracked();
        IEnumerable<T> GetAll();
        int CountFunc(Expression<Func<T, bool>> predicate);
        long LongCountFunc(Expression<Func<T, bool>> predicate);
        bool IsExist(Expression<Func<T, bool>> predicate);
        T First(Expression<Func<T, bool>> predicate);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        T Find(Expression<Func<T, bool>> predicate);
        string Max(Expression<Func<T, string>> predicate);
        string MaxFunc(Expression<Func<T, string>> predicate, Expression<Func<T, bool>> where);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        IQueryable<T> WhereUntracked(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate);
        T Create(T item);
        int Update(T item);
        int Update(Expression<Func<T, bool>> predicate);
        int Delete(T item);
        int Delete(Expression<Func<T, bool>> predicate);
        string Min(Expression<Func<T, string>> predicate);
        string MinFunc(Expression<Func<T, string>> predicate, Expression<Func<T, bool>> where);

        #endregion


        #region DATABSE TRANSACTION

        void Attach<TEntity>(TEntity item) where TEntity : class;
        void SetModified<TEntity>(TEntity item) where TEntity : class;
        void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class;
        void ApplyCommonTask();
        int SaveChanges();
        void SaveAndRefreshChanges();
        void RollbackChanges();

        #endregion


        #region LINQ ASYNC

        Task<ICollection<T>> GetAllAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
        Task<T> CreateAsync(T entity);
        Task<int> UpdateAsync(T item);
        Task<int> UpdateAsync(Expression<Func<T, bool>> predicate);
        Task<int> DeleteAsync(T t);
        Task<int> DeleteAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
        Task<long> LongCountAsync();
        Task<int> CountFuncAsync(Expression<Func<T, bool>> predicate);
        Task<long> LongCountFuncAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<string> MaxAsync(Expression<Func<T, string>> predicate);
        Task<string> MaxFuncAsync(Expression<Func<T, string>> predicate, Expression<Func<T, bool>> where);
        Task<string> MinAsync(Expression<Func<T, string>> predicate);
        Task<string> MinFuncAsync(Expression<Func<T, string>> predicate, Expression<Func<T, bool>> where);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate);
        Task<int> SaveChangesAsync();
        #endregion


        #region SQL RAW QUERY

        List<T> RunGetQuery(string query, Func<DbDataReader, T> map);

        #endregion
    }

    public interface IRepository
    {

    }
}
