using DataAccess.RepositoryFarm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RepositoryFactory
{
    public interface IRepositoryFactory
    {
        IOshopRepository OshopRepository { get; }
        IOrderRepository OrderRepository { get; }
        Task CreateDatabaseIfNotExists();



    }
}
