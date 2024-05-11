

//using IdentityManager.Library.Infrastructure;
//using IdentityManager.Library.Infrastructure.Options;
//using Microsoft.Extensions.Options;

//namespace IdentityManager.Main.Extensions
//{
//    public class RequestAuthenticationMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly ILogger<RequestAuthenticationMiddleware> _logger;
//        //private readonly IServiceCollection _serviceProvider;

//        //private readonly IClientConfigManager _clientConfigManager;

//        //private readonly IDapperContext _dapper;
//        private readonly DatabaseOptions _dbOptions;
//        private readonly AppOptions _appOptions;

//        public RequestAuthenticationMiddleware(
//            RequestDelegate next,
//            ILogger<RequestAuthenticationMiddleware> logger,
//            //IServiceCollection serviceProvider,
//            //IClientConfigManager clientConfigManager,
//            //IDapperContext dapper,
//            IOptions<DatabaseOptions> dbOptions,
//            IOptions<AppOptions> appOptions)
//        {
//            _next = next;
//            _logger = logger;
//            //_serviceProvider = serviceProvider;
//            //_clientConfigManager = clientConfigManager;
//            //_dapper = dapper;
//            _dbOptions = dbOptions.Value;
//            _appOptions = appOptions.Value;
//        }



//        public async Task InvokeAsync(HttpContext context, IClientConfigManager _clientConfigManager)
//        {
//            if (String.IsNullOrEmpty(PluginConstants.AuthorizedSites))
//                await _clientConfigManager.GetAllSite();
//            //using (ServiceProvider serviceProvider = _serviceProvider.BuildServiceProvider())
//            //{
//            //    var loginService = serviceProvider.GetRequiredService<IClientConfigManager>();
//            //    _clientConfigManager.GetAllSite();
//            //}

//            string authHeader = context.Request.Headers["accessToken"];
//            var url = context.Request.Headers["X-Forwarded-For"];
//            var remoteHost = context.Request.Headers["Referer"].ToString().Replace("http://", "").Replace("https://", "").Split("/")[0];
//            var basePath = context.Request.Path.ToString();
//            var sites = PluginConstants.AuthorizedSites.Split(",");
//            bool root = false;
//            foreach (var site in sites)
//            {
//                if (!String.IsNullOrEmpty(remoteHost.ToString()) && site.ToLower().Contains(remoteHost.ToLower()))
//                {
//                    root = true;
//                    break;
//                }
//            }

//            await _next.Invoke(context);

//            ////if (String.IsNullOrEmpty(remoteHost.ToString()) || remoteHost.ToString().Contains(_appOptions.BackendUrl) || basePath.ToLower().Contains("install") || sites.Where(x => x.Contains(remoteHost)).Any())
//            //if(basePath.ToLower().Contains("install") || basePath.ToLower().Contains("system") || basePath.ToLower().Contains("webhook"))
//            //{
//            //    await _next.Invoke(context);
//            //}
//            //else if (!String.IsNullOrEmpty(remoteHost.ToString()) && (basePath.ToLower().Contains("startedpage") || basePath.ToLower().Contains("overviewlandingpage") || basePath == "/") && root)
//            //{
//            //    await _next.Invoke(context);
//            //}
//            //else if (!String.IsNullOrEmpty(remoteHost.ToString()) && _appOptions.BackendUrl.Contains(remoteHost)) //server
//            //{
//            //    await _next.Invoke(context);
//            //}
//            //else
//            //{
//            //    _logger.LogError($"HttpContext remoteHost ==> {remoteHost}");
//            //    _logger.LogError($"HttpContext basePath ==> {basePath}");

//            //    foreach (var site in context.Request.Headers)
//            //    {
//            //        _logger.LogError($"HttpContext site ==> {site}");
//            //    }
//            //    context.Response.StatusCode = 401; //Unauthorized
//            //    return;
//            //}
//        }


//    }
//}
