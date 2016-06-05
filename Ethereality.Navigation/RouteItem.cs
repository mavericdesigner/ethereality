namespace Ethereality.Navigation
{
    public class RouteItem
    {
        //private static RouteItem routeItem;

        //public static RouteItem RouteItemInstance
        //{
        //    get
        //    {
        //        routeItem = new RouteItem();

        //        return routeItem;
        //    }
        //}

        public RouteItem()
        {
            RouteModel = new RouteModel();
        }

        public RouteModel RouteModel { get; set; }
    }
}