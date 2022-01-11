using ApiRealState.Models.ModelDB;
using System;
using System.Linq;
using System.Web.Http;
using ApiRealState.Constants;
using System.Collections.Generic;
using System.Data.Entity;
using ApiRealState.Models.ModelApplication;

namespace ApiRealState.Controllers
{
    public class PropertyController : ApiController
    {
        // GET api/values/5
        public object Get(int id)
        {
            var property = new object();
            using (dbRealState db = new dbRealState())
            {
                try
                {
                    property = (from p in db.tbProperties
                                where p.Id == id
                                select new
                                {
                                    p.Code,
                                    p.Name,
                                    p.Adress,
                                    p.Rooms,
                                    p.Area,
                                    p.Bathrooms,
                                    p.Garages,
                                    p.Description,
                                    Images = (from i in db.tbImages
                                              where i.IdProperty == p.Id && i.Enabled == 1
                                              select new
                                              {
                                                  i.File
                                              }).ToList()
                                }).ToList();

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(Constants.Constants.Error, ex);
                }
            }

            return property;
        }

        public void CreateProperty(PropertyModel propertyModel)
        {
            var property = new tbProperty();
            var image = new tbImage();
            using (dbRealState db = new dbRealState())
            {
                try
                {
                    if (!db.tbProperties.Any(x => x.Code == propertyModel.Code))
                    {
                        var product = new tbProduct();
                        var Categories = new List<tbSubCategoryProduct>();

                        product.Code = p.Code;
                        product.Name = p.Name;
                        product.State = p.State;
                        product.Stock = p.Stock;
                        product.Discount = p.Discount;
                        product.InternalPrice = p.InternalPrice;
                        product.PublicPrice = p.PublicPrice;
                        product.IdBrand = p.IdBrand;
                        product.IdProvider = p.IdProvider;
                        product.Description = p.Description;
                        product.ProductDetail = RestrictionConstants.JsonMockup;

                        foreach (var category in p.Categories.Split(','))
                        {
                            Categories.Add(new tbSubCategoryProduct
                            {
                                tbProduct = product,
                                IdSubCategory = int.Parse(category)
                            });
                        }

                        if (this.Request.Form[RestrictionConstants.SaveMode] == RestrictionConstants.DataBase)
                            SaveDatabase(product, Categories);
                        else
                        {
                            var productsMemory = ProductsMemory();
                            productsMemory.Add(product);
                            GenericMethods.CreateCookie(JsonConvert.SerializeObject(productsMemory), RestrictionConstants.ProductCookie);
                        }

                        return RedirectToAction("ListProducts");
                    }
                    else
                    {
                            ModelState.AddModelError("", Constants.Constants.PropertyExist);
                    }
                    SaveDatabase(property, new List<tbImage>());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(Constants.Constants.Error, ex);
                }
            }
        }

        private void SaveDatabase(tbProperty property, List<tbImage> images)
        {
            using (dbRealState db = new dbRealState())
            {
                try
                {
                    db.tbProperties.Add(property);
                    foreach (var image in images)
                    {
                        db.tbImages.Add(image);
                    }

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(Constants.Constants.Error, ex);
                }
            }
        }
    }
}