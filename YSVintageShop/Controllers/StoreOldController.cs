using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YSVintageShop.Models;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;

using YSVintageShop.Utils;
using Newtonsoft.Json;

namespace AngularJSWebApiEmpty.Controllers
{
    public class YSStoreOldController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        public string GetAllBrands()
        {
            try
            {
                var brandsList = db.Brands.Include(x => x.Files).Include(x => x.Products).ToList();

                var brands = from entity in brandsList
                             select new
                             {
                                 BrandId = entity.BrandId,
                                 Description = entity.Description,
                                 Slug = entity.Slug,
                                 Status = entity.Status,
                                 Title = entity.Title,
                                 ImageName = entity.Files.First().FileName,
                                 ImageContent = entity.Files.First().Content
                             };

                var json = JsonConvert.SerializeObject(brands);
                return json;
            }
            catch(Exception ex)
            {
                var list = new List<string>();
                list.Add(ex.Message.ToString());
                var errors = from entity in list select new { Error = entity };
                return JsonConvert.SerializeObject(errors);
            }
      
        }

        public string GetAllCollections()
        {
            try
            {
                var collectionList = db.Collections.Include(x => x.Files).Include(x => x.Products).ToList();


                var collections = from entity in collectionList
                             select new
                             {
                                 CollectionId = entity.CollectionId,
                                 Description = entity.Description,
                                 Slug = entity.Slug,
                                 Status = entity.Status,
                                 Title = entity.Title,
                                 ImageName = entity.Files.First().FileName,
                                 ImageContent = entity.Files.First().Content
                             };

                var json = JsonConvert.SerializeObject(collections);
                return json;
            }
            catch (Exception ex)
            {
                var list = new List<string>();
                list.Add(ex.Message.ToString());
                var errors = from entity in list select new { Error = entity };
                return JsonConvert.SerializeObject(errors);
            }
        }

        public string GetAllProducts()
        {
            try
            {
                var productList = db.Products.Include(x => x.Files).Include(x => x.Brand).Include(x=> x.Collection).ToList();

                var products = from entity in productList
                             select new
                             {
                                 ProductId = entity.ProductId,
                                 Description = entity.Description,
                                 Slug = entity.Slug,
                                 Status = entity.Status,
                                 Title = entity.Title,
                                 Price = entity.Price,
                                 Stock = entity.Stock,
                                 ImageName = entity.Files.First().FileName,
                                 ImageContent = entity.Files.First().Content,
                                 BrandId = entity.Brand.BrandId,
                                 CollectionId = entity.Collection.CollectionId
                             };

                var json = JsonConvert.SerializeObject(products);
                return json;
            }
            catch (Exception ex)
            {
                var exData = new Dictionary<string, string>();
                exData.Add("error", ex.ToString());
                var json = JsonConvert.SerializeObject(exData);
                return json;
            }
        }

        public string GetProductWithCollectionId(int id)
        {
            try
            {
                var productList = db.Products.Where(x=> x.CollectionId == id).Include(x => x.Files).Include(x => x.Brand).Include(x => x.Collection).ToList();

                var products = from entity in productList
                               select new
                               {
                                   ProductId = entity.ProductId,
                                   Description = entity.Description,
                                   Slug = entity.Slug,
                                   Status = entity.Status,
                                   Title = entity.Title,
                                   Price = entity.Price,
                                   Stock = entity.Stock,
                                   ImageName = entity.Files.First().FileName,
                                   ImageContent = entity.Files.First().Content,
                                   BrandId = entity.Brand.BrandId,
                                   CollectionId = entity.Collection.CollectionId
                               };

                var json = JsonConvert.SerializeObject(products);
                return json;
            }
            catch (Exception ex)
            {
                var list = new List<string>();
                list.Add(ex.Message.ToString());
                var errors = from entity in list select new { Error = entity };
                return JsonConvert.SerializeObject(errors);
            }
        }


        public string GetProductWithBrandId(int id)
        {
            try
            {
                var productList = db.Products.Where(x => x.BrandId == id).Include(x => x.Files).Include(x => x.Brand).Include(x => x.Collection).ToList();

                var products = from entity in productList
                               select new
                               {
                                   ProductId = entity.ProductId,
                                   Description = entity.Description,
                                   Slug = entity.Slug,
                                   Status = entity.Status,
                                   Title = entity.Title,
                                   Price = entity.Price,
                                   Stock = entity.Stock,
                                   ImageName = entity.Files.First().FileName,
                                   ImageContent = entity.Files.First().Content,
                                   BrandId = entity.Brand.BrandId,
                                   CollectionId = entity.Collection.CollectionId
                               };

                var json = JsonConvert.SerializeObject(products);
                return json;
            }
            catch (Exception ex)
            {
                var list = new List<string>();
                list.Add(ex.Message.ToString());
                var errors = from entity in list select new { Error = entity };
                return JsonConvert.SerializeObject(errors);
            }
        }

     

    }
}