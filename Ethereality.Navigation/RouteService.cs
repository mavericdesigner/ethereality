using System;

namespace Ethereality.Navigation
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