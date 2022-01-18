using ApiRealState.GenerateToken.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRealState.GenerateToken.Business
{
    public class AuthTokenRepository
    {
        private static List<AuthTokenModel> list = new List<AuthTokenModel>();

        public AuthTokenModel GetAuthToken(string token)
        {
            return list.Where(w => w.Token == token).FirstOrDefault();
        }

        public bool Insert(AuthTokenModel token)
        {
            list.Add(token);
            return true;
        }
    }
}