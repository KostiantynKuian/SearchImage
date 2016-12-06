using System.Collections.Generic;
using System.Net.Http;
using ImageSearch.WebApi.ViewModels;

namespace ImageSearch.WebApi.Services
{
    public interface ISearchImageService
    {
        List<Image> SearchImageByQuery(string keyword, int startFrom);

        StreamContent DownloadImageByUrl(string url);

        Filter SaveFilter(string[] imageUrls);

        List<Filter> GetFilters();

        List<Image> LoadFilter(int filterId);

        void RemoveFilterItem(int filterId);
    }
}