//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.StaticFiles;
//using System;
//using Xunit;
//using static Microsoft.Net.Http.Headers.HeaderNames;

//namespace Microsoft.AspNetCore.Builder
//{
//    public class StaticFilePostConfigureOptionsTests
//    {
//        public class PostConfigureMethod
//        {
//            public class NameAndOptionsOverload
//            {
//                [Fact]
//                public void NullOptions_Throws()
//                {
//                    Assert.Throws<ArgumentNullException>("options", () =>
//                    {
//                        ConfigureOptions().PostConfigure(name: null!, options: null!);
//                    });
//                }

//                [Fact]
//                public void WhenCalled_AddsCacheControl()
//                {
//                    var options = Options();
//                    var configureOptions = ConfigureOptions();
//                    var responseContext = ResponseContext();

//                    configureOptions.PostConfigure(name: null!, options);

//                    options.OnPrepareResponse(responseContext);

//                    Assert.Equal("public,max-age=31536000",
//                        responseContext.Context.Response.Headers[CacheControl]);
//                }

//                [Fact]
//                public void OnPrepareResponseExists_PreservesAction()
//                {
//                    var onPrepareResponseCalled = false;
//                    void onPrepareResponse(StaticFileResponseContext context)
//                    {
//                        onPrepareResponseCalled = true;
//                    }
//                    var options = Options();
//                    options.OnPrepareResponse = onPrepareResponse;
//                    var configureOptions = ConfigureOptions();
//                    var responseContext = ResponseContext();

//                    configureOptions.PostConfigure(name: null!, options);

//                    options.OnPrepareResponse(responseContext);

//                    Assert.True(onPrepareResponseCalled);
//                    Assert.Equal("public,max-age=31536000",
//                        responseContext.Context.Response.Headers[CacheControl]);
//                }
//            }
//        }

//        private static StaticFileOptions Options() => new StaticFileOptions();
//        private static StaticFilePostConfigureOptions ConfigureOptions() =>
//            new StaticFilePostConfigureOptions();
//        private static StaticFileResponseContext ResponseContext()
//        {
//            var context = new StaticFileResponseContext();

//            var type = context.GetType();
//            var property = type.GetProperty(nameof(context.Context));

//            var httpContext = new DefaultHttpContext();

//            property.SetValue(context, httpContext, null);

//            return context;
//        }
//    }
//}