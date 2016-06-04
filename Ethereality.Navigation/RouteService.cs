using System;

namespace Isis.Model.Route
{
    public class RouteService : IRouteService
    {
        public void RouteData(Action<RouteItem, Exception> routecallback)
        {
            RouteItem routeItem = new RouteItem();
            routecallback(routeItem, null);
        }
    }
}