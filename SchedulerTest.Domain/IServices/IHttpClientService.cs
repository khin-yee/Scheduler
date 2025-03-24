using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Domain.IServices
{
    public interface IHttpClientService
    {
        Task<T?> SendAsync<T>(string endpoint, string timeout,  HttpMethod method);

    }
}
