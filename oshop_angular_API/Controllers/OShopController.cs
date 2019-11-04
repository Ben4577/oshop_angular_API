using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using oshop_angular_API.Services;
using Microsoft.AspNetCore.Mvc;
using oshop_angular_API.Models;

namespace oshop_angular_API.Controllers
{
    [Route("api/Oshop")]
    [ApiController]
    public class OShopController : ControllerBase
    {

        private readonly IOshopService _oshopService;


        public OShopController(IOshopService oshopService)
        {
            _oshopService = oshopService;
        }


        // GET api/values
        [Route("getfamilies/")]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var families = await _oshopService.GetProducts();

            return Ok(families);
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string productTitle)
        {
            var product = await _oshopService.GetProduct(productTitle);
            return Ok(product);
        }


        // POST api/values
        [HttpPost]
        public void CreateProduct([FromBody] Product product)
        {
            _oshopService.CreateProduct(product);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void UpdateProduct([FromBody] Product product)
        {
            _oshopService.UpdateProduct(product);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _oshopService.DeleteProduct(id);
        }
    }
}
