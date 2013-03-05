using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rajas.Persona.Web.Framework.Constants;
using Rajas.Persona.Domain.Models;
using Rajas.Persona.Domain.Utility;
using Rajas.Persona.Helper.Utility;

namespace MyBlood4You.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

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
                new { controller = Controllers.User.Application, action = "MyBlood4YouHome", id = UrlParameter.Optional });
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            WebSiteModel webSiteModel = DomainUtility.GetWebSite(ConfigReader.OwnerApplicationsKey);
            ApplicationObjectHandler.IsApplicationActive = webSiteModel.IsActive;
            ApplicationObjectHandler.ApplicationId = webSiteModel.WebSiteId;
            ApplicationObjectHandler.ApplicationKey = webSiteModel.ConfirmationKey;
            ApplicationObjectHandler.WebsiteDetails = webSiteModel;
        }
    }
}