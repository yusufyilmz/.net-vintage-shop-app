//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Security.Principal;
//using System.Web;
//using YSVintageShop.Models;

//namespace YSVintageShop.Extensions
//{

//    public static class IdentityExtensions
//    {
//        public static List<ProductModel> GetProducts(this IIdentity identity)
//        {
//            var claim = ((ClaimsIdentity)identity).FindAll("Products");

//            if(claim != null)
//            {
//                var products = (List<ProductModel>)(claim);
//                return products;
//            }
//            // Test for null to avoid issues during local testing
//            return new List<ProductModel>();
//        }
//    }
//}