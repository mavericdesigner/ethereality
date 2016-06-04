using System;

namespace Isis.Model.Route
{
    public interface IRouteService
    {
        void RouteData(Action<RouteItem, Exception> routecallback);
    }
}