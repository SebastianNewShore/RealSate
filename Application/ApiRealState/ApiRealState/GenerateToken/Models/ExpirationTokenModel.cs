using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRealState.GenerateToken.Models
{
    public class ExpirationTokenModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}