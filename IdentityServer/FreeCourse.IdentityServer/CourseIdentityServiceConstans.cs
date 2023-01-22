// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;

namespace FreeCourse.IdentityServer
{
    public class CourseIdentityServiceConstans
    {
        public static string CatalogResoruce = "resource_catalog";
        public static string PhotoStockResoruce = "resource_photo_stock";
        public static string BasketResource = "resource_basket";
        public static string DiscountResource = "resource_discount";
        public static string OrderResource = "resource_order";
        public static string FakePaymentResource = "resource_fakePayment";
        public static string GatewayResource = "resource_gateway";

        public static string CatalogFullPermission = "catalog_full";
        public static string PhotoStockFullPermission = "photo_stock_full";
        public static string BasketFullPermission = "basket_full";
        public static string DiscountFullPermission = "discount_full";
        public static string OrderFullPermission = "order_full";
        public static string FakePaymentFullPermission = "fakePayment_full";
        public static string GatewayFullPermission = "gateway_full";


        public static string ClientId = "MVCClient";
        public static string ClientName = "MVC";
        public static string Secret = "verySecret".Sha256();
        public static string ClientIdForUser = "MVCClientUser";
        public static string ClientNameForUser = "MVCUser";
        public static string SecretForUser = "verySecretUser".Sha256();


        public static string RoleIdentityResource = "Roles";
    }
}