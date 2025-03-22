using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Domain
{
    public  interface IProductRepo
    {
        List<Product> GetProducts();
    }
}
