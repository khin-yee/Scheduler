using SchedulerTest.Domain.Domain;
using SchedulerTest.Domain.IRepo;
using SchedulerTest.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest.Service.Services
{
    public class ProductScheduleService : IProductScheduleService
    {
        private readonly IProductRepo _repo;

        public ProductScheduleService(IProductRepo repo)
        {
            _repo = repo;
        }

        public async Task<ScheduleProductResponse> GetProducts()
        {
            var response = new ScheduleProductResponse();
            var products = await _repo.GetProductSchedule();
            if (products == null)
            {
                response.ErrorCode = 1;
                response.ErrorMessage = "fail";
                return response;
            }
            response.Products = products;
            return response;
        }
    }
}
