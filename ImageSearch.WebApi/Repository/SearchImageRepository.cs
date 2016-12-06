using System.Collections.Generic;
using System.Linq;
using ImageSearch.WebApi.Context;
using ImageSearch.WebApi.DataModel;
using ImageSearch.WebApi.ViewModels;

namespace ImageSearch.WebApi.Repository
{
    public class SearchImageRepository : ISearchImageRepository
    {
        public FilterRecord SaveFilter(string[] imageUrls)
        {
            var filter = new FilterRecord();
            if (imageUrls.Any())
            {
                using (var db = new FilterContext())
                {
                    db.Filters.Add(filter);
                    db.SaveChanges();
                    var images = imageUrls.Select(url => new ImageRecord
                    {
                        FilterId = filter.Id,
                        Url = url
                    }).ToList();

                    filter.Images = images;

                    db.SaveChanges();
                }
            }
            return filter;
        }

        public List<ImageRecord> LoadFilter(int filterId)
        {
            List<ImageRecord> images;
            using (var db = new FilterContext())
            {
                images = db.Images.Where(x => x.FilterId == filterId)
                           .ToList();
            }

            return images;
        }

        public void RemoveFilter(int filterId)
        {
            using (var db = new FilterContext())
            {
                var items = db.Filters.First(x => x.Id == filterId);
                db.Filters.Remove(items);

                db.SaveChanges();
            }
        }

        public List<Filter> InitializeFilters()
        {
            List<Filter> filters;
            using (var db = new FilterContext())
            {
                filters = db.Images
                            .GroupBy(x => x.FilterId)
                            .Select(gr => new Filter { Id = gr.Key, ImageCount = gr.Count() })
                            .ToList();
            }
            return filters;
        }
    }
}