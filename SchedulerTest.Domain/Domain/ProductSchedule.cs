using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Domain.Domain
{
    public  class ProductSchedule
    {
        [Key]
        public int Id { get; set; } 
        public string Code { get; set; }
        public string? Name { get; set; }
        public decimal? Amount { get; set; }
        public bool IsActive { get; set; }
        
    }
}
