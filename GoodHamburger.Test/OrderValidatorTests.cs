using GoodHamburger.Core.Models;
using GoodHamburger.Core.Validators;
using FluentAssertions;
using Xunit;

namespace GoodHamburger.Test
{
    public class OrderValidatorTests
    {
        private readonly OrderValidator _validator = new();

        [Fact]
        public void Should_Throw_When_No_Items()
        {
            var order = new Order();
            var act = () => _validator.Validate(order);
            act.Should().Throw<ArgumentException>().WithMessage("*pelo menos um item*");
        }

        [Fact]
        public void Should_Throw_When_Two_Burgers()
        {
            var order = new Order
            {
                Items =
                [
                    new() { Type = OrderItemType.Burger },
                    new() { Type = OrderItemType.Burger }
                ]
            };
            var act = () => _validator.Validate(order);
            act.Should().Throw<ArgumentException>().WithMessage("*sanduíche*");
        }

        [Fact]
        public void Should_Throw_When_Two_Sides()
        {
            var order = new Order
            {
                Items =
                [
                    new() { Type = OrderItemType.Side },
                    new() { Type = OrderItemType.Side }
                ]
            };
            var act = () => _validator.Validate(order);
            act.Should().Throw<ArgumentException>().WithMessage("*batata*");
        }

        [Fact]
        public void Should_Throw_When_Two_Drinks()
        {
            var order = new Order
            {
                Items =
                [
                    new() { Type = OrderItemType.Drink },
                    new() { Type = OrderItemType.Drink }
                ]
            };
            var act = () => _validator.Validate(order);
            act.Should().Throw<ArgumentException>().WithMessage("*refrigerante*");
        }

        [Fact]
        public void Should_Pass_When_Valid_Full_Combo()
        {
            var order = new Order
            {
                Items =
                [
                    new() { Type = OrderItemType.Burger },
                    new() { Type = OrderItemType.Side },
                    new() { Type = OrderItemType.Drink }
                ]
            };
            var act = () => _validator.Validate(order);
            act.Should().NotThrow();
        }
    }
}