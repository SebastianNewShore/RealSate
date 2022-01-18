using ApiRealState.GenerateToken.Business;
using ApiRealState.GenerateToken.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace ApiRealState.GenerateToken.Controllers
{
    public class TokenController: ApiController
    {
        private TokenRepository appRepository;
        private AuthTokenRepository tokenRepository;

        public TokenController()
        {
            this.appRepository = new TokenRepository();
            this.tokenRepository = new AuthTokenRepository();
        }

        public HttpResponseMessage Post([FromBody]TokenRequestModel model)
        {
            try
            {
                var user = this.appRepository.GetApiUser().Where(w => w.ClientId == model.ApiKey).FirstOrDefault();
                if(user != null)
                {
                    var secret = user.ClientSecret;

                    var key = Convert.FromBase64String(secret);
                    var provider = new System.Security.Cryptography.HMACSHA256(key);

                    var hash = provider.ComputeHash(Encoding.UTF8.GetBytes(user.ClientId));
                    var signature = Convert.ToBase64String(hash);

                    if(signature == model.Signature)
                    {
                        var rawTokenInfo = string.Concat(user.ClientId + DateTime.UtcNow.ToString("d"));
                        var rawTokenByte = Encoding.UTF8.GetBytes(rawTokenInfo);
                        var token = provider.ComputeHash(rawTokenByte);
                        var authToken = new AuthTokenModel()
                        {
                            Token = Convert.ToBase64String(token),
                            Expiration = DateTime.Now.AddDays(1),
                            ApiUser = user
                        };
                        if(tokenRepository.Insert(authToken))
                        {
                            return Request.CreateResponse(HttpStatusCode.Created, new ExpirationTokenModel
                            {
                                Token = authToken.Token,
                                Expiration = authToken.Expiration
                            });
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}