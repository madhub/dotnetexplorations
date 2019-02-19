// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
                        {
                            // https://stackoverflow.com/questions/48694295/identity-server-4-api-unauthorized-to-call-introspection-endpoint
                            // https://stackoverflow.com/questions/46490885/identityserver-enable-reference-tokens
                             new ApiResource("api1")
                            {
                                ApiSecrets =
                                {
                                    new Secret("{2ACAFD66-F33A-4561-9D80-0C73A0D3D318}".Sha256())
                                }
                            },
                            new ApiResource("api2", "My API"),
                            new ApiResource("apiclient", "client")

                        };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
                        {
                            new Client
                            {
                                ClientId = "client",

                                // no interactive user, use the clientid/secret for authentication
                                AllowedGrantTypes = GrantTypes.ClientCredentials,

                                // secret for authentication
                                ClientSecrets =
                                {
                                    new Secret("{387B590E-F851-406C-94C8-D406E10EFB30}".Sha256())
                                },

                                // scopes that client has access to
                                AllowedScopes = { "api1","api2" },
                                //AccessTokenType = AccessTokenType.Reference
                            }
                        };
        }
    }
}