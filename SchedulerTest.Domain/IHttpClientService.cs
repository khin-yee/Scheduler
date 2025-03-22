using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Domain
{
    public interface IHttpClientService
    {
        Task<T?> SendAsync<T>(string endpoint, string timeout, FormUrlEncodedContent content, HttpMethod method);

    }
}
