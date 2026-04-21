using GoodHamburger.Core.Models;

namespace GoodHamburger.Core.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(Order order);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task UpdateOrder(int id, Order order);
        Task DeleteOrderAsync(int id);
    }
}
