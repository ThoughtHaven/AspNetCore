using Microsoft.AspNetCore.Builder;
using System;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc.Startup
{
    public class MvcBuilderOptionsTests
    {
        public class StatusCodePagePathFormatProperty
        {
            [Fact]
            public void SetToNull_Throws()
            {
                var options = Options();

                Assert.Throws<ArgumentNullException>("value", () =>
                {
                    options.StatusCodePagePathFormat = null;
                });
            }

            [Fact]
            public void SetToEmpty_Throws()
            {
                var options = Options();

                Assert.Throws<ArgumentException>("value", () =>
                {
                    options.StatusCodePagePathFormat = "";
                });
            }

            [Fact]
            public void SetToWhiteSpace_Throws()
            {
                var options = Options();

                Assert.Throws<ArgumentException>("value", () =>
                {
                    options.StatusCodePagePathFormat = " ";
                });
            }

            [Fact]
            public void Set_SetsValue()
            {
                var options = Options();

                options.StatusCodePagePathFormat = "/format";

                Assert.Equal("/format", options.StatusCodePagePathFormat);
            }
        }

        public class Constructor
        {
            public class EmptyOverload
            {
                [Fact]
                public void WhenCalled_SetsDeveloperExceptionPage()
                {
                    var options = Options();

                    Assert.NotNull(options.DeveloperExceptionPage);
                }

                [Fact]
                public void WhenCalled_SetsExceptionHandler()
                {
                    var options = Options();

                    Assert.NotNull(options.ExceptionHandler);
                    Assert.Equal("/errors/server",
                        options.ExceptionHandler.ExceptionHandlingPath);
                }

                [Fact]
                public void WhenCalled_SetsStatusCodePagePathFormat()
                {
                    var options = Options();

                    Assert.NotNull(options.StatusCodePagePathFormat);
                    Assert.Equal("/errors/statuscode/{0}", options.StatusCodePagePathFormat);
                }

                [Fact]
                public void WhenCalled_SetsStaticFiles()
                {
                    var options = Options();

                    Assert.NotNull(options.StaticFiles);
                    Assert.NotNull(options.StaticFiles.OnPrepareResponse);
                }

                [Fact]
                public void WhenCalled_SetsRewrite()
                {
                    var options = Options();

                    Assert.NotNull(options.Rewrite);
                }
            }
        }

        private static MvcBuilderOptions Options() => new MvcBuilderOptions();
    }
}