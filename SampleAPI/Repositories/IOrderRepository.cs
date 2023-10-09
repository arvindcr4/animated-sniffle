using SampleAPI.Entities;
using SampleAPI.Requests;

namespace SampleAPI.Repositories
{
    public interface IOrderRepository
    {
        IList<Order> GetRecentOrders();
        void AddNewOrder(CreateOrderRequest request);

    }
}
