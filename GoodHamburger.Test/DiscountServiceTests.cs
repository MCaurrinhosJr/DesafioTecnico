using GoodHamburger.Core.Models;
using GoodHamburger.Core.Services;
using FluentAssertions;
using Xunit;

namespace GoodHamburger.Test
{
    public class DiscountServiceTests
    {
        private readonly DiscountService _service = new();

        [Fact]
        public void Should_Apply_20_Percent_When_Full_Combo()
        {
            var order = new Order
            {
                Items =
                [
                    new() { Type = OrderItemType.Burger, Price = 10 },
                    new() { Type = OrderItemType.Side, Price = 5 },
                    new() { Type = OrderItemType.Drink, Price = 5 }
                ]
            };

            var discount = _service.CalculateDiscount(order, 20);

            discount.Should().Be(4); // 20%
        }

        [Fact]
        public void Should_Apply_15_Percent_When_Burger_And_Drink()
        {
            var order = new Order
            {
                Items =
                [
                    new() { Type = OrderItemType.Burger, Price = 10 },
                    new() { Type = OrderItemType.Drink, Price = 5 }
                ]
            };

            var discount = _service.CalculateDiscount(order, 15);

            discount.Should().Be(2.25m); // 15%
        }

        [Fact]
        public void Should_Apply_10_Percent_When_Burger_And_Side()
        {
            var order = new Order
            {
                Items =
                [
                    new() { Type = OrderItemType.Burger, Price = 10 },
                    new() { Type = OrderItemType.Side, Price = 5 }
                ]
            };

            var discount = _service.CalculateDiscount(order, 15);

            discount.Should().Be(1.5m); // 10%
        }

        [Fact]
        public void Should_Return_Zero_When_Only_Burger()
        {
            var order = new Order
            {
                Items = [new() { Type = OrderItemType.Burger, Price = 10 }]
            };

            var discount = _service.CalculateDiscount(order, 10);

            discount.Should().Be(0);
        }

        [Fact]
        public void Should_Return_Zero_When_No_Combo()
        {
            var order = new Order
            {
                Items =
                [
                    new() { Type = OrderItemType.Side, Price = 5 },
                    new() { Type = OrderItemType.Drink, Price = 5 }
                ]
            };

            var discount = _service.CalculateDiscount(order, 10);

            discount.Should().Be(0);
        }
    }
}