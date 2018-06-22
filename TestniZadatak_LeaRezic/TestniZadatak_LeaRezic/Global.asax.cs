using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TestniZadatak_LeaRezic
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        // copy-paste nažalost, nisam još dovoljno istražila lijepo lovljenje grešaka u MVC-u, probala sam
        // na isti način kao kod web-formsa (makar ni to nije bila prevelika sreća) ali mi je bilo napola krivo,
        // pa sam posudila od interneta - čisto da ne puca aplikacija bezveze :) (makar tu sad nema detalja greške)
        protected void Application_Error()
        {
            HttpContext ctx = HttpContext.Current;
            KeyValuePair<string, object> error = new KeyValuePair<string, object>("ErrorMessage", ctx.Server.GetLastError().ToString());
            ctx.Response.Clear();
            RequestContext rc = ((MvcHandler)ctx.CurrentHandler).RequestContext;
            string controllerName = rc.RouteData.GetRequiredString("controller");
            IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();
            IController controller = factory.CreateController(rc, controllerName);
            ControllerContext cc = new ControllerContext(rc, (ControllerBase)controller);

            ViewResult viewResult = new ViewResult { ViewName = "Error" };
            viewResult.ViewData.Add(error);
            viewResult.ExecuteResult(cc);
            ctx.Server.ClearError();
        }
    }
}
