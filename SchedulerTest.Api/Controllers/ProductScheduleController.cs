using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchedulerTest.Domain.Domain;
using SchedulerTest.Domain.IServices;
using SchedulerTest.Service.Services;

namespace SchedulerTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductScheduleController : ControllerBase
    {
        private readonly IProductScheduleService _service;

        public ProductScheduleController(IProductScheduleService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ScheduleProductResponse> GetProducts()
        {
            return await _service.GetProducts();
        }
    }
}
