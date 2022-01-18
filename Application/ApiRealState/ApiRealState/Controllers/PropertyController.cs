using ApiRealState.Models.ModelDB;
using System;
using System.Linq;
using System.Web.Http;
using System.Collections.Generic;
using System.Data.Entity;
using ApiRealState.Models.ModelApplication;
using System.IO;
using System.Net.Http;
using System.Net;
using ApiRealState.GenerateToken.Business;
using AuthorizeAttribute = ApiRealState.GenerateToken.Business.AuthorizeAttribute;

namespace ApiRealState.Controllers
{
    [Authorize]
    [RequireHttps]
    public class PropertyController : ApiController
    {
        ///<summary>
        ///Method to obtain information of all the properties with different search filters.
        ///</summary>
        ///
        [System.Web.Http.HttpGet]
        public object GetProperty(string area, string value, string rooms, string bathrooms, string Garages)
        {
            var property = new object();
            using (dbRealState db = new dbRealState())
            {
                try
                {
                    var a = area != null ? area.Split(',') : null;
                    var v = value != null ? value.Split(',') : null;
                    var r = rooms != null ? rooms.Split(',') : null;
                    var b = bathrooms != null ? bathrooms.Split(',') : null;
                    var g = Garages != null ? Garages.Split(',') : null;

                    property = (from p in db.tbProperties.AsEnumerable()
                                where (a == null || (p.Area >= int.Parse(a[0]) && p.Area <= int.Parse(a[1]))) &&
                                      (v == null || (p.InternalValue >= int.Parse(v[0]) && p.InternalValue <= int.Parse(v[1]))) &&
                                      (r == null || (p.Rooms >= int.Parse(r[0]) && p.Rooms <= int.Parse(r[1]))) &&
                                      (b == null || (p.Bathrooms >= int.Parse(b[0]) && p.Bathrooms <= int.Parse(b[1]))) &&
                                      (g == null || (p.Garages >= int.Parse(g[0]) && p.Garages <= int.Parse(g[1])))
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

        ///<summary>
        ///Create a new property and the images associated with it.
        ///</summary>
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CreateProperty(PropertyModel propertyModel)
        {
            using (dbRealState db = new dbRealState())
            {
                var request = new HttpRequestMessage();
                try
                {                  
                    if (!db.tbProperties.Any(x => x.Code == propertyModel.Code))
                    {

                        db.tbProperties.Add(Property(propertyModel));
                        foreach (var image in Image(propertyModel, Property(propertyModel)))
                        {
                            db.tbImages.Add(image);
                        }

                        db.SaveChanges();
                        SaveImages(propertyModel.Images, propertyModel.Code);

                        return request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return request.CreateResponse(HttpStatusCode.NotModified);
                    }

                }
                catch (Exception ex)
                {
                    return request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
        }

        ///<summary>
        ///Update property and the images associated with it.
        ///</summary>
        [System.Web.Http.HttpPut]
        public HttpResponseMessage UpdateProperty(int id, PropertyModel propertyModel)
        {
            using (dbRealState db = new dbRealState())
            {
                var request = new HttpRequestMessage();
                try
                {
                    var property = Property(propertyModel);
                    var images = Image(propertyModel, property);

                    property.Id = id;

                    db.Entry(property).State = EntityState.Modified;

                    foreach (var image in images)
                    {
                        image.IdProperty = id;
                        image.Id = (from i in db.tbImages where i.File == image.File select i.Id).FirstOrDefault();
                        db.Entry(image).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                    SaveImages(propertyModel.Images, propertyModel.Code);

                    return request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    return request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
        }

        ///<summary>
        ///construct object to save or update a property.
        ///</summary>
        private tbProperty Property(PropertyModel propertyModel)
        {
            var property = new tbProperty();
            try
            {
                property.Code = propertyModel.Code;
                property.Name = propertyModel.Name;
                property.Adress = propertyModel.Adress;
                property.InternalValue = propertyModel.InternalValue;
                property.Description = propertyModel.Description;
                property.PublicationDate = propertyModel.PublicationDate;
                property.Area = propertyModel.Area;
                property.Rooms = propertyModel.Rooms;
                property.Bathrooms = propertyModel.Bathrooms;
                property.Garages = propertyModel.Garages;
                property.DateInformationUpdate = propertyModel.DateInformationUpdate;
                property.IdOwner = propertyModel.IdOwner;
                property.IdCategory = propertyModel.IdCategory;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(Constants.Constants.Error, ex);
            }
            return property;
        }

        ///<summary>
        ///construct object to save or update a Images.
        ///</summary>
        private List<tbImage> Image(PropertyModel propertyModel, tbProperty property)
        {
            var images = new List<tbImage>();
            try
            {
                foreach (var image in propertyModel.Images)
                {
                    images.Add(new tbImage
                    {
                        tbProperty = property,
                        File = image.Key.ToString(),
                        Enabled = Constants.Constants.Enabled
                    });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(Constants.Constants.Error, ex);
            }
            return images;
        }

        ///<summary>
        ///Create folder and save images of the property.
        ///</summary>
        private void SaveImages(Dictionary<string, string> images, string code)
        {
            try
            {
                string folderPath = AppDomain.CurrentDomain.BaseDirectory + Constants.Constants.folder + code;
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                foreach (var image in images)
                {
                    byte[] imageBytes = Convert.FromBase64String(image.Value);
                    var path = Path.Combine(folderPath, image.Key + Constants.Constants.Extension);
                    File.WriteAllBytes(path, imageBytes);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(Constants.Constants.Error, ex);
            }
        }
    }
}