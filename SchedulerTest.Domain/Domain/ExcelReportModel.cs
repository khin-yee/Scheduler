using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Domain.Domain
{
    public  class ExcelReportModel
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal  OldAmount { get; set; }
        public decimal  NewAmount { get; set; }
        public string UpdateAt { get; set; }
    }
}
