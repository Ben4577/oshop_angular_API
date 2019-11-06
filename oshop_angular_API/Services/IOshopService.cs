using Microsoft.AspNetCore.Mvc;
using oshop_angular_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oshop_angular_API.Services
{
    public interface IOshopService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(string productTitle);
        void SaveProduct(Product product);
        Task DeleteProduct(Product product);

    }
}
