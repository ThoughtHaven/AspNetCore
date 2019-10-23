using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System;
using System.Linq;
using ThoughtHaven.AspNetCore.Mvc.Fakes;
using ThoughtHaven.AspNetCore.Mvc.Ui.Test.EmbeddedFiles;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc.Ui
{
    public class UiPostConfigureStaticFileOptionsTests
    {
        public class Constructor
        {
            public class EnvironmentOverload
            {
                [Fact]
                public void NullEnvironment_Throws()
                {
                    Assert.Throws<ArgumentNullException>("environment", () =>
                    {
                        new UiPostConfigureStaticFileOptions<FakeWebHostEnvironment>(
                            environment: null!);
                    });
                }
            }
        }

        public class PostConfigureMethod
        {
            public class NameAndOptionsOverload
            {
                [Fact]
                public void NullOptions_Throws()
                {
                    var options = Options();

                    Assert.Throws<ArgumentNullException>("options", () =>
                    {
                        options.PostConfigure(name: null!, options: null!);
                    });
                }

                [Fact]
                public void NullOptionsFileProviderAndNullEnvironmentWebRootFileProvider_Throws()
                {
                    var environment = Environment();
                    environment.WebRootFileProvider = null!;
                    var options = Options(environment);
                    var staticFiles = new StaticFileOptions
                    {
                        FileProvider = null
                    };

                    var exception = Assert.Throws<InvalidOperationException>(() =>
                    {
                        options.PostConfigure(name: null!, staticFiles);
                    });

                    Assert.Equal("Missing FileProvider.", exception.Message);
                }

                [Fact]
                public void WhenCalled_SetsFileProvider()
                {
                    var options = Options();
                    var originalFileProvider = new NullFileProvider();
                    var staticFiles = new StaticFileOptions()
                    {
                        FileProvider = originalFileProvider
                    };

                    options.PostConfigure(name: null!, staticFiles);

                    var composite = Assert.IsType<CompositeFileProvider>(
                        staticFiles.FileProvider);
                    Assert.Equal(2, composite.FileProviders.Count());
                    Assert.Equal(originalFileProvider, composite.FileProviders.ElementAt(0));
                    Assert.IsType<ManifestEmbeddedFileProvider>(
                        composite.FileProviders.ElementAt(1));
                }
            }
        }

        private static FakeWebHostEnvironment Environment() => new FakeWebHostEnvironment();
        private static UiPostConfigureStaticFileOptions<TestEmbeddedUi> Options(
            FakeWebHostEnvironment? environment = null) =>
            new UiPostConfigureStaticFileOptions<TestEmbeddedUi>(
                environment ?? Environment());
    }
}