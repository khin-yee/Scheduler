using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchedulerTest.Domain.Domain;
using SchedulerTest.Domain.IServices;
using SchedulerTest.Service;
using System.Threading.Tasks;

namespace SchedulerTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productservice;

        public ProductController(IProductService productservice)
        {
            _productservice = productservice;
        }
        [HttpGet]
        public  async Task<List<Product>> GetProducts()
        {
            return await  _productservice.GetProducts();
        }


        [HttpPost]
        [Route("FileCreate")]
        public async Task<IActionResult> FileCreate()
        {
            return Ok(_productservice.CreateAsync("Test", "ProductList"));
        }

        [HttpPost]
        [Route("FileUpload")]
        public async Task<IActionResult> FileUpload()
        {
            return Ok( await _productservice.CreateFileWithTxnData("ProductList"));
        }
    }
}
