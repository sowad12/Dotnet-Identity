//using Microsoft.OpenApi.Models;
//using Swashbuckle.AspNetCore.SwaggerGen;

//namespace WC.Addon.ClubeezWF.Main.Filter
//{
//    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
//    {
//        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
//        {
//            var toReplaceWith = new OpenApiPaths();

//            foreach (var (key, value) in swaggerDoc.Paths)
//            {
//                toReplaceWith.Add(key.Replace("v{version}", swaggerDoc.Info.Version, StringComparison.InvariantCulture), value);
//            }

//            swaggerDoc.Paths = toReplaceWith;
//        }
//    }
//}
