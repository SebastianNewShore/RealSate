using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace ApiRealState.GenerateToken.Business
{
    public class RequireHttpsAttribute: AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var req = actionContext.Request;

            if (req.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                if (req.Method.Method == "GET")
                {
                    actionContext.Response = req.CreateResponse(HttpStatusCode.Found);

                    var uriBuilder = new UriBuilder(req.RequestUri);
                    uriBuilder.Scheme = Uri.UriSchemeHttps;
                    uriBuilder.Port = 443;

                    actionContext.Response.Headers.Location = uriBuilder.Uri;
                }
                else
                {
                    actionContext.Response = req.CreateResponse(HttpStatusCode.NotFound);
                }
            }
        }
    }
}