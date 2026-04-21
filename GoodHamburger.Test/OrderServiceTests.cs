using GoodHamburger.Core.Interfaces.Repositories;
using GoodHamburger.Core.Interfaces.Services;
using GoodHamburger.Core.Interfaces.Validators;
using GoodHamburger.Core.Models;
using GoodHamburger.Core.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace GoodHamburger.Test
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _repoMock = new();
        private readonly Mock<IOrderValidator> _validatorMock = new();
        private readonly Mock<IDiscountService> _discountMock = new();

        private OrderService CreateService()
            => new(_repoMock.Object, _validatorMock.Object, _discountMock.Object);

        [Fact]
        public async Task Should_Create_Order_With_Correct_Total()
        {
            var order = new Order
            {
                Items = [new() { Price = 5.00m }, new() { Price = 2.00m }]
            };

            _discountMock.Setup(x => x.CalculateDiscount(order, 15)).Returns(0.7m);
            _repoMock.Setup(x => x.CreateOrder(It.IsAny<Order>())).ReturnsAsync(order);

            var service = CreateService();
            var result = await service.CreateOrder(order);

            result.Price.Should().Be(7.00m);
            result.Discount.Should().Be(0.7m);
            result.TotalPrice.Should().Be(6.3m);
        }

        [Fact]
        public async Task Should_Call_Validator_When_Creating_Order()
        {
            var order = new Order { Items = [new() { Price = 10 }] };
            var service = CreateService();
            await service.CreateOrder(order);
            _validatorMock.Verify(x => x.ValidateAsync(order), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_When_Order_Not_Found_On_Update()
        {
            _repoMock.Setup(x => x.UpdateOrder(It.IsAny<int>(), It.IsAny<Order>()))
                     .ThrowsAsync(new KeyNotFoundException());

            var service = CreateService();
            var act = async () => await service.UpdateOrder(1, new Order());
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task Should_Update_Order_Correctly()
        {
            var order = new Order { Items = [new() { Price = 10 }] };
            _repoMock.Setup(x => x.UpdateOrder(1, order)).Returns(Task.CompletedTask);
            _repoMock.Setup(x => x.GetOrderByIdAsync(1)).ReturnsAsync(order);
            _discountMock.Setup(x => x.CalculateDiscount(order, 10)).Returns(1);

            var service = CreateService();
            var result = await service.UpdateOrder(1, order);

            result.Should().Be(order);
            result.TotalPrice.Should().Be(9); // 10 - 1
        }

        [Fact]
        public async Task Should_Delete_Order()
        {
            _repoMock.Setup(x => x.DeleteOrderAsync(1)).Returns(Task.CompletedTask);
            var service = CreateService();
            await service.DeleteOrderAsync(1);
            _repoMock.Verify(x => x.DeleteOrderAsync(1), Times.Once);
        }

        [Fact]
        public async Task Should_Get_All_Orders()
        {
            var orders = new List<Order> { new() { Id = 1 }, new() { Id = 2 } };
            _repoMock.Setup(x => x.GetAllOrdersAsync()).ReturnsAsync(orders);

            var service = CreateService();
            var result = await service.GetAllOrdersAsync();

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task Should_Get_Order_By_Id()
        {
            var order = new Order { Id = 1 };
            _repoMock.Setup(x => x.GetOrderByIdAsync(1)).ReturnsAsync(order);

            var service = CreateService();
            var result = await service.GetOrderByIdAsync(1);

            result.Should().Be(order);
        }
    }
}