using Microsoft.Practices.Unity;
using System.Web.Http;
using Jewson.Data.Interfaces;
using Jewson.Data.Repositories;
using Jewson.RestService.Business.DataRetrieval;
using Jewson.RestService.Business.Interfaces;
using Unity.WebApi;

namespace Jewson.RestService
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IDataProvider, DataProvider>();
            container.RegisterType<IDataRepository, StaticDataRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}