using System;

namespace Ethereality.Navigation
{
    public interface IRouteService
    {
        void RouteData(Action<RouteItem, Exception> routecallback);
    }
}