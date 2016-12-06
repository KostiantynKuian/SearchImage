using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ImageSearch.WebApi.Async
{
    public class WebApiController : ApiController
    {
        protected HttpResponseMessage ExecuteAction(Func<object> action)
        {
            try
            {
                var result = action();  

                return Request.CreateResponse(HttpStatusCode.OK, result); ;
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        protected HttpResponseMessage ExecuteAction(Action action)
        {
            try
            {
                action();
                
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        protected HttpResponseMessage ExecuteAction(Func<StreamContent> action, string mediaType)
        {
            try
            {
                var response = new HttpResponseMessage();
                var result = action();


                if (result == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                }
                else
                {
                    response.Content = result;
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");

                    response.StatusCode = HttpStatusCode.OK;
                }

                return response;
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}