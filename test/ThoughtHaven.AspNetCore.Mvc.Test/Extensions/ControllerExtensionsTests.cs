using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.DependencyInjection;
using System;
using ThoughtHaven.AspNetCore.Mvc.Fakes;
using Xunit;

namespace Microsoft.AspNetCore.Mvc
{
    public class ControllerExtensionsTests
    {
        public class ViewExistsMethod
        {
            public class ControllerAndViewNameOverload
            {
                [Fact]
                public void NullController_Throws()
                {
                    Assert.Throws<ArgumentNullException>("controller", () =>
                    {
                        ControllerExtensions.ViewExists(
                            controller: null!,
                            viewName: "ViewName");
                    });
                }

                [Fact]
                public void NullViewName_Throws()
                {
                    Assert.Throws<ArgumentNullException>("viewName", () =>
                    {
                        ControllerExtensions.ViewExists(
                            controller: Controller(),
                            viewName: null!);
                    });
                }

                [Fact]
                public void EmptyViewName_Throws()
                {
                    Assert.Throws<ArgumentException>("viewName", () =>
                    {
                        ControllerExtensions.ViewExists(
                            controller: Controller(),
                            viewName: "");
                    });
                }

                [Fact]
                public void WhiteSpaceViewName_Throws()
                {
                    Assert.Throws<ArgumentException>("viewName", () =>
                    {
                        ControllerExtensions.ViewExists(
                            controller: Controller(),
                            viewName: " ");
                    });
                }

                [Fact]
                public void WhenCalled_CallsFindViewOnCompositeViewEngine()
                {
                    var viewEngine = ViewEngine();
                    var controller = Controller(viewEngine);

                    controller.ViewExists("TestViewName");

                    Assert.Equal(controller.ControllerContext,
                        viewEngine.FindView_InputContext);
                    Assert.Equal("TestViewName", viewEngine.FindView_InputViewName);
                    Assert.True(viewEngine.FindView_IsMainPage);
                }

                [Fact]
                public void FindViewOnCompositeViewEngineNotFindsView_ReturnsFalse()
                {
                    var viewEngine = ViewEngine();
                    viewEngine.FindView_Output = ViewEngineResult.NotFound("TestViewName",
                        new string[0]);
                    var controller = Controller(viewEngine);

                    var result = controller.ViewExists("TestViewName");

                    Assert.False(result);
                }

                [Fact]
                public void FindViewOnCompositeViewEngineFindsView_ReturnsTrue()
                {
                    var viewEngine = ViewEngine();
                    var controller = Controller(viewEngine);

                    var result = controller.ViewExists("TestViewName");

                    Assert.True(result);
                }
            }
        }

        private static FakeCompositeViewEngine ViewEngine() => new FakeCompositeViewEngine();
        private static FakeController Controller(FakeCompositeViewEngine? viewEngine = null)
        {
            viewEngine ??= new FakeCompositeViewEngine();

            var services = new ServiceCollection();
            services.AddSingleton<ICompositeViewEngine>(viewEngine);

            return new FakeController(services);
        }
    }
}