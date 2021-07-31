using AuthDemo.IdentityServer4.App.Data;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemo.IdentityServer4.App.Core
{
    public class ResourceStore : IResourceStore
    {
        private readonly AuthDbContext _authDbContext;
        public ResourceStore(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }
        public async Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            var dbApiResource = await _authDbContext.ApiResources.Select(_ => new ApiResource { Name = _.Name, DisplayName = _.DisplayName }).ToListAsync();

            var result = new List<ApiResource>();

            foreach (var name in apiResourceNames)
            {
                if(dbApiResource.Any(_ => _.Name.ToLower() == name.ToLower()))
                {
                    result.Add(dbApiResource.Where(_ => _.Name.ToLower() == name.ToLower()).First());
                }
            }
            return result;
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var apiResourWithScopes = await _authDbContext.ApiResources
                .Join(
                _authDbContext.ApiResourceScopes,
                ar => ar.Id,
                ars => ars.ApiResourceId,
                (ar, ars) => new { ApiName = ar.Name, Scope = ars.Scope }
                ).ToListAsync();

            var groupedAPIResoruceScope = apiResourWithScopes.GroupBy(
                _ => _.ApiName)
                .Select(_ => new ApiResource { Name = _.Key, Scopes = _.Select(_ => _.Scope).ToList() })
                .ToList();

            var result = new List<ApiResource>();

            foreach (var name in scopeNames)
            {
                var matchedResource = groupedAPIResoruceScope.Where(_ => _.Scopes.Any(s => s.ToLower() == name.ToLower())).FirstOrDefault();
                if (matchedResource != null)
                {
                    result.Add(matchedResource);
                }
            }

            return result;
        }

        public async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            var dbScopes = await _authDbContext.ApiResourceScopes.Select(_ => new ApiScope { Name = _.Scope }).ToListAsync();
            var result = new List<ApiScope>();
            foreach (var name in scopeNames)
            {
                var matchedApiScope = dbScopes.Where(_ => _.Name.ToLower() == name.ToLower()).FirstOrDefault();
                if (matchedApiScope != null)
                {
                    result.Add(matchedApiScope);
                }
            }
            return result;
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            return new List<IdentityResource>() { };
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            var apiScopes = await _authDbContext.ApiResourceScopes.Select(_ => new ApiScope { Name = _.Scope }).ToListAsync();
            var apiResources = await _authDbContext.ApiResources.Select(_ => new ApiResource { Name = _.Name, DisplayName = _.DisplayName }).ToListAsync();

            return new Resources
            {
                ApiResources = apiResources,
                ApiScopes = apiScopes
            };
        }
    }
}
