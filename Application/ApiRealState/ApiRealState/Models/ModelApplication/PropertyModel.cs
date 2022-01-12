using ApiRealState.Models.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRealState.Models.ModelApplication
{
    public class PropertyModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public decimal InternalValue { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public int Area { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public int Garages { get; set; }
        public DateTime DateInformationUpdate { get; set; }
        public int IdOwner { get; set; }
        public int IdCategory { get; set; }
        public Dictionary<string,string> Images { get; set; }
    }
}