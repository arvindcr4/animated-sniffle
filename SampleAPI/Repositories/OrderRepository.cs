using Microsoft.EntityFrameworkCore;
using SampleAPI.Entities;
using SampleAPI.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SampleApiDbContext _context;  

        public OrderRepository(SampleApiDbContext context) 
        {
            _context = context;
        }

        public void AddNewOrder(CreateOrderRequest request)
        {
            var order = new Order
            {
                Id = request.OrderId,
                ItemName = request.ItemName,
                Quantity = request.Quantity,
                Price = request.Price,
                CustomerEmail = request.CustomerEmail,
                OrderDate = DateTime.Now,
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public IList<Order> GetRecentOrders()
        {
            return _context.Orders.OrderByDescending(o => o.OrderDate)
                              .Take(10)
                              .ToList();
        }
    }
}
