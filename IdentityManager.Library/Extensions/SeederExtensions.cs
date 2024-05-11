

//using IdentityManager.Library.Contexts;
//using IdentityManager.Library.Models.Entities;
//using IdentityManager.Library.Seeders;
//using Microsoft.EntityFrameworkCore;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace IdentityManager.Library.Extensions
//{
//    public static class SeederExtensions
//    {
//        public static void AddOrUpdateRange<T>(this DbContext context, IEnumerable<T> enumerable) where T : BaseEntity
//        {
//            if (enumerable != null && enumerable.Count() > 0)
//            {
//                var repo = context.Set<T>();
//                var allData = enumerable.ToList();

//                allData.ForEach(e =>
//                {
//                    var entity = repo.Where(x => x.Id == e.Id).FirstOrDefault();
//                    if (entity == null)
//                    {
//                        // create new one
//                        context.Add(e);
//                    }
//                    else
//                    {
//                        // update existing one
//                        context.Entry(entity).CurrentValues.SetValues(e);
//                    }
//                });

//            }
//        }

//        public static int IdentitySeedWithTransaction(ApplicationDbContext _context, List<ISeeder> seedObjects)
//        {
//            var result = 0;
//            var strategy = _context.Database.CreateExecutionStrategy();
//            //--https://www.thecodebuzz.com/configured-strategy-sqlserverretryingexecutionstrategy-transaction/

//            strategy.Execute(() =>
//            {
//                using (var tran = _context.Database.BeginTransaction())
//                {
//                    try
//                    {
//                        foreach (var seedObj in seedObjects)
//                        {
//                            seedObj.Seed(_context);

//                            var seedTable = _context.ChangeTracker.Entries()
//                                .Where(x => x.State == EntityState.Modified || x.State == EntityState.Deleted || x.State == EntityState.Added)
//                                .Select(x => x.Entity.GetType()).Distinct()
//                                .SelectMany(System.Attribute.GetCustomAttributes)
//                                .Where(x => x is TableAttribute)
//                                .Select(x => ((TableAttribute)x))
//                                .Select(x => new
//                                {
//                                    TableName = x.Name,
//                                    Schema = string.IsNullOrEmpty(x.Schema) ? "dbo" : x.Schema
//                                })
//                                .Select(x => $"[{x.Schema}].[{x.TableName}]")
//                                .FirstOrDefault();


//                            if (seedTable.HasValue())
//                            {
//                                _context.Database.ExecuteSqlRaw(GetIdentityInsertSQL(seedTable, true));
//                                result += _context.SaveChanges();
//                                _context.Database.ExecuteSqlRaw(GetIdentityInsertSQL(seedTable, false));
//                            }
//                        }

//                        tran.Commit();
//                    }
//                    catch (Exception)
//                    {
//                        tran.Rollback();
//                        throw;
//                    }
//                }
//            });

//            return result;
//        }

//        public static string GetIdentityInsertSQL(string tableName, bool enable)
//        {
//            if (enable) return $"SET IDENTITY_INSERT {tableName} OFF; SET IDENTITY_INSERT {tableName} ON; ";
//            return $"SET IDENTITY_INSERT {tableName} OFF; ";
//        }

//        internal static string IdentitySeedWithTransaction(ApplicationDbContext context, List<ISeeder> seedObjects)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
