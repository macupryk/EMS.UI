using System.Web.Mvc;
using EMS.Common.Common;
using EMS.Common.Interfaces;
using EMS.UI.Services;
using Unity;
using Unity.Mvc5;

namespace EMS.UI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<IRestApiCaller, RestApiCaller>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}