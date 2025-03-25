using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Domain.Domain
{
    public  class AdaptorProduct
    {
        [Key]
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; } 
        public string? UpdatedBy { get; set; } 
        public string? BillerCode { get; set; }
        public string? AdaptorCode { get; set; }
        public string? ProductCode { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public decimal? Amount { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
