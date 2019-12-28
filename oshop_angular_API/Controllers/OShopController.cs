using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using oshop_angular_API.Services;
using Microsoft.AspNetCore.Mvc;
using oshop_angular_API.Models;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

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

        // GET api/values
        [AllowAnonymous]
        [Route("getproducts")]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _oshopService.GetProducts();
            return Ok(products);
        }

        // GET api/values/5
        [HttpGet("getproduct/{productId}")]
        public async Task<IActionResult> GetProduct(string productId)
        {
            var product = await _oshopService.GetProduct(productId);
            return Ok(product);
        }

        // POST api/values
        [HttpPost("createproduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var prod = await _oshopService.SaveProduct(product);
           return Ok(prod);
        }

        // PUT api/oshop/updateproduct/5
        [HttpPut("updateproduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            var prod = await _oshopService.SaveProduct(product);
            return Ok(prod);
        }

        [HttpDelete("deleteproduct/{productId}")]
        public async Task<IActionResult> Delete(string productId)
        {
           var result = await _oshopService.DeleteProduct(productId);

           return Ok(result);
        }

        [AllowAnonymous]
        [Route("getcategories")]
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _oshopService.GetCategories();
            return Ok(categories);
        }

        [AllowAnonymous]
        [Route("createshoppingcartid")]
        [HttpGet]
        public async Task<IActionResult> CreateShoppingCartId()
        {
            var shoppingCart = await _oshopService.CreateShoppingCartId();
            return Ok(shoppingCart);
        }


    }
}
