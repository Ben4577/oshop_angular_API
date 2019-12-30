using DataAccess.RepositoryFactory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Domain.Objects.Models;


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
            var newProduct = await _repositoryFactory.OshopRepository.SaveProduct(product);
            return newProduct;
        }


        public async Task<bool> DeleteProduct(string ProductId)
        {
            return await _repositoryFactory.OshopRepository.DeleteProduct(ProductId);
        }


        
        public async Task<List<Category>> GetCategories()
        {
            var repositoryCategories = await _repositoryFactory.OshopRepository.GetCategories();

            List<Category> serviceCategories = new List<Category>();

            foreach (var cat in repositoryCategories)
            {
                Category category = new Category
                {
                    name = cat
                };
                serviceCategories.Add(category);
            }
            return serviceCategories;
        }


        public async Task<Order> SaveOrder(Order order)
        {
            var newOrder = await _repositoryFactory.OrderRepository.SaveOrder(order);
            return newOrder;
        }

    }
}
