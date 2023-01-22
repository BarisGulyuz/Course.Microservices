using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {
        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource(CourseIdentityServiceConstans.CatalogResoruce) { Scopes = {CourseIdentityServiceConstans.CatalogFullPermission } },
                new ApiResource(CourseIdentityServiceConstans.PhotoStockResoruce) { Scopes = {CourseIdentityServiceConstans.PhotoStockFullPermission } },
                new ApiResource(CourseIdentityServiceConstans.BasketResource) { Scopes = {CourseIdentityServiceConstans.BasketFullPermission } },
                new ApiResource(CourseIdentityServiceConstans.DiscountResource) { Scopes = {CourseIdentityServiceConstans.DiscountFullPermission } },
                new ApiResource(CourseIdentityServiceConstans.OrderResource) { Scopes = {CourseIdentityServiceConstans.OrderFullPermission } },
                new ApiResource(CourseIdentityServiceConstans.FakePaymentResource) { Scopes = {CourseIdentityServiceConstans.FakePaymentFullPermission } },
                new ApiResource(CourseIdentityServiceConstans.GatewayResource) { Scopes = {CourseIdentityServiceConstans.GatewayFullPermission } },
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
            };

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource() {
                           Name = CourseIdentityServiceConstans.RoleIdentityResource,
                           DisplayName =  CourseIdentityServiceConstans.RoleIdentityResource,
                           UserClaims = new []{ "role" }
                       }
                   };

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(CourseIdentityServiceConstans.CatalogFullPermission),
                new ApiScope(CourseIdentityServiceConstans.PhotoStockFullPermission),
                new ApiScope(CourseIdentityServiceConstans.BasketFullPermission),
                new ApiScope(CourseIdentityServiceConstans.DiscountFullPermission),
                new ApiScope(CourseIdentityServiceConstans.OrderFullPermission),
                new ApiScope(CourseIdentityServiceConstans.FakePaymentFullPermission),
                new ApiScope(CourseIdentityServiceConstans.GatewayFullPermission),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
               //Client Credentials
                new Client
                {
                    ClientId = CourseIdentityServiceConstans.ClientId,
                    ClientName = CourseIdentityServiceConstans.ClientName,
                    ClientSecrets ={  new Secret(CourseIdentityServiceConstans.Secret) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { CourseIdentityServiceConstans.CatalogFullPermission ,
                                      CourseIdentityServiceConstans.PhotoStockFullPermission,
                                      CourseIdentityServiceConstans.FakePaymentFullPermission,
                                      CourseIdentityServiceConstans.GatewayFullPermission,
                                      IdentityServerConstants.LocalApi.ScopeName
                    }
                },

                   //ResourceOwnerPassword
                 new Client
                {
                    ClientId = CourseIdentityServiceConstans.ClientIdForUser,
                    ClientName = CourseIdentityServiceConstans.ClientNameForUser,
                    AllowOfflineAccess = true,
                    ClientSecrets ={  new Secret (CourseIdentityServiceConstans.SecretForUser) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.Email,
                                      IdentityServerConstants.StandardScopes.OpenId,
                                      IdentityServerConstants.StandardScopes.Profile,
                                      IdentityServerConstants.StandardScopes.OfflineAccess,
                                      IdentityServerConstants.LocalApi.ScopeName,
                                      "roles",
                                      CourseIdentityServiceConstans.BasketFullPermission,
                                      CourseIdentityServiceConstans.DiscountFullPermission,
                                      CourseIdentityServiceConstans.OrderFullPermission,
                                      CourseIdentityServiceConstans.FakePaymentFullPermission,
                                      CourseIdentityServiceConstans.GatewayFullPermission
                     },
                    AccessTokenLifetime = 3600,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                }
            };
    }

}