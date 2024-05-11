

//namespace IdentityManager.Main.Extensions
//{
//    public static class SwaggerExtensions
//    {
//        public static IServiceCollection AddSwaggerService(
//            this IServiceCollection services, IConfiguration configuration)
//        {
//            services.AddApiVersioning(o =>
//            {
//                o.ReportApiVersions = true;
//                o.AssumeDefaultVersionWhenUnspecified = true;
//                o.DefaultApiVersion = new ApiVersion(1, 0);
//            });
//            services.AddSwaggerGen(options =>
//            {
//                options.SwaggerDoc("v1", new OpenApiInfo
//                {
//                    Title = "WC plugin v1 for Miningb",
//                    Version = "v1"
//                });

//                options.SwaggerDoc("v2", new OpenApiInfo
//                {
//                    Title = "WC plugin v2 for Miningb",
//                    Version = "v2"
//                });

//                options.OperationFilter<RemoveVersionFromParameter>();
//                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();

//                options.DocInclusionPredicate((version, desc) =>
//                {
//                    if (!desc.TryGetMethodInfo(out MethodInfo methodInfo))
//                        return false;

//                    var versions = methodInfo.DeclaringType
//                    .GetCustomAttributes(true)
//                    .OfType<ApiVersionAttribute>()
//                    .SelectMany(attr => attr.Versions);

//                    var maps = methodInfo
//                    .GetCustomAttributes(true)
//                    .OfType<MapToApiVersionAttribute>()
//                    .SelectMany(attr => attr.Versions)
//                    .ToArray();

//                    return versions.Any(v => $"v{v}" == version)
//                    && (!maps.Any() || maps.Any(v => $"v{v}" == version));
//                });
//            });

//            return services;
//        }

//        public static IApplicationBuilder UseSwaggerService(this IApplicationBuilder app)
//        {
//            app.UseSwagger();
//            app.UseSwaggerUI(c =>
//            {
//                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//                c.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
//               // c.RoutePrefix = string.Empty;
//            });

//            return app;
//        }

//    }
//}
