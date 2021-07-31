// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthDemo.IdentityServer4.App
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            { new ApiScope { Name = "Api2.Read" }};

        public static IEnumerable<Client> Clients =>
            new Client[]
            { new Client{ AllowedGrantTypes = new string[]{ GrantType.ClientCredentials } } };

        public static IEnumerable<ApiResource> GetApiResources()
        {
            var apiResource = new ApiResource()
            {
                Scopes = new[] { "Api2.Read" },
                Name = "Api2",

            };
            var result = new List<ApiResource>();
            result.Add(apiResource);
            return result;
        }
    }
}