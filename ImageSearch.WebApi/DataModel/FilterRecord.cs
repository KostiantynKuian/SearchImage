using System.Collections.Generic;

namespace ImageSearch.WebApi.DataModel
{
    public class FilterRecord
    {
        public int Id { get; set; }

        public virtual List<ImageRecord> Images { get; set; }
    }
}