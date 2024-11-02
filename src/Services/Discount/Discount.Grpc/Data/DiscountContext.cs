using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    public DiscountContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, Amount = 55, ProductName = "IPhone X", Description = "IPhone Discount"},
            new Coupon { Id = 2, Amount = 33, ProductName = "Samsung 10", Description = "Samsung Discount"}
            );
    }

}
