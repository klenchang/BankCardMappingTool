using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace AutoMapBankCard
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapPageRoute("Index", "Index", "~/Index.aspx");
            routes.MapPageRoute("DataUpload", "Upload", "~/Upload.aspx");
            routes.MapPageRoute("CheckBankCard", "CheckBankCard", "~/CheckBankCard.aspx");
            routes.MapPageRoute("Notify", "Notify", "~/Notify.aspx");
            routes.MapPageRoute("Search", "Search", "~/Search.aspx");
            routes.MapPageRoute("Error", "Error", "~/Error.aspx");
        }
    }
}