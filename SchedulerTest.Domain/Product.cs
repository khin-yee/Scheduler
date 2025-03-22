using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Domain
{
    public class Product 
    {
        public int Id { get; set; } = default!;
        public string BillerCode { get; set; }
        public string? ProductCategoryCode { get; set; }
        public string Code { get; set; }
        public string? NameEng { get; set; }
        public string? NameMmr { get; set; }
        public string? DescriptionEng { get; set; }
        public string? DescriptionMmr { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; } = default!;
        public string? UpdatedBy { get; set; } = default!;
    }
}
