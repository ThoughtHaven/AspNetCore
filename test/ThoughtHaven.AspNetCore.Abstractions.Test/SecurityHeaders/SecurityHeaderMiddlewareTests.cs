using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ThoughtHaven.AspNetCore.SecurityHeaders
{
    public class SecurityHeaderMiddlewareTests
    {
        public class Constructor
        {
            public class NextAndOptionsOverload
            {
                [Fact]
                public void NullNext_Throws()
                {
                    Assert.Throws<ArgumentNullException>("next", () =>
                    {
                        new SecurityHeaderMiddleware(next: null!, options: Options());
                    });
                }

                [Fact]
                public void NullOptions_Throws()
                {
                    Assert.Throws<ArgumentNullException>("options", () =>
                    {
                        new SecurityHeaderMiddleware(next: Next(), options: null!);
                    });
                }
            }
        }

        public class InvokeMethod
        {
            public class ContextOverload
            {
                [Fact]
                public async Task NullContext_Throws()
                {
                    await Assert.ThrowsAsync<ArgumentNullException>("context", async () =>
                    {
                        await Middleware().Invoke(context: null!);
                    });
                }

                [Fact]
                public async Task ContentSecurityPolicyNull_DoesNotSetContentSecurityPolicy()
                {
                    var options = Options();
                    options.ContentSecurityPolicy = null;
                    var context = Context();

                    await Middleware(options).Invoke(context);

                    Assert.False(context.Response.Headers
                        .ContainsKey("content-security-policy"));
                }

                [Fact]
                public async Task WhenCalled_SetsContentSecurityPolicy()
                {
                    var options = Options();
                    var context = Context();

                    await Middleware(options).Invoke(context);

                    Assert.Equal(options.ContentSecurityPolicy,
                        context.Response.Headers["content-security-policy"]);
                }

                [Fact]
                public async Task XxsProtectionNull_DoesNotSetXxsProtection()
                {
                    var options = Options();
                    options.XxsProtection = null;
                    var context = Context();

                    await Middleware(options).Invoke(context);

                    Assert.False(context.Response.Headers.ContainsKey("x-xss-protection"));
                }

                [Fact]
                public async Task WhenCalled_SetsXxsProtection()
                {
                    var options = Options();
                    var context = Context();

                    await Middleware(options).Invoke(context);

                    Assert.Equal(options.XxsProtection,
                        context.Response.Headers["x-xss-protection"]);
                }

                [Fact]
                public async Task XFrameOptionsNull_DoesNotSetXFrameOptions()
                {
                    var options = Options();
                    options.XFrameOptions = null;
                    var context = Context();

                    await Middleware(options).Invoke(context);

                    Assert.False(context.Response.Headers.ContainsKey("x-frame-options"));
                }

                [Fact]
                public async Task WhenCalled_SetsXFrameOptions()
                {
                    var options = Options();
                    var context = Context();

                    await Middleware(options).Invoke(context);

                    Assert.Equal(options.XFrameOptions,
                        context.Response.Headers["x-frame-options"]);
                }

                [Fact]
                public async Task XContentTypeOptionsNull_DoesNotSetXContentTypeOptions()
                {
                    var options = Options();
                    options.XContentTypeOptions = null;
                    var context = Context();

                    await Middleware(options).Invoke(context);

                    Assert.False(context.Response.Headers
                        .ContainsKey("x-content-type-options"));
                }

                [Fact]
                public async Task WhenCalled_SetsXContentTypeOptions()
                {
                    var options = Options();
                    var context = Context();

                    await Middleware(options).Invoke(context);

                    Assert.Equal(options.XContentTypeOptions,
                        context.Response.Headers["x-content-type-options"]);
                }

                [Fact]
                public async Task ReferrerPolicyNull_DoesNotSetReferrerPolicy()
                {
                    var options = Options();
                    options.ReferrerPolicy = null;
                    var context = Context();

                    await Middleware(options).Invoke(context);

                    Assert.False(context.Response.Headers.ContainsKey("Referrer-Policy"));
                }

                [Fact]
                public async Task WhenCalled_SetsReferrerPolicy()
                {
                    var options = Options();
                    var context = Context();

                    await Middleware(options).Invoke(context);

                    Assert.Equal(options.ReferrerPolicy,
                        context.Response.Headers["Referrer-Policy"]);
                }

                [Fact]
                public async Task ExpectCTNull_DoesNotSetExpectCT()
                {
                    var options = Options();
                    options.ExpectCT = null;
                    var context = Context();

                    await Middleware(options).Invoke(context);

                    Assert.False(context.Response.Headers.ContainsKey("Expect-CT"));
                }

                [Fact]
                public async Task WhenCalled_SetsExpectCT()
                {
                    var options = Options();
                    var context = Context();

                    await Middleware(options).Invoke(context);

                    Assert.Equal(options.ExpectCT,
                        context.Response.Headers["Expect-CT"]);
                }

                [Fact]
                public async Task FeaturePolicyNull_DoesNotSetFeaturePolicy()
                {
                    var options = Options();
                    options.FeaturePolicy = null;
                    var context = Context();

                    await Middleware(options).Invoke(context);

                    Assert.False(context.Response.Headers.ContainsKey("Feature-Policy"));
                }

                [Fact]
                public async Task WhenCalled_SetsFeaturePolicy()
                {
                    var options = Options();
                    var context = Context();

                    await Middleware(options).Invoke(context);

                    Assert.Equal(options.FeaturePolicy,
                        context.Response.Headers["Feature-Policy"]);
                }

                [Fact]
                public async Task WhenCalled_CallsNext()
                {
                    var wrapper = Wrapper();
                    var context = Context();

                    await Middleware(wrapper.Next).Invoke(context);

                    Assert.True(wrapper.NextCalled);
                    Assert.Equal(context, wrapper.Context);
                }
            }
        }

        private static RequestDelegate Next() => context => Task.CompletedTask;
        private static SecurityHeaderOptions Options() => new SecurityHeaderOptions();
        private static SecurityHeaderMiddleware Middleware() => Middleware(Options());
        private static SecurityHeaderMiddleware Middleware(SecurityHeaderOptions options) =>
            new SecurityHeaderMiddleware(Next(), options);
        private static SecurityHeaderMiddleware Middleware(RequestDelegate next) =>
            new SecurityHeaderMiddleware(next, Options());
        private static DefaultHttpContext Context() => new DefaultHttpContext();
        private static RequestDelegateWrapper Wrapper() => new RequestDelegateWrapper();

        private class RequestDelegateWrapper
        {
            public bool NextCalled { get; private set; } = false;
            public HttpContext? Context { get; private set; } = null;
            public RequestDelegate Next { get; }

            public RequestDelegateWrapper()
            {
                this.Next = context =>
                {
                    this.NextCalled = true;
                    this.Context = context;

                    return Task.CompletedTask;
                };
            }
        }
    }
}