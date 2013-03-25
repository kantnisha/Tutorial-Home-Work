
namespace Rajas.Persona.Web.MyBlood4You.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Rajas.Persona.Domain.Models;
    using Rajas.Persona.Domain.Utility;
    using Rajas.Persona.Helper.Utility;
    using Rajas.Persona.Web.MyBlood4You.Web.Constants;
    using Rajas.Persona.Web.MyBlood4You.Web.ModelBinder;

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = AppControllers.Home, action = AppActionMethods.Home.Index, id = UrlParameter.Optional });
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterBindings();

            WebSiteModel webSiteModel = DomainUtility.GetWebSite(ConfigReader.OwnerApplicationsKey);
            ApplicationObjectHandler.IsApplicationActive = webSiteModel.IsActive;
            ApplicationObjectHandler.ApplicationId = webSiteModel.WebSiteId;
            ApplicationObjectHandler.ApplicationKey = webSiteModel.ConfirmationKey;
            ApplicationObjectHandler.WebsiteDetails = webSiteModel;
        }

        private void RegisterBindings()
        {
            ModelBinders.Binders.Add(typeof(UserModel), new DonorModelBinder());
        }
    }
}