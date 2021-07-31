using AuthDemo.IdentityServer4.App.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemo.IdentityServer4.App.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        public DbSet<Client> Client { get; set; }
        public DbSet<ClientGrantTypes> ClientGrantTypes { get; set; }
        public DbSet<ClientSecrets> ClientSecrets { get; set; }
        public DbSet<ClientScopes> ClientScopes { get; set; }
        public DbSet<ApiResources> ApiResources { get; set; }
        public DbSet<ApiResourceScopes> ApiResourceScopes { get; set; }
    }
}
