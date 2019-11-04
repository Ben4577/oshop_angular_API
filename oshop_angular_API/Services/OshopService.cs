using DataAccess.RepositoryFactory;
using Microsoft.AspNetCore.Mvc;
using oshop_angular_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    Title = repProduct.Title,
                    Price = repProduct.Price,
                    Category = repProduct.Category,
                    ImageURL = repProduct.ImageURL
                };

                serviceProducts.Add(serviceProduct);
            }

            return serviceProducts;
        }

        public async Task<Product> GetProduct(string productTitle)
        {
            var product = await _repositoryFactory.OshopRepository.GetProduct(productTitle);

            Product serviceProduct = new Product
            {
                Title = product.Title,
                Price = product.Price,
                Category = product.Category,
                ImageURL = product.ImageURL
            };

            return serviceProduct;
        }

        public void CreateProduct(Product product)
        {

            Domain.Objects.Models.Product repoProduct = new Domain.Objects.Models.Product
            {
                Title = product.Title,
                Price = product.Price,
                Category = product.Category,
                ImageURL = product.ImageURL
            };

            _repositoryFactory.OshopRepository.CreateProduct(repoProduct);
        }

        public void UpdateProduct(Product product)
        {
            Domain.Objects.Models.Product repoProduct = new Domain.Objects.Models.Product
            {
                Title = product.Title,
                Price = product.Price,
                Category = product.Category,
                ImageURL = product.ImageURL
            };
            _repositoryFactory.OshopRepository.UpdateProduct(repoProduct);
        }

        public void DeleteProduct(int id)
        {
            _repositoryFactory.OshopRepository.DeleteProduct(id);
        }





    }
}
