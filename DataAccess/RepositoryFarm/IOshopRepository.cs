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
       Task<Product> GetProduct(string productTitle);
       void CreateProduct(Product product);
       void UpdateProduct(Product product);
       void DeleteProduct(int id);

    }
}
