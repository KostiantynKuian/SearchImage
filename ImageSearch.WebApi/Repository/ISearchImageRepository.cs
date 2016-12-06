using System.Collections.Generic;
using ImageSearch.WebApi.DataModel;
using ImageSearch.WebApi.ViewModels;

namespace ImageSearch.WebApi.Repository
{
    public interface ISearchImageRepository
    {
        FilterRecord SaveFilter(string[] imageUrls);

        List<Filter> InitializeFilters();

        List<ImageRecord> LoadFilter(int filterId);

        void RemoveFilter(int filterId);
    }
}