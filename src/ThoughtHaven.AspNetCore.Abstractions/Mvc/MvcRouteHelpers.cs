namespace ThoughtHaven.AspNetCore.Mvc
{
    public static class MvcRouteHelpers
    {
        public static string Controller<TController>() =>
            typeof(TController).Name.Replace("Controller", string.Empty);

        public static object Defaults<TController>(string action) => new
        {
            controller = Controller<TController>(),
            action = Guard.NullOrWhiteSpace(nameof(action), action),
        };
    }
}