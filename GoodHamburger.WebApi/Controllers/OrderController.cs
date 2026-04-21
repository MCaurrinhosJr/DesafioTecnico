using GoodHamburger.Core.Interfaces.Services;
using GoodHamburger.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.WebApi.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar os pedidos dos clientes.
    /// Ele permitirá que os clientes façam novos pedidos, visualizem seus pedidos anteriores e atualizem ou cancelem pedidos existentes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        /// <summary>
        /// Lista todos os pedidos feitos pelos clientes.
        /// </summary>
        /// <returns>Lista de pedidos</returns>
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Busca um pedido específico pelo seu ID. 
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <returns>Pedido encontrado ou 404</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
                return NotFound($"Pedido com ID {id} não encontrado");

            return Ok(order);
        }

        /// <summary>
        /// Cria um novo pedido com base nas informações fornecidas.
        /// </summary>
        /// <param name="order">Dados do pedido</param>
        /// <returns>Pedido criado</returns>
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            var createdOrder = await _orderService.CreateOrder(order);

            return CreatedAtAction(
                nameof(GetOrderById),
                new { id = createdOrder.Id },
                createdOrder
            );
        }

        /// <summary>
        /// Atualiza um pedido existente com base no ID fornecido.
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <param name="order">Dados atualizados</param>
        /// <returns>Pedido atualizado ou 404</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(int id, [FromBody] Order order)
        {
            try
            {
                var updated = await _orderService.UpdateOrder(id, order);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Pedido com ID {id} não encontrado");
            }
        }

        /// <summary>
        /// Remove um pedido específico pelo seu ID.
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <returns>204 NoContent ou 404</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                await _orderService.DeleteOrderAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Pedido com ID {id} não encontrado");
            }
        }
    }
}
