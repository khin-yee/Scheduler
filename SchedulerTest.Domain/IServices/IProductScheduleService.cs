using SchedulerTest.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Domain.IServices
{
    public  interface IProductScheduleService
    {
        Task<ScheduleProductResponse> GetProducts();

    }
}
