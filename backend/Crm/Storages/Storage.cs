using Crm.Storages.Models;
using Microsoft.EntityFrameworkCore;

namespace Crm.Storages
{
    public class Storage : DbContext
    {
        public DbSet<Store> Store { get; set; }

        public DbSet<Client> Client { get; set; }

        public DbSet<ClientAttribute> ClientAttribute { get; set; }

        public DbSet<ClientAttributeLink> ClientAttributeLink { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ProductStatus> ProductStatus { get; set; }

        public DbSet<ProductAttribute> ProductAttribute { get; set; }

        public DbSet<ProductAttributeLink> ProductAttributeLink { get; set; }

        public DbSet<ProductImageKey> ProductImageKey { get; set; }

        public DbSet<ProductImageKeyLink> ProductImageKeyLink { get; set; }

        public Storage(DbContextOptions options) : base(options)
        {
        }
    }
}