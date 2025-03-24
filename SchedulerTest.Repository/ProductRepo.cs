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
        public bool UpdateAdaptorProduct(List<Product> products)
        {
            try
            {
                var adaptorproducts = _context.AdaptorProduct.Where(x => x.IsActive==true&&x.ProductCode!= null ).ToList();
                foreach (var product in products)
                {
                    var adaptorproduct = adaptorproducts.FirstOrDefault(x => x.ProductCode==product.Code&&x.Code != null);
                   
                    if (adaptorproduct != null && adaptorproduct.Amount != null && product.Amount != null)
                    {
                        if (adaptorproduct.Amount != product.Amount)
                        {
                            product.Amount = adaptorproduct.Amount??0;

                            try
                            {
                                _context.Product.Update(product);
                                _context.SaveChanges();
                            }
                            catch(Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
