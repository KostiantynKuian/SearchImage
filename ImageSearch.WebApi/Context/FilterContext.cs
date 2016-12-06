using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ImageSearch.WebApi.DataModel;

namespace ImageSearch.WebApi.Context
{
    public class FilterContext : DbContext
    {
        public DbSet<ImageRecord> Images { get; set; }

        public DbSet<FilterRecord> Filters { get; set; }

        public FilterContext() : base("FilterContext")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<ImageRecord>()
                        .HasRequired(a => a.Filter)
                        .WithMany()
                        .WillCascadeOnDelete(true);
        }
    }
}