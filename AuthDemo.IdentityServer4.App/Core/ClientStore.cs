using AuthDemo.IdentityServer4.App.Data;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemo.IdentityServer4.App.Core
{
    public class ClientStore : IClientStore
    {
        private readonly AuthDbContext _authDbContext;
        public ClientStore(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }
        public async Task<Client> FindClientByIdAsync(string clientId)
        {

            var dbClient = await _authDbContext.Client.Where(_ => _.ClientId == clientId).FirstOrDefaultAsync();
            if(dbClient == null)
            {
                return new Client();
            }

            var dbClientGrantTypes = await _authDbContext.ClientGrantTypes.Where(_ => _.ClientId == dbClient.Id).ToListAsync();

            var dbclientSecrets = await _authDbContext.ClientSecrets.Where(_ => _.ClientId == dbClient.Id).ToListAsync();

            var dbClientScopes = await _authDbContext.ClientScopes.Where(_ => _.ClientId == dbClient.Id).ToListAsync();

            return new Client
            {
                ClientId = dbClient.ClientId,

                AllowedGrantTypes = (dbClientGrantTypes?.Count ?? 0) > 0 ?
                dbClientGrantTypes.Select(_ => _.GrantType).ToList() : new List<string>(),

                ClientSecrets = (dbclientSecrets?.Count ?? 0) > 0 ?
                dbclientSecrets.Select(_ => new Secret(_.Secrets.Sha256())).ToList() : null,

                AllowedScopes = (dbClientScopes?.Count ?? 0) > 0?
                dbClientScopes.Select(_ => _.Scope).ToList(): new List<string>()
            };
        }
    }
}
