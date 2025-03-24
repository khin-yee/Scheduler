using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Domain.Domain
{
    public  class ScheduleProductResponse
    {
        public int ErrorCode { get; set; } = 0;
        public string ErrorMessage { get; set; } = "success";
        public List<ProductSchedule> Products { get; set; }
    }
}
