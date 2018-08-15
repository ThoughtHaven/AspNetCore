using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ThoughtHaven.AspNetCore.Mvc.Fakes
{
    public class FakeControllerBase : ControllerBase
    {
        public FakeControllerBase(ServiceCollection services)
        {
            this.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    RequestServices = services.BuildServiceProvider()
                }
            };
        }
    }
}