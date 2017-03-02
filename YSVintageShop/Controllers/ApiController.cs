using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YSVintageShop.Models;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;

using YSVintageShop.Utils;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Text;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NLog;

namespace YSVintageShop.Controllers
{
    public class ApiController : System.Web.Http.ApiController
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _ysContext;
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? Request.GetOwinContext().Get<ApplicationSignInManager>(); // <-- Can't find this
            }
            set
            {
                _signInManager = value;
            }
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationDbContext YSContext
        {
            get
            {
                return _ysContext ?? new ApplicationDbContext();
            }
            set
            {
                _ysContext = value;
            }
        }

        private ApplicationDbContext YSNewContext
        {
            get
            {
                return new ApplicationDbContext();
            }
        
        }


        [Route("api/getallsearchitems")]

        public HttpResponseMessage GetAllSearchItems()
        {
            try
            {
                var brandsList = YSContext.Brands.Include(x => x.Files).Include(x => x.Products).ToList();

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

                var collectionList = YSContext.Collections.Include(x => x.Files).Include(x => x.Products).ToList();


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

                var maincollectionList = YSContext.MainCollections.Include(x => x.Files).ToList();


                var maincollections = from entity in maincollectionList
                                      select new
                                      {
                                          MainCollectionId = entity.MainCollectionId,
                                          Description = entity.Description,
                                          Slug = entity.Slug,
                                          Status = entity.Status,
                                          Title = entity.Title,
                                          ImageName = entity.Files.First().FileName,
                                          ImageContent = entity.Files.First().Content
                                      };

                var mainHeadList = YSContext.MainHeads.Include(x => x.Files).ToList();


                var mainHeads = from entity in mainHeadList
                                select new
                                {
                                    MainHeadId = entity.MainHeadId,
                                    Description = entity.Description,
                                    Slug = entity.Slug,
                                    Status = entity.Status,
                                    Title = entity.Title,
                                    ImageName = entity.Files.First().FileName,
                                    ImageContent = entity.Files.First().Content
                                };

                string json = JsonConvert.SerializeObject(new
                {
                    Brands = brands,
                    Collections = collections,
                    MainHeads = mainHeads,
                    MainCollections = maincollections
                });

                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                //var list = new List<string>();
                //list.Add(ex.Message.ToString());
                //var errors = from entity in list select new { Error = entity };
                //var json = JsonConvert.SerializeObject(brands);
                //return Request.CreateResponse(HttpStatusCode.OK, response, "application/json");

                var response = this.Request.CreateErrorResponse(HttpStatusCode.OK, ex);
                return response;

            }

        }

        [Route("api/getallbrands")]

        public HttpResponseMessage GetAllBrands()
        {
            try
            {
                var brandsList = YSContext.Brands.Include(x => x.Files).Include(x => x.Products).ToList();

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
                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                //var list = new List<string>();
                //list.Add(ex.Message.ToString());
                //var errors = from entity in list select new { Error = entity };
                //var json = JsonConvert.SerializeObject(brands);
                //return Request.CreateResponse(HttpStatusCode.OK, response, "application/json");

                var response = this.Request.CreateErrorResponse(HttpStatusCode.OK, ex);
                return response;

            }

        }

        [Route("api/getallcollections")]

        public HttpResponseMessage GetAllCollections()
        {
            try
            {
                var collectionsList = YSContext.Collections.Include(x => x.Files).Include(x => x.MainCollection).ToList();

                var collections = from entity in collectionsList
                                  select new
                                  {
                                      CollectionId = entity.CollectionId,
                                      Description = entity.Description,
                                      Slug = entity.Slug,
                                      Status = entity.Status,
                                      Title = entity.Title,
                                      MainCollection = entity.MainCollection.Title,
                                      ImageName = entity.Files.First().FileName,
                                      ImageContent = entity.Files.First().Content
                                  };

                var json = JsonConvert.SerializeObject(collections);
                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                var list = new List<string>();
                list.Add(ex.Message.ToString());
                var errors = from entity in list select new { Error = entity };
                //return JsonConvert.SerializeObject(errors);
                var response = this.Request.CreateErrorResponse(HttpStatusCode.OK, ex);
                return response;
            }
        }

        [Route("api/getallmaincollections")]

        public HttpResponseMessage GetAllMainCollections()
        {
            try
            {
                var maincollectionList = YSContext.MainCollections.Include(x => x.Files).ToList();


                var maincollections = from entity in maincollectionList
                                      select new
                                      {
                                          MainCollectionId = entity.MainCollectionId,
                                          Description = entity.Description,
                                          Slug = entity.Slug,
                                          Status = entity.Status,
                                          Title = entity.Title,
                                          ImageName = entity.Files.First().FileName,
                                          ImageContent = entity.Files.First().Content
                                      };

                var json = JsonConvert.SerializeObject(maincollections);
                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                var list = new List<string>();
                list.Add(ex.Message.ToString());
                var errors = from entity in list select new { Error = entity };
                //return JsonConvert.SerializeObject(errors);
                var response = this.Request.CreateErrorResponse(HttpStatusCode.OK, ex);
                return response;
            }
        }

        [Route("api/getallmainheads")]

        public HttpResponseMessage GetAllMainHeads()
        {
            try
            {
                var mainHeadList = YSContext.MainHeads.Include(x => x.Files).ToList();


                var mainHeads = from entity in mainHeadList
                                select new
                                {
                                    MainHeadId = entity.MainHeadId,
                                    Description = entity.Description,
                                    Slug = entity.Slug,
                                    Status = entity.Status,
                                    Title = entity.Title,
                                    ImageName = entity.Files.First().FileName,
                                    ImageContent = entity.Files.First().Content
                                };

                var json = JsonConvert.SerializeObject(mainHeads);
                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                var list = new List<string>();
                list.Add(ex.Message.ToString());
                var errors = from entity in list select new { Error = entity };
                //return JsonConvert.SerializeObject(errors);
                var response = this.Request.CreateErrorResponse(HttpStatusCode.OK, ex);
                return response;
            }
        }

        [Route("api/getsubcollections/{mainCollectionId}")]

        public HttpResponseMessage GetSubCollections(int mainCollectionId)
        {
            try
            {
                var subCollections = YSContext.Collections.Include(x => x.Files).Where(x => x.MainCollectionId == mainCollectionId).ToList();

                var collections = from entity in subCollections
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
                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                var list = new List<string>();
                list.Add(ex.Message.ToString());
                var errors = from entity in list select new { Error = entity };
                //return JsonConvert.SerializeObject(errors);
                var response = this.Request.CreateErrorResponse(HttpStatusCode.OK, ex);
                return response;
            }
        }

        [Route("api/getallproducts")]
        public HttpResponseMessage GetAllProducts()
        {
            try
            {
                //using (var dbCtx = new ApplicationDbContext())
                //{
                    var productList = YSContext.Products.Include(x => x.Brand).Include(x => x.Collection).Include(x => x.Files).ToList();
                    var products = GetProducts(productList);
                    var json = JsonConvert.SerializeObject(products);
                    var response = this.Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    return response;
                //}
            }
            catch (Exception ex)
            {
                var exData = new Dictionary<string, string>();
                exData.Add("error", ex.ToString());
                //return JsonConvert.SerializeObject(errors);
                var response = this.Request.CreateErrorResponse(HttpStatusCode.OK, ex);
                return response;
            }
        }

        [Route("api/getproductswithcollectionid/{id}")]
        public HttpResponseMessage GetProductWithCollectionId(int id)
        {
            try
            {
                var productList = YSContext.Products.Where(x => x.CollectionId == id).Include(x => x.Files).Include(x => x.Brand).Include(x => x.Collection).ToList();
                var products = GetProducts(productList);
                var json = JsonConvert.SerializeObject(products);
                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                var response = this.Request.CreateErrorResponse(HttpStatusCode.OK, ex);
                return response;
            }
        }

        [Route("api/getproductswithbrandid/{id}")]
        public HttpResponseMessage GetProductWithBrandId(int id)
        {
            try
            {
                var productList = YSContext.Products.Where(x => x.BrandId == id).Include(x => x.Files).Include(x => x.Brand).Include(x => x.Collection).ToList();
                var products = GetProducts(productList);
                var json = JsonConvert.SerializeObject(products);
                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                var response = this.Request.CreateErrorResponse(HttpStatusCode.OK, ex);
                return response;
            }
        }


        [HttpPost]
        [Route("api/addproduct")]
        public async Task<HttpResponseMessage> AddProduct()
        {
            var guid = Guid.NewGuid();
            try
            {
                //Log.Debug($"Guid:{guid} AddProduct userId:{userId} userProductRelation:{userProductRelation}");
                var jsonMessage = await Request.Content.ReadAsStringAsync();
                Log.Debug($"Guid:{guid} AddOnlyProduct jsonMessage:{jsonMessage}");

                var product = JsonConvert.DeserializeObject<ProductModel>(jsonMessage);
                //YSContext.Products.Add(product);
                //YSContext.SaveChanges();


                using (var dbCtx = new ApplicationDbContext())
                {
                    //Add Student object into Students DBset
                    dbCtx.Products.Add(product);
                    Log.Debug($"Guid:{guid} AddProduct product added");

                    dbCtx.SaveChanges();
                    Log.Debug($"Guid:{guid} AddProduct product saved");
                }


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error($"Guid:{guid} AddProduct Exception:{ex.Message}");

                var list = new List<string>();
                list.Add(ex.Message.ToString());
                var errors = from entity in list select new { Error = entity };
                return Request.CreateErrorResponse(HttpStatusCode.OK, ex.ToString());
            }
        }


        [HttpPost]
        [Route("api/addtempproduct")]
        public async Task<HttpResponseMessage> AddTempProduct(ProductModel model)
        {
            var guid = Guid.NewGuid();
            try
            {
                //Log.Debug($"Guid:{guid} AddProduct userId:{userId} userProductRelation:{userProductRelation}");
                //var jsonMessage = await Request.Content.ReadAsStringAsync();
                //og.Debug($"Guid:{guid} AddOnlyProduct jsonMessage:{jsonMessage}");

                //   var product = JsonConvert.DeserializeObject<ProductModel>(jsonMessage);
                //YSContext.Products.Add(product);
                //YSContext.SaveChanges();


                using (var context = YSNewContext)
                {
                    context.Products.Add(model);
                    context.SaveChanges();
                }

                ////using (var dbCtx = new YSDbConfiguration())
                ////{
                //    var a = YSContext.Products.ToList();

                //    a.Add(model);
                //    YSContext.SaveChanges();

                ////Add Student object into Students DBset
                //    YSContext.Products.Add(model);
                //    Log.Debug($"Guid:{guid} AddProduct product added");

                //    YSContext.SaveChanges();
                //    Log.Debug($"Guid:{guid} AddProduct product saved");
                ////}


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error($"Guid:{guid} AddProduct Exception:{ex.Message}");

                var list = new List<string>();
                list.Add(ex.Message.ToString());
                var errors = from entity in list select new { Error = entity };
                return Request.CreateErrorResponse(HttpStatusCode.OK, ex.ToString());
            }
        }

        [HttpPost]
        [Route("api/addproduct/{userId}/{userProductRelation}")]
        public async Task<HttpResponseMessage> AddProduct(string userId, int userProductRelation)
        {
            var guid = Guid.NewGuid();
            try
            {
                Log.Debug($"Guid:{guid} AddProduct userId:{userId} userProductRelation:{userProductRelation}");
                var jsonMessage = await Request.Content.ReadAsStringAsync();
                Log.Debug($"Guid:{guid} AddProduct jsonMessage:{jsonMessage}");

                var product = JsonConvert.DeserializeObject<ProductModel>(jsonMessage);
                //YSContext.Products.Add(product);
                //YSContext.SaveChanges();


                using (var dbCtx = new ApplicationDbContext())
                {
                    //Add Student object into Students DBset
                    dbCtx.Products.Add(product);
                    Log.Debug($"Guid:{guid} AddProduct product added");

                    dbCtx.SaveChanges();
                    Log.Debug($"Guid:{guid} AddProduct product saved");


                    var userProduct = dbCtx.UserProduct.FirstOrDefault(x => x.UserId == userId && x.UserProductRelationType == userProductRelation);
                    if (userProduct != null)
                    {
                        userProduct.RelatedProducts.Add(product);
                        Log.Debug($"Guid:{guid} Relation user with product added");

                    }
                    else
                    {
                        if (dbCtx.Users.Any(x => x.Id == userId))
                        {
                            var userProductModel = new UserProductModel()
                            {
                                UserId = userId,
                                UserProductRelationType = userProductRelation,
                                RelatedProducts = new List<ProductModel>()
                                {
                                    product
                                }
                            };

                            dbCtx.UserProduct.Add(userProductModel);
                            Log.Debug($"Guid:{guid} Relation user with product added");

                            //product.UserProductId = userProductModel.Id;
                        }

                    }

                    dbCtx.SaveChanges();
                    Log.Debug($"Guid:{guid} Relation user with product saved");

                }


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error($"Guid:{guid} AddProduct Exception:{ex.Message}");
      
                var list = new List<string>();
                list.Add(ex.Message.ToString());
                var errors = from entity in list select new { Error = entity };
                return Request.CreateErrorResponse(HttpStatusCode.OK, ex.ToString());
            }
        }

        //[HttpPost]
        //[Route("api/addproduct")]
        //public async Task<HttpResponseMessage> GetUserFollowers()
        //{
        //    try
        //    {
        //        var jsonMessage = await Request.Content.ReadAsStringAsync();
        //        var product = JsonConvert.DeserializeObject<UserProductModel>(jsonMessage);
        //        YSContext.Products.Add(product);
        //        YSContext.SaveChanges();
        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        var list = new List<string>();
        //        list.Add(ex.Message.ToString());
        //        var errors = from entity in list select new { Error = entity };
        //        return Request.CreateErrorResponse(HttpStatusCode.OK, ex.ToString());
        //    }


        [HttpPost]
        [Route("api/adduserrelations")]
        public HttpResponseMessage AddUserRelation(string userId)
        {
            try
            {
                var users = YSContext.UserUsers.Where(x => x.UserId == userId).Include(y => y.RelatedUsers).Include(y => y.RelatedUsers.Select(x => x.Files)).ToList();
                //var productList = YSContext.user.Where(x => x.BrandId == id).Include(x => x.Files).Include(x => x.Brand).Include(x => x.Collection).ToList();

                //YSContext
                var userrelations = from entity in users
                                    select new
                                    {
                                        ProductId = entity.UserId,
                                        Description = entity.UserUserRelationType,
                                        Users = from relateUser in entity.RelatedUsers
                                                let file = relateUser.Files
                                                select new
                                                {
                                                    UserId = relateUser.Id,
                                                    UserPhoneNumber = relateUser.PhoneNumber,
                                                    UserName = relateUser.UserName,
                                                    UserEmail = relateUser.Email,
                                                    ImageName = relateUser.Files.First().FileName,
                                                    ImageContent = relateUser.Files.First().Content,
                                                }
                                                //entity.RelatedUsers.
                                    };

                var json = JsonConvert.SerializeObject(userrelations);
                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                var response = this.Request.CreateErrorResponse(HttpStatusCode.OK, ex);
                return response;
            }
        }

        [Route("api/getuserrelations/{id}")]
        public HttpResponseMessage GetUserRelations(string userId)
        {
            try
            {
                var users = YSContext.UserUsers.Where(x => x.UserId == userId).Include(y => y.RelatedUsers).Include(y => y.RelatedUsers.Select(x => x.Files)).ToList();
                //var productList = YSContext.user.Where(x => x.BrandId == id).Include(x => x.Files).Include(x => x.Brand).Include(x => x.Collection).ToList();

                //YSContext
                var userrelations = from entity in users
                                    select new
                                    {
                                        ProductId = entity.UserId,
                                        Description = entity.UserUserRelationType,
                                        Users = from relateUser in entity.RelatedUsers
                                                let file = relateUser.Files
                                                select new
                                                {
                                                    UserId = relateUser.Id,
                                                    UserPhoneNumber = relateUser.PhoneNumber,
                                                    UserName = relateUser.UserName,
                                                    UserEmail = relateUser.Email,
                                                    ImageName = relateUser.Files.First().FileName,
                                                    ImageContent = relateUser.Files.First().Content,
                                                }
                                                //entity.RelatedUsers.
                                    };

                var json = JsonConvert.SerializeObject(userrelations);
                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                var response = this.Request.CreateErrorResponse(HttpStatusCode.OK, ex);
                return response;
            }
        }



        [HttpPost]
        [Route("api/adduser")]
        public async Task<HttpResponseMessage> AddUser()
        {
            try
            {
                //var jsonMessage = await Request.Content.ReadAsStringAsync();
                //var user = JsonConvert.DeserializeObject<UserProductModel2>(jsonMessage);
                //YSContext.UserProducts.Add(user);
                //YSContext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var response = this.Request.CreateErrorResponse(HttpStatusCode.OK, ex);
                return response;
            }
        }


        [HttpPost]
        [Route("api/loginuser")]
        public async Task<HttpResponseMessage> LoginUser()
        {
            try
            {
                var jsonMessage = await Request.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<LoginViewModel>(jsonMessage);
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        var user = YSContext.Users.FirstOrDefault(x => x.Email == model.Email);
                        //return Request.CreateResponse(HttpStatusCode.OK, user, "Success");
                        return Request.CreateResponse(HttpStatusCode.OK, user, "application/json");
                    //user.
                    //return Request.CreateResponse(HttpStatusCode.OK, "Success");
                    case SignInStatus.LockedOut:
                        return Request.CreateErrorResponse(HttpStatusCode.OK, "Lockout");

                    //return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return Request.CreateErrorResponse(HttpStatusCode.OK, "RequiresVerification");

                    // return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.OK, "Invalid login attempt");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, ex);

            }
        }


        [HttpPost]
        [Route("api/registeruser")]
        public async Task<HttpResponseMessage> RegisterUser()
        {
            try
            {
                var jsonMessage = await Request.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<RegisterViewModel>(jsonMessage);

                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    var newUser = new UserModel();
                    newUser.UserId = user.Id;
                    newUser.PhoneNumber = user.PhoneNumber;
                    newUser.Email = user.Email;
                    YSContext.UserDetail.Add(newUser);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    //user.Id
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return Request.CreateResponse(HttpStatusCode.OK, user, "application/json");
                }

                return Request.CreateErrorResponse(HttpStatusCode.OK, result.Errors.Aggregate((current, next) => current + Environment.NewLine + next));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, ex);
            }
        }


        public object GetProducts(List<ProductModel> productList)
        {

            var products = from entity in productList
                           select new
                           {
                               ProductId = entity.ProductId,
                               Description = entity.Description,
                               Slug = entity.Slug,
                               Status = entity.Status,
                               Title = entity.Title,
                               Price = entity.Price,
                               OriginalPrice = entity.OriginalPrice,
                               UsageStatus = entity.UsageStatus,
                               Stock = entity.Stock,
                               ImageName = entity.Files != null && entity.Files.Count > 0 ? entity.Files.First().FileName :"",
                               ImageContent = entity.Files != null && entity.Files.Count > 0 ? entity.Files.First().Content : new byte[] { },
                               BrandId = entity.Brand.BrandId,
                               CollectionId = entity.Collection.CollectionId,
                               MainCollectionId = entity.Collection.MainCollectionId
                           };

            return products;
        }
    }
}