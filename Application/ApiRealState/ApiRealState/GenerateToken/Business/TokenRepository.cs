using ApiRealState.GenerateToken.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ApiRealState.GenerateToken.Business
{
    public class TokenRepository
    {

        private List<TokenModel> apiUsers = new List<TokenModel>()
            {
                new TokenModel
                {
                    ClientId = "UHJ1ZWJhQXBp",
                    ClientSecret = "UHJ1ZWJhQXBp",
                    Signature = "mND1wt48kfeqhetDOi7jTEB9J6fG+rRbzHhlUAK3aqA="
                }
            };

        public List<TokenModel> GetApiUser()
        {
            return apiUsers;
        }
    }
}