using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Domain
{
    public interface IProductService
    {
        List<Product> GetProducts();
        string CreateAsync(string content, string filename);
        Task<string> CreateFileWithTxnData(string fileName);

    }
}
