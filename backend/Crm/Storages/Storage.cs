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

        public Storage(DbContextOptions options) : base(options)
        {
        }
    }
}