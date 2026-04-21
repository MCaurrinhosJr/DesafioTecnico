using GoodHamburger.Core.Interfaces.Repositories;
using GoodHamburger.Core.Interfaces.Validators;
using GoodHamburger.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Core.Validators
{
    public class OrderValidator(IMenuRepository menuRepository) : IOrderValidator
    {
        private readonly IMenuRepository _menuRepository = menuRepository;

        public async Task ValidateAsync(Order order)
        {
            if (order.Items == null || !order.Items.Any())
                throw new ArgumentException("Pedido deve conter pelo menos um item");

            if (order.Items.Count(i => i.Type == OrderItemType.Burger) > 1)
                throw new ArgumentException("Apenas um sanduíche é permitido");

            if (order.Items.Count(i => i.Type == OrderItemType.Side) > 1)
                throw new ArgumentException("Apenas uma batata é permitida");

            if (order.Items.Count(i => i.Type == OrderItemType.Drink) > 1)
                throw new ArgumentException("Apenas um refrigerante é permitido");

            // Busca todos os itens válidos do menu
            var menuItems = await _menuRepository.GetMenuItemsAsync();

            foreach (var item in order.Items)
            {
                var menuItem = menuItems.FirstOrDefault(m => m.Id == item.MenuItemId);

                if (menuItem == null)
                    throw new ArgumentException($"Item '{item.Name}' não existe no menu");

                if (menuItem.Type != item.Type)
                    throw new ArgumentException($"Item '{item.Name}' possui tipo inválido");

                if (menuItem.Price != item.Price)
                    throw new ArgumentException($"Item '{item.Name}' possui preço inválido");
            }
        }
    }
}
