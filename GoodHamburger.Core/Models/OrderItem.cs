namespace GoodHamburger.Core.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public OrderItemType Type { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}