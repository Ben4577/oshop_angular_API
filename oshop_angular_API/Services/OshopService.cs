using DataAccess.RepositoryFactory;
using Microsoft.AspNetCore.Mvc;
using oshop_angular_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace oshop_angular_API.Services
{
    public class OshopService : IOshopService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        //private readonly IEventLogger _eventLogger;

        public OshopService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;

        }

        public async Task<List<Product>> GetProducts()
        {
            var repositoryProducts = await _repositoryFactory.OshopRepository.GetProducts();

            List<Product> serviceProducts = new List<Product>();

            foreach (var repProduct in repositoryProducts)
            {
                Product serviceProduct = new Product
                {
                    Id = repProduct.Id,
                    Title = repProduct.Title,
                    Price = repProduct.Price,
                    Category = repProduct.Category,
                    ImageUrl = repProduct.ImageUrl
                };

                serviceProducts.Add(serviceProduct);
            }

            return serviceProducts;
        }


        public async Task<Product> GetProduct(string productId)
        {
            var product = await _repositoryFactory.OshopRepository.GetProduct(productId);

            if (product != null)
            {
                Product serviceProduct = new Product
                {
                    Id = product.Id,
                    Title = product.Title,
                    Price = product.Price,
                    Category = product.Category,
                    ImageUrl = product.ImageUrl
                };
                return serviceProduct;
            }
            else
            {
                Product serviceProduct = new Product();
                return serviceProduct;
            }

            
        }

        public async Task<Product> SaveProduct(Product product)
        {

            Domain.Objects.Models.Product repoProduct = new Domain.Objects.Models.Product
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Category = product.Category,
                ImageUrl = product.ImageUrl
            };

             var newProduct = await _repositoryFactory.OshopRepository.SaveProduct(repoProduct);

             Product prod = new Product
             {
                 Id = newProduct.Id,
                 Title = newProduct.Title,
                 Price = newProduct.Price,
                 Category = newProduct.Category,
                 ImageUrl = newProduct.ImageUrl
             };

            return prod;
        }


        public async Task<bool> DeleteProduct(string ProductId)
        {
            return await _repositoryFactory.OshopRepository.DeleteProduct(ProductId);
        }





    }
}
