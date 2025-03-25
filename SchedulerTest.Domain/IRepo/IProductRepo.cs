using SchedulerTest.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Domain.IRepo
{
    public  interface IProductRepo
    {
        Task<List<ProductSchedule>> GetProductSchedule();
        Task<List<Product>> GetProducts();
        bool UpdateAdaptorProduct(List<Product> products);
        Task<List<AdaptorProduct>> GetAdaptorProducts();
        bool Update(AdaptorProduct adaptorproduct);
        bool AddAdaptorProduct(AdaptorProduct adaptorproduct);
        bool AddProduct(Product adaptorproduct);

    }
}
