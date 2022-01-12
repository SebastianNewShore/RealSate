using ApiRealState.Models.ModelDB;
using System;
using System.Linq;
using System.Web.Http;
using ApiRealState.Constants;
using System.Collections.Generic;
using System.Data.Entity;
using ApiRealState.Models.ModelApplication;
using System.IO;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace ApiRealState.Controllers
{
    public class PropertyController : ApiController
    {
        [HttpGet]
        public object GetProperty(string area, string value, string rooms, string bathrooms, string Garages)
        {
            var property = new object();
            using (dbRealState db = new dbRealState())
            {
                try
                {
                    var a = area != null ? area.Split(',') : new string[0];
                    var v = value != null ? value.Split(',') : new string[0];
                    var r = rooms != null ? rooms.Split(',') : new string[0];
                    var b = bathrooms != null ? bathrooms.Split(',') : new string[0];
                    var g = Garages != null ? Garages.Split(',') : new string[0];

                    property = (from p in db.tbProperties
                                //where (p.Area >= a[0] && p.Area <= a[1]) &&
                                //      (p.InternalValue >= v[0] && p.InternalValue <= v[1]) &&
                                //      (p.Rooms >= r[0] && p.Rooms <= r[1]) &&
                                //      (p.Bathrooms >= b[0] && p.Bathrooms <= b[1]) &&
                                //      (p.Garages >= g[0] && p.Garages <= g[1])
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

        [HttpPost]
        public void CreateProperty(PropertyModel propertyModel)
        {
            using (dbRealState db = new dbRealState())
            {
                try
                {
                    if (!db.tbProperties.Any(x => x.Code == propertyModel.Code))
                    {
                        var property = Property(propertyModel);
                        var images = Image(propertyModel, property);

                        SaveDatabase(property, images);
                        SaveImages(propertyModel.Images, propertyModel.Code);
                    }
                    else
                    {
                            ModelState.AddModelError("", Constants.Constants.PropertyExist);
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(Constants.Constants.Error, ex);
                }
            }
        }

        [HttpPut]
        public void UpdateProperty(int id,PropertyModel propertyModel)
        {
            using (dbRealState db = new dbRealState())
            {
                try
                {
                    var property = Property(propertyModel);
                    var images = Image(propertyModel,property);

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
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(Constants.Constants.Error, ex);
                }
            }
        }

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

        private void SaveDatabase(tbProperty property, List<tbImage> images )
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