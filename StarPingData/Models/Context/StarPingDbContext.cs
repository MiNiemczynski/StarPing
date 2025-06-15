using Microsoft.EntityFrameworkCore;
using StarPingData.Models.Cart;

namespace StarPingData.Models.Context
{
    public class StarPingDbContext : DbContext
    {
        public StarPingDbContext(DbContextOptions<StarPingDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<CartItemModel> CartItems { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderDeviceModel> OrderDevices { get; set; }
        public DbSet<DeviceModel> Devices { get; set; }
        public DbSet<SubscriptionModel> Subscriptions { get; set; }
        public DbSet<OfferModel> Offers { get; set; }
        public DbSet<PaymentModel> Payments { get; set; }
        public DbSet<OfferReviewModel> OfferReviews { get; set; }
        public DbSet<DeviceReviewModel> DeviceReviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // USER
            modelBuilder.Entity<UserModel>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Orders)
                .WithOne(s => s.User!)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.OfferReviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.DeviceReviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // ADDRESS
            modelBuilder.Entity<AddressModel>()
                .HasMany(a => a.Orders)
                .WithOne(s => s.Address)
                .HasForeignKey(s => s.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            // ORDER
            modelBuilder.Entity<OrderModel>()
                .HasMany(o => o.Subscriptions)
                .WithOne(s => s.Order!)
                .HasForeignKey(s => s.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderModel>()
                .HasMany(o => o.Payments)
                .WithOne(s => s.Order!)
                .HasForeignKey(s => s.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderModel>()
                .HasMany(o => o.OrderDevices)
                .WithOne(s => s.Order!)
                .HasForeignKey(s => s.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // PAYMENT
            modelBuilder.Entity<PaymentModel>()
                .Property(p => p.Status)
                .HasConversion<string>();

            // PRODUCTS
            modelBuilder.Entity<DeviceModel>()
                .HasMany(o => o.OrderDevices)
                .WithOne(s => s.Device)
                .HasForeignKey(s => s.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeviceModel>()
                .HasMany(o => o.Reviews)
                .WithOne(s => s.Device)
                .HasForeignKey(s => s.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OfferModel>()
                .HasMany(o => o.Subscriptions)
                .WithOne(s => s.Offer)
                .HasForeignKey(s => s.OfferId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OfferModel>()
                .HasMany(o => o.Reviews)
                .WithOne(s => s.Offer)
                .HasForeignKey(s => s.OfferId)
                .OnDelete(DeleteBehavior.Cascade);

            // CART
            modelBuilder.Entity<CartItemModel>()
                .HasOne(c => c.Device)
                .WithMany()
                .HasForeignKey(c => c.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartItemModel>()
                .HasOne(c => c.Subscription)
                .WithMany()
                .HasForeignKey(c => c.SubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<OfferModel>().HasBaseType<BaseProductModel>();
            //modelBuilder.Entity<DeviceModel>().HasBaseType<BaseProductModel>();

            //modelBuilder.Entity<OfferReviewModel>().HasBaseType<BaseReviewModel>();
            //modelBuilder.Entity<DeviceReviewModel>().HasBaseType<BaseReviewModel>();

        }
    }
}
