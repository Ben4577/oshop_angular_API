﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Objects.Models;

namespace DataAccess.RepositoryFarm
{
    public interface IOrderRepository
    { 
        Task<Order> SaveOrder(Order order);
    }
}
