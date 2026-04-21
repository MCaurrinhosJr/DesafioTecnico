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
                    new() { Type = OrderItemType.Burger, Price = 5.00m },
                    new() { Type = OrderItemType.Side, Price = 2.00m },
                    new() { Type = OrderItemType.Drink, Price = 2.50m }
                ]
            };

            var discount = _service.CalculateDiscount(order, 20);

            discount.Should().Be(1.95m); // 20% de 9.50
        }

        [Fact]
        public void Should_Apply_15_Percent_When_Burger_And_Drink()
        {
            var order = new Order
            {
                Items =
                [
                    new() { Type = OrderItemType.Burger, Price = 5.00m },
                    new() { Type = OrderItemType.Drink, Price = 2.50m }
                ]
            };

            var discount = _service.CalculateDiscount(order, 15);

            discount.Should().Be(1.125m); // 15% de 7.50
        }

        [Fact]
        public void Should_Apply_10_Percent_When_Burger_And_Side()
        {
            var order = new Order
            {
                Items =
                [
                    new() { Type = OrderItemType.Burger, Price = 5.00m },
                    new() { Type = OrderItemType.Side, Price = 2.00m }
                ]
            };

            var discount = _service.CalculateDiscount(order, 10);

            discount.Should().Be(0.7m); // 10% de 7.00
        }

        [Fact]
        public void Should_Return_Zero_When_Only_Burger()
        {
            var order = new Order
            {
                Items = [new() { Type = OrderItemType.Burger, Price = 5.00m }]
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
                    new() { Type = OrderItemType.Side, Price = 2.00m },
                    new() { Type = OrderItemType.Drink, Price = 2.50m }
                ]
            };

            var discount = _service.CalculateDiscount(order, 10);

            discount.Should().Be(0);
        }
    }
}