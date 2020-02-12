using System.Threading.Tasks;
using Domain.Objects.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using oshop_angular_API.Services;
using Microsoft.AspNetCore.Mvc;


namespace oshop_angular_API.Controllers
{

    [Route("api/oshop")]
    [ApiController]
    public class OShopController : ControllerBase
    {

        private readonly IOshopService _oshopService;

        public OShopController(IOshopService oshopService)
        {
            _oshopService = oshopService;
        }

        [Route("getProducts")]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _oshopService.GetProducts();
            return Ok(products);
        }

        [HttpGet("getProduct/{productId}")]
        public async Task<IActionResult> GetProduct(string productId)
        {
            var product = await _oshopService.GetProduct(productId);
            return Ok(product);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("createProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var prod = await _oshopService.SaveProduct(product);
           return Ok(prod);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("updateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            var prod = await _oshopService.SaveProduct(product);
            return Ok(prod);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("deleteProduct/{productId}")]
        public async Task<IActionResult> Delete(string productId)
        {
           var result = await _oshopService.DeleteProduct(productId);

           return Ok(result);
        }


        [Route("getCategories")]
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _oshopService.GetCategories();
            return Ok(categories);
        }

        [HttpPost("placeOrder")]
        public async Task<IActionResult> PlaceOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var prod = await _oshopService.SaveOrder(order);
            return Ok(prod);
        }






    }
}
