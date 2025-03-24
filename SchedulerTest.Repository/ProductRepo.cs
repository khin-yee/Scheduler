using Microsoft.EntityFrameworkCore;
using SchedulerTest.Domain.Domain;
using SchedulerTest.Domain.IRepo;
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

        public async Task<List<ProductSchedule>> GetProductSchedule()
        {
            var scheduleproducts =await  _context.ProductSchedule.AsNoTracking().Where(x => x.IsActive == true).ToListAsync();
            return scheduleproducts;
        }
        public async Task<List<Product>> GetProducts()
        {           
            var products = await  _context.Product.Where(x => x.IsActive == true).ToListAsync();
            return products;
        }
        public async Task<List<AdaptorProduct>> GetAdaptorProducts()
        {
            var adaptorproducts = await _context.AdaptorProduct.Where(x => x.IsActive == true).ToListAsync();
            return adaptorproducts;
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

        public bool Update(AdaptorProduct adaptorproduct)
        {
            try
            {
                _context.AdaptorProduct.Update(adaptorproduct);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
