using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;
using System.Collections.Generic;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Order>> GetOrders()
        {
            try
            {
                var orders = _orderRepository.GetRecentOrders();
                return Ok(orders);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateOrder(CreateOrderRequest request)
        {
            try
            {
                _orderRepository.AddNewOrder(request);
                return CreatedAtAction(nameof(GetOrders), request);
            }
            catch
            {
                return BadRequest("Error occurred while creating the order");
            }
        }
    }
}
