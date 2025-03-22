using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchedulerTest.Domain;
using SchedulerTest.Service;

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
        public  List<Product> GetProducts()
        {
            return  _productservice.GetProducts();
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
            return Ok(_productservice.CreateFileWithTxnData("ProductList"));
        }
    }
}
