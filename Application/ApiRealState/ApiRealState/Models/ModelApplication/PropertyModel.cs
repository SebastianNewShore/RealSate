using ApiRealState.Models.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRealState.Models.ModelApplication
{
    public class PropertyModel : tbProperty
    {
        public List<string> Image { get; set; }
    }
}