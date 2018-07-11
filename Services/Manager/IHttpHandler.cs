using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Manager
{
    public interface IHttpHandler
    {
        Task<HttpResponseMessage> GetAsync(Uri fullUri);
    }
}
