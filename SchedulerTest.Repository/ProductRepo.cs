using Microsoft.EntityFrameworkCore;
using SchedulerTest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Repository
{
    public class ProductRepo:IProductRepo
    {
        protected readonly ApplicationDbContext _context;

        public ProductRepo(ApplicationDbContext context) {
            _context = context;
        }

        public List<Product> GetProducts()
        {
            var products =  _context.Product.Where(x => x.IsActive == true).ToList();
            return products;
        }
    }
}
