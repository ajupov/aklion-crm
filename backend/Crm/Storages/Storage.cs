using Crm.Storages.Models;
using Microsoft.EntityFrameworkCore;

namespace Crm.Storages
{
    public class Storage : DbContext
    {
        public DbSet<Client> Client { get; set; }

        public DbSet<ClientAttribute> ClientAttribute { get; set; }

        public DbSet<ClientAttributeLink> ClientAttributeLink { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<OrderAttribute> OrderAttribute { get; set; }

        public DbSet<OrderAttributeLink> OrderAttributeLink { get; set; }

        public DbSet<OrderItem> OrderItem { get; set; }

        public DbSet<OrderSource> OrderSource { get; set; }

        public DbSet<OrderStatus> OrderStatus { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ProductAttribute> ProductAttribute { get; set; }

        public DbSet<ProductAttributeLink> ProductAttributeLink { get; set; }

        public DbSet<ProductImageKey> ProductImageKey { get; set; }

        public DbSet<ProductImageKeyLink> ProductImageKeyLink { get; set; }

        public DbSet<ProductStatus> ProductStatus { get; set; }

        public DbSet<Store> Store { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserAttribute> UserAttribute { get; set; }

        public DbSet<UserAttributeLink> UserAttributeLink { get; set; }

        public DbSet<UserPermission> UserPermission { get; set; }

        public DbSet<UserToken> UserToken { get; set; }

        public Storage(DbContextOptions options) : base(options)
        {
        }
    }
}