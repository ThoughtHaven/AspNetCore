using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.DependencyInjection;
using ThoughtHaven;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ControllerBaseExtensions
    {
        public static bool ViewExists(this ControllerBase controller, string viewName)
        {
            Guard.Null(nameof(controller), controller);
            Guard.NullOrWhiteSpace(nameof(viewName), viewName);

            var viewEngine = controller.HttpContext.RequestServices
                .GetRequiredService<ICompositeViewEngine>();
            var result = viewEngine.FindView(controller.ControllerContext, viewName,
                isMainPage: true);

            return result.Success;
        }
    }
}