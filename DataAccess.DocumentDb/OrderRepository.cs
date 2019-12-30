using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.RepositoryFarm;
using Domain.Objects.Models;

namespace DataAccess.DocumentDb
{
    public class OrderRepository : IOrderRepository
    {

        private readonly DocumentDbRepository<Order> _documentDbRepository;

        public OrderRepository(DocumentDbRepository<Order> documentDbRepository)
        {
            _documentDbRepository = documentDbRepository;
        }

        public async Task<Order> SaveOrder(Order order)
        {
            var cart =  await _documentDbRepository.Save(order);
            return cart;
        }

    }
}
