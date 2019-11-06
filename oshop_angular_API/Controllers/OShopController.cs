﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using oshop_angular_API.Services;
using Microsoft.AspNetCore.Mvc;
using oshop_angular_API.Models;
using Microsoft.AspNetCore.Cors;

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
        [HttpGet("getproduct/{productTitle}")]
        public async Task<IActionResult> GetProduct(string productTitle)
        {
            var product = await _oshopService.GetProduct(productTitle);
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


        // PUT api/values/5
        [HttpPut("updateproduct/{product}")]
        public void UpdateProduct([FromBody] Product product)
        {
            
            _oshopService.SaveProduct(product);
        }

        // DELETE api/values/5
        [HttpDelete("deleteproduct/{product}")]
        public async Task<IActionResult> Delete(Product product)
        {
           var result = _oshopService.DeleteProduct(product);
           return Ok(result);
        }
    }
}
