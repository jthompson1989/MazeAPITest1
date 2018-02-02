using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace MapAPITest.App_Start
{
    public class WebApiConfig
    {
        public static void Configure(HttpConfiguration config)
        {

            config.Formatters.Add(new JsonMediaTypeFormatter());
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}