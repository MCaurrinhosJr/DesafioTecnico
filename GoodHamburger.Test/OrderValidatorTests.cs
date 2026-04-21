using GoodHamburger.Core.Interfaces.Repositories;
using GoodHamburger.Core.Models;
using GoodHamburger.Core.Validators;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GoodHamburger.Test
{
    public class OrderValidatorTests
    {
        private readonly Mock<IMenuRepository> _menuRepoMock = new();
        private readonly OrderValidator _validator;

        public OrderValidatorTests()
        {
            _validator = new OrderValidator(_menuRepoMock.Object);
        }

        private void SetupMenu(params MenuItem[] items)
        {
            _menuRepoMock.Setup(x => x.GetMenuItemsAsync())
                .ReturnsAsync(new List<MenuItem>(items));
        }

        [Fact]
        public async Task Should_Throw_When_No_Items()
        {
            var order = new Order();
            await _validator.Invoking(v => v.ValidateAsync(order))
                            .Should().ThrowAsync<ArgumentException>()
                            .WithMessage("*pelo menos um item*");
        }

        [Fact]
        public async Task Should_Throw_When_Two_Burgers()
        {
            SetupMenu(new MenuItem { Id = 1, Name = "X Burger", Type = OrderItemType.Burger, Price = 10 });

            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new() { MenuItemId = 1, Type = OrderItemType.Burger, Price = 10 },
                    new() { MenuItemId = 1, Type = OrderItemType.Burger, Price = 10 }
                }
            };

            await _validator.Invoking(v => v.ValidateAsync(order))
                            .Should().ThrowAsync<ArgumentException>()
                            .WithMessage("*sanduíche*");
        }

        [Fact]
        public async Task Should_Throw_When_Item_Not_In_Menu()
        {
            SetupMenu(new MenuItem { Id = 1, Name = "X Burger", Type = OrderItemType.Burger, Price = 10 });

            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new() { MenuItemId = 2, Type = OrderItemType.Side, Price = 5 }
                }
            };

            await _validator.Invoking(v => v.ValidateAsync(order))
                            .Should().ThrowAsync<ArgumentException>()
                            .WithMessage("*não existe no menu*");
        }

        [Fact]
        public async Task Should_Throw_When_Type_Mismatch()
        {
            SetupMenu(new MenuItem { Id = 1, Name = "X Burger", Type = OrderItemType.Burger, Price = 10 });

            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new() { MenuItemId = 1, Type = OrderItemType.Side, Price = 10 }
                }
            };

            await _validator.Invoking(v => v.ValidateAsync(order))
                            .Should().ThrowAsync<ArgumentException>()
                            .WithMessage("*tipo inválido*");
        }

        [Fact]
        public async Task Should_Throw_When_Price_Mismatch()
        {
            SetupMenu(new MenuItem { Id = 1, Name = "X Burger", Type = OrderItemType.Burger, Price = 10 });

            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new() { MenuItemId = 1, Type = OrderItemType.Burger, Price = 12 }
                }
            };

            await _validator.Invoking(v => v.ValidateAsync(order))
                            .Should().ThrowAsync<ArgumentException>()
                            .WithMessage("*preço inválido*");
        }

        [Fact]
        public async Task Should_Pass_When_Valid_Order()
        {
            SetupMenu(
                new MenuItem { Id = 1, Name = "X Burger", Type = OrderItemType.Burger, Price = 10 },
                new MenuItem { Id = 2, Name = "Batata Frita", Type = OrderItemType.Side, Price = 5 },
                new MenuItem { Id = 3, Name = "Refrigerante", Type = OrderItemType.Drink, Price = 5 }
            );

            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new() { MenuItemId = 1, Type = OrderItemType.Burger, Price = 10 },
                    new() { MenuItemId = 2, Type = OrderItemType.Side, Price = 5 },
                    new() { MenuItemId = 3, Type = OrderItemType.Drink, Price = 5 }
                }
            };

            await _validator.Invoking(v => v.ValidateAsync(order))
                            .Should().NotThrowAsync();
        }
    }
}