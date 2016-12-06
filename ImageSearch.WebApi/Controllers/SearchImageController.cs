using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using ImageSearch.WebApi.Async;
using ImageSearch.WebApi.Extensions;
using ImageSearch.WebApi.Services;
using ImageSearch.WebApi.ViewModels;

namespace ImageSearch.WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class SearchImageController : WebApiController
    {
        private readonly ISearchImageService _searchImageService;

        public SearchImageController(ISearchImageService searchImageService)
        {
            _searchImageService = searchImageService;
        }

        [HttpGet]
        public HttpResponseMessage DownloadImage(string url)
        {
            return ExecuteAction(() => _searchImageService.DownloadImageByUrl(url), "image/JPEG");
        }

        [HttpPost]
        public HttpResponseMessage SaveFilter(string[] imageUrls)
        {
            return ExecuteAction(() => _searchImageService.SaveFilter(imageUrls));
        }

        [HttpGet]
        public HttpResponseMessage InitializeFilter()
        {
            return ExecuteAction(() => _searchImageService.GetFilters());
        }

        [HttpGet]
        public HttpResponseMessage LoadFilter(int filterId)
        {
            return ExecuteAction(() => _searchImageService.LoadFilter(filterId));
        }
        
        [HttpPost]
        public HttpResponseMessage RemoveFilterItem([FromBody] int filterId)
        {
            return ExecuteAction(() => _searchImageService.RemoveFilterItem(filterId));
        }

        [HttpGet]
        public HttpResponseMessage SearchImage(string keyword, int startFrom = 1)
        {
            return ExecuteAction(() => _searchImageService.SearchImageByQuery(keyword, startFrom));
        }
    }
}