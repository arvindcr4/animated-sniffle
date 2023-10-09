using FluentAssertions;
using Moq;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace SampleAPI.Tests.Repositories
{
    public class OrderRepositoryTests
    {
        private readonly Mock<DbSet<Order>> _mockSet;
        private readonly Mock<SampleApiDbContext> _mockContext;
        private readonly IOrderRepository _repository;

        public OrderRepositoryTests()
        {
            var orders = new List<Order>
            {
                new Order { Id = 1, OrderDate = DateTime.Now.AddDays(-1), ItemName = "Item1", CustomerEmail = "test1@email.com" },
                new Order { Id = 2, OrderDate = DateTime.Now.AddDays(-2), ItemName = "Item2", CustomerEmail = "test2@email.com" },
                // ... add more sample data
            }.AsQueryable();

            _mockSet = new Mock<DbSet<Order>>();
            _mockSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(orders.Provider);
            _mockSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(orders.Expression);
            _mockSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(orders.ElementType);
            _mockSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(orders.GetEnumerator());

            _mockContext = new Mock<SampleApiDbContext>();
            _mockContext.Setup(ctx => ctx.Orders).Returns(_mockSet.Object);

            _repository = new OrderRepository(_mockContext.Object);
        }

        [Fact]
        public void AddNewOrder_ShouldAddOrder()
        {
            // Arrange
            var request = new CreateOrderRequest
            {
                OrderId = 3,
                ItemName = "TestItem",
                Quantity = 2,
                Price = 99.99M,
                CustomerEmail = "test@email.com"
            };

            // Act
            _repository.AddNewOrder(request);

            // Assert
            _mockSet.Verify(set => set.Add(It.IsAny<Order>()), Times.Once);
            _mockContext.Verify(ctx => ctx.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GetRecentOrders_ShouldReturnRecentOrders()
        {
            // Act
            var orders = _repository.GetRecentOrders();

            // Assert
            orders.Should().NotBeNull();
            orders.Count.Should().Be(2); // Based on our test data setup above
            orders.First().Id.Should().Be(1); // The latest order should be the first in the list
        }
    }
}
