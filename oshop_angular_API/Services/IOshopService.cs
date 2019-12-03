using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Objects.Models;
using Product = oshop_angular_API.Models.Product;

namespace oshop_angular_API.Services
{
    public interface IOshopService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(string productId);
        Task<Product> SaveProduct(Product product);
        Task<bool> DeleteProduct(string ProductId);
        Task<List<Category>> GetCategories();

    }
}
