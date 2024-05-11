using Microsoft.EntityFrameworkCore;

namespace IdentityManager.Library.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        //private readonly IRepository<UnitOfWork> _repository;

        public UnitOfWork(DbContext context/*, IRepository<UnitOfWork> repository*/)
        {
            _context = context;
           // _repository = repository;
        }
        public async Task<int> SaveChangesAsync()
        {
            try
            {
              //  _repository.ApplyCommonTask();
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = ex.InnerException == null ? ex.Message :
                              ("(1). " + ex.Message + (" (2). " + ex.InnerException.Message)) +
                              (ex.InnerException?.InnerException == null ? "" : " (3). " + ex.InnerException.InnerException.Message);
                throw new Exception(message);
            }
        }

        public int SaveChanges()
        {
            try
            {
              //  _repository.ApplyCommonTask();
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                var message = ex.InnerException == null ? ex.Message :
                    ("(1). " + ex.Message + (" (2). " + ex.InnerException.Message)) +
                    (ex.InnerException?.InnerException == null ? "" : " (3). " + ex.InnerException.InnerException.Message);
                throw new Exception(message);
            }
        }
    }
}
