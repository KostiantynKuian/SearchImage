namespace ImageSearch.WebApi.DataModel
{
    public class ImageRecord
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public int FilterId { get; set; }

        public virtual FilterRecord Filter { get; set; }
    }
}