using System.Web.Mvc;
using System.Web.Routing;

namespace Capela.Web
{
    public class RouteConfig
        {
                public static void RegisterRoutes(RouteCollection routes)
                {
                        routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

                        routes.MapRoute(
                            name: "Pastorais",
                            url: "pastorais",
                            defaults: new { controller = "CapelaGroups", action = "Index" }
                        );

                        routes.MapRoute(
                            name: "PastoraisDetail",
                            url: "pastorais/detalhes/{name}",
                            defaults: new { controller = "CapelaGroups", action = "Details", name = "" }
                        );

                        routes.MapRoute(
                            name: "Eventos",
                            url: "eventos",
                            defaults: new { controller = "CapelaEvents", action = "Index" }
                        );

                        routes.MapRoute(
                            name: "Agenda",
                            url: "agenda",
                            defaults: new { controller = "Home", action = "Contact" }
                        );

                        routes.MapRoute(
                            name: "Mapa",
                            url: "quemsomos",
                            defaults: new { controller = "Home", action = "About" }
                        );

                        routes.MapRoute(
                            name: "Biblia",
                            url: "bibliaonline",
                            defaults: new { controller = "Home", action = "OnlineBible" }
                        );

                        routes.MapRoute(
                            name: "Inicio",
                            url: "inicio",
                            defaults: new { controller = "Home", action = "Index" }
                        );

                        routes.MapRoute(
                            name: "Default",
                            url: "{controller}/{action}/{id}",
                            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                        );
                }
        }
}
