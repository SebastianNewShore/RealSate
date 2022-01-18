using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRealState.GenerateToken.Models
{
    public class TokenModel
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Signature { get; set; }
    }
}