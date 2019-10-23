using System;
using ThoughtHaven.AspNetCore.Fakes;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc
{
    public class MvcRouteHelpersTests
    {
        public class ControllerMethod
        {
            public class EmptyOverload
            {
                [Fact]
                public void TNameEndsInController_RemovesTheWordController()
                {
                    var result = MvcRouteHelpers.Controller<FakeController>();

                    Assert.Equal("Fake", result);
                }

                [Fact]
                public void TNameDoesNotEndInController_ReturnsName()
                {
                    var result = MvcRouteHelpers.Controller<MvcRouteHelpersTests>();

                    Assert.Equal("MvcRouteHelpersTests", result);
                }
            }
        }

        public class DefaultsMethod
        {
            public class ActionOverload
            {
                [Fact]
                public void NullAction_Throws()
                {
                    Assert.Throws<ArgumentNullException>("action", () =>
                    {
                        MvcRouteHelpers.Defaults<FakeController>(action: null!);
                    });
                }

                [Fact]
                public void EmptyAction_Throws()
                {
                    Assert.Throws<ArgumentException>("action", () =>
                    {
                        MvcRouteHelpers.Defaults<FakeController>(action: "");
                    });
                }

                [Fact]
                public void WhiteSpaceAction_Throws()
                {
                    Assert.Throws<ArgumentException>("action", () =>
                    {
                        MvcRouteHelpers.Defaults<FakeController>(action: " ");
                    });
                }

                [Theory]
                [InlineData("Index")]
                [InlineData("Home")]
                public void WhenCalled_ReturnsObject(string action)
                {
                    var result = MvcRouteHelpers.Defaults<FakeController>(action);

                    Assert.Equal("Fake",
                        result.GetType().GetProperty("controller")!.GetValue(result, null));
                    Assert.Equal(action,
                        result.GetType().GetProperty("action")!.GetValue(result, null));
                }
            }
        }
    }
}