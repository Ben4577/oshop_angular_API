using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Objects.Models;

namespace oshop_angular_API.Services
{
    public interface IOshopService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(string productId);
        Task<Product> SaveProduct(Product product);
        Task<bool> DeleteProduct(string ProductId);
        Task<List<Category>> GetCategories();
        Task<Order> SaveOrder(Order order);
    }
}
