using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Habitual.WebAPI.Controllers
{
    public class BaseController : ApiController
    {
        protected static readonly string CONNECTION_STRING = "Omitted";
        protected HttpResponseMessage BuildSuccessResult(HttpStatusCode statusCode)
        {
            return this.Request.CreateResponse(statusCode);
        }

        protected HttpResponseMessage BuildSuccessResult(HttpStatusCode statusCode, object data)
        {
            return data != null ? this.Request.CreateResponse(statusCode, data) : this.Request.CreateResponse(statusCode);
        }
        
        protected HttpResponseMessage BuildSuccessResultList<T>(HttpStatusCode statusCode, IEnumerable<T> data)
        {
            return data != null ? this.Request.CreateResponse<IEnumerable<T>>(statusCode, data) : this.Request.CreateResponse(statusCode);
        }

        protected HttpResponseMessage BuildErrorResult(HttpStatusCode statusCode, string message = "Error")
        {
            return BuildErrorResult(statusCode, message);
        }
    }
}
