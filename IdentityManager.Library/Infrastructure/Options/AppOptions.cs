using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManager.Library.Infrastructure.Options
{
    public class AppOptions
    {
        public string BackendUrl { get; set; }
        public string PaymentProcessor { get; set; }
        public string ClubeezIdentityApiUri { get; set; }
        public string ClubeezApiUri { get; set; }
        public string ClubeezRedirectUri { get; set; }
        public string ClubeezGrantType { get; set; }
        public string ClubeezScope { get; set; }
    }
}
