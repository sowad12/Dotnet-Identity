using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManager.Library.Models.ViewModels
{
    public abstract class ResponseViewModel
    {
        public dynamic Data { get; set; }
        public StatusViewModel Status { get; set; }
    }

    public class StatusViewModel
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
    }
}
