using GoodHamburger.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infra.Context
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options) : base(options) { }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<MenuItem> MenuItems => Set<MenuItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .Property(o => o.Price)
                .HasPrecision(10,2);

            modelBuilder.Entity<Order>()
                .Property(o => o.Discount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(i => i.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<MenuItem>()
                .Property(m => m.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem { Id = 1, Name = "X Burger", Price = 5.00m, Type = OrderItemType.Burger },
                new MenuItem { Id = 2, Name = "X Egg", Price = 4.50m, Type = OrderItemType.Burger },
                new MenuItem { Id = 3, Name = "X Bacon", Price = 7.00m, Type = OrderItemType.Burger },
                new MenuItem { Id = 4, Name = "Batata Frita", Price = 2.00m, Type = OrderItemType.Side },
                new MenuItem { Id = 5, Name = "Refrigerante", Price = 2.50m, Type = OrderItemType.Drink }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
