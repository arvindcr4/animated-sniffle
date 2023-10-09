using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SampleAPI.Controllers;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;
using System.Collections.Generic;
using Xunit;

namespace SampleAPI.Tests.Controllers
{
    public class OrdersControllerTests
    {
        private readonly Mock<IOrderRepository> _mockRepository;
        private readonly OrdersController _controller;

        public OrdersControllerTests()
        {
            _mockRepository = new Mock<IOrderRepository>();
            _controller = new OrdersController(_mockRepository.Object);
        }

        [Fact]
        public void GetOrders_ReturnsOk_WhenCalled()
        {
            // Arrange
            var mockOrders = new List<Order>
            {
                new Order { Id = 1, ItemName = "Item1", CustomerEmail = "test1@email.com" },
                new Order { Id = 2, ItemName = "Item2", CustomerEmail = "test2@email.com" }
            };
            _mockRepository.Setup(repo => repo.GetRecentOrders()).Returns(mockOrders);

            // Act
            var result = _controller.GetOrders();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(mockOrders);
        }

        [Fact]
        public void CreateOrder_ReturnsCreated_WhenCalled()
        {
            // Arrange
            var mockRequest = new CreateOrderRequest
            {
                OrderId = 1,
                ItemName = "TestItem",
                Quantity = 1,
                Price = 9.99M,
                CustomerEmail = "test@email.com"
            };
            _mockRepository.Setup(repo => repo.AddNewOrder(mockRequest));

            // Act
            var result = _controller.CreateOrder(mockRequest);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtResult = result as CreatedAtActionResult;
            createdAtResult.Value.Should().BeEquivalentTo(mockRequest);
        }
    }
}
