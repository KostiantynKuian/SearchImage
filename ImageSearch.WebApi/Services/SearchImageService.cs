using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using AutoMapper;
using Google.Apis.Customsearch.v1;
using ImageSearch.WebApi.DataModel;
using ImageSearch.WebApi.Extensions;
using ImageSearch.WebApi.Repository;
using ImageSearch.WebApi.ViewModels;

namespace ImageSearch.WebApi.Services
{
    public class SearchImageService : ISearchImageService
    {
        private readonly ISearchImageRepository _searchImageRepository;

        private readonly string apiKey;

        private readonly string searchEngineId;

        public SearchImageService(ISearchImageRepository searchImageRepository)
        {
            _searchImageRepository = searchImageRepository;
            apiKey = ConfigurationManager.AppSettings["ApiKey"];
            searchEngineId = ConfigurationManager.AppSettings["SearchEngineId"];
        }

        public StreamContent DownloadImageByUrl(string url)
        {
            byte[] pictureBytes;
            using (var webClient = new WebClient())
                pictureBytes = webClient.DownloadData(url);

            var ms = new MemoryStream(pictureBytes);
            var stream = new StreamContent(ms);

            return stream;
        }

        public Filter SaveFilter(string[] imageUrls)
        {
            var filterRecord = _searchImageRepository.SaveFilter(imageUrls);

            return Mapper.Map<FilterRecord, Filter>(filterRecord);
        }

        public List<Filter> GetFilters()
        {
            return _searchImageRepository.InitializeFilters();
        }

        public List<Image> LoadFilter(int filterId)
        {
            var imageRecords = _searchImageRepository.LoadFilter(filterId);

            return Enumerable.ToList(imageRecords.Select(Mapper.Map<ImageRecord, Image>));
        }

        public void RemoveFilterItem(int filterId)
        {
            _searchImageRepository.RemoveFilter(filterId);
        }

        public List<Image> SearchImageByQuery(string keyword, int startFrom)
        {
            var uncodedKeyword = HttpContext.Current.Server.UrlDecode(keyword);
            var initializer = new Google.Apis.Services.BaseClientService.Initializer { ApiKey = apiKey };
            var customSearchService = new CustomsearchService(initializer);
            var listImage = new List<Image>();

            var listRequest = customSearchService.Cse.List(uncodedKeyword);
            listRequest.Cx = searchEngineId;
            listRequest.Start = startFrom;
            listRequest.Num = 10;
            listRequest.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;
            var search = listRequest.Execute();

            foreach (var item in search.Items)
            {
                listImage.Add(new Image
                {
                    Link = item.Link,
                    Title = item.Link.ConvertUrlToImageName()
                });
            }

            return listImage;
        }
    }
}