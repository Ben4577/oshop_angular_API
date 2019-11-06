using DataAccess.RepositoryFarm;
using Domain.Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DocumentDb
{
    public class OshopRepository : IOshopRepository
    {

        private readonly DocumentDbRepository<Product> _documentDbRepository;


        public OshopRepository(DocumentDbRepository<Product> documentDbRepository)
        {
            _documentDbRepository = documentDbRepository;
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = _documentDbRepository.Query().AsEnumerable();
            return await Task.FromResult(products.ToList());
        }


        public async Task<Product> GetProduct(string productTitle)
        {
            Product product = _documentDbRepository.Query().Where(x => x.Title == productTitle).AsEnumerable().FirstOrDefault();
            return await Task.FromResult(product);
        }


        public async Task<Product> SaveProduct(Product product)
        {
            return await _documentDbRepository.Save(product);
        }

        public Task DeleteProduct(Product product)
        {
            return _documentDbRepository.Delete(product);
        }


    }
}
