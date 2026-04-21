using GoodHamburger.Core.Models;

namespace GoodHamburger.Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<Order> CreateOrder(Order order);
        Task<Order> UpdateOrder(int id, Order order);
        Task DeleteOrderAsync(int id);
    }
}
