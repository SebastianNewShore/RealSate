using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRealState.GenerateToken.Models
{
    public class TokenRequestModel
    { 
        public string ApiKey { get; set; }
        public string Signature { get; set; }
    }
}