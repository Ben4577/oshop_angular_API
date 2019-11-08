using Domain.Objects.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RepositoryFarm
{
    public interface IOshopRepository 
    { 
        Task<List<Product>> GetProducts();
       Task<Product> GetProduct(string productId);
       Task<Product> SaveProduct(Product product);
       Task DeleteProduct(Product product);
    }
}
