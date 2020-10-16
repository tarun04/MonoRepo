using IdentityServer4.Models;
using System.Collections.Generic;

namespace MonoRepo.Microservice.IdentityServer.B2C.Migration
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[]
            {
                new ApiResource("application", "Application")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = "application-gateway",
                    ClientName = "Client Credentials Gateway",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("51E0D310-F9C5-43CC-9F32-B6271F0155B1".Sha256()) },
                    AllowedScopes = { "application" }
                },
                // SPA client using implicit flow
                new Client
                {
                    ClientId = "application-angular",
                    ClientName = "Application Angular SPA Client",
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowOfflineAccess =true,
                    AccessTokenLifetime = 3300,// 330 seconds, default 60 minutes
                    IdentityTokenLifetime = 28800,
                    AllowedGrantTypes = GrantTypes.Code,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = new List<string> { "openid", "profile", "email", "application" },
                    RedirectUris = new List<string>
                    {
                        "https://localhost:4200/application/auth-callback",
                        "https://localhost:4200/application/signin-oidc",
                        "https://localhost:4200/application/silent-renew",
                        "https://localhost:4200/application",
                        "http://localhost:4200/application/auth-callback",
                        "http://localhost:4200/application",
                        "http://localhost:4200/application/signin-oidc",
                        "http://localhost:4200/application/silent-renew"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:4200",
                        "https://localhost:4200"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:4200",
                        "https://localhost:4200"
                    },
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false
                }
            };
        }
    }
}
