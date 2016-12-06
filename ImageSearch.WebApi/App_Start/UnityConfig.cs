using Microsoft.Practices.Unity;
using System.Web.Http;
using ImageSearch.WebApi.Repository;
using ImageSearch.WebApi.Services;
using Unity.WebApi;

namespace ImageSearch.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<ISearchImageService, SearchImageService>();
            container.RegisterType<ISearchImageRepository, SearchImageRepository>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}