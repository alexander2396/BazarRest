using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bazar.Models.Repository;
using Bazar.Models.Entities;

namespace Bazar.Models.Repository
{
    public class OrderRepository
    {
        private EfDbContext context;

        public OrderRepository(EfDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Order> Orders => context.Orders;


        public void SaveOrder(Order Order)
        {
            context.Orders.Add(Order);
            context.SaveChanges();
        }

        public Order DeleteOrder(int orderId)
        {
            Order dbEntry = context.Orders
                .FirstOrDefault(o => o.OrderId == orderId);

            if (dbEntry == null)
                throw new Exception($"Delete was not complete, because didn't find Order with OrderId = {orderId}");

            context.Orders.Remove(dbEntry);
            context.SaveChanges();

            return dbEntry;
        }
    }
}
