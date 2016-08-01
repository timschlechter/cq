using System;
using System.Web;
using System.Web.Http;

namespace Samples.WebApi
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Configure);
        }
    }
}