using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.RepositoryFarm;
using Domain.Objects.Models;

namespace DataAccess.DocumentDb
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {

        private readonly DocumentDbRepository<ShoppingCart> _documentDbRepository;

        public ShoppingCartRepository(DocumentDbRepository<ShoppingCart> documentDbRepository)
        {
            _documentDbRepository = documentDbRepository;
        }

        public async Task<ShoppingCart> CreateShoppingCart(ShoppingCart shoppingCart)
        {
            var cart =  await _documentDbRepository.Save(shoppingCart);
            return cart;
        }

    }
}
