//using IdentityManager.Library.Contexts;
//using IdentityManager.Library.Extensions;
//using Microsoft.Extensions.Logging;


//namespace IdentityManager.Library.Seeders
//{
//    public class MainSeeder
//    {
//        public int Seed(ApplicationDbContext context, ILogger logger)
//        {
//            try
//            {
//                var seedObjects = new List<ISeeder>()
//                {
//                    //new InfoSeeder(),
//                    //new WebhookSeeder(),
//                    //new ScriptTagSeeder(),
//                    //new WidgetSeeder(),
//                    //new ApiAccessScopeSeeder(),
//                    //new CustomerProfileTabSeeder()
//                };
//                var result = SeederExtensions.IdentitySeedWithTransaction(context, seedObjects);
//                logger?.LogWarning("SEED SUCCESS " + result);
//                return result;
//            }
//            catch (Exception ex)
//            {
//                logger?.LogError("SEED ERROR " + ex.Message);
//                throw;
//            }
//        }
//    }
//}
