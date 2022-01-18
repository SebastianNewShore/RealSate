using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Http.Filters;

namespace ApiRealState.GenerateToken.Business
{
    public class AuthorizeAttribute : AuthorizationFilterAttribute
    {
        private AuthTokenRepository tokenRepo;

        public AuthorizeAttribute()
        {
            this.tokenRepo = new AuthTokenRepository();
        }
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var query = HttpUtility.ParseQueryString(actionContext.Request.RequestUri.Query);

            if(!string.IsNullOrWhiteSpace(query[Constants.Constants.tokenName]))
            {
                var token = query[Constants.Constants.tokenName];

                var authToken = tokenRepo.GetAuthToken(token);

                if (authToken != null && authToken.Expiration > DateTime.UtcNow)
                {
                    return;
                }
            }
            HandlerUnauthorized(actionContext);

        }

        public void HandlerUnauthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }
}