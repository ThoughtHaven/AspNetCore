using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System;
using ThoughtHaven.AspNetCore.Mvc.UI.Test.EmbeddedFiles;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc.UI
{
    public class UIStaticFileOptionsTests
    {
        public class Type
        {
            [Fact]
            public void InheritsStaticFileOptions()
            {
                Assert.True(typeof(StaticFileOptions).IsAssignableFrom(
                    typeof(UIStaticFileOptions)));
            }
        }

        public class Constructor
        {
            public class EmbeddedFileAssemblyOverload
            {
                [Fact]
                public void NullEmbeddedFileAssembly_Throws()
                {
                    Assert.Throws<ArgumentNullException>("embeddedFileAssembly", () =>
                    {
                        new UIStaticFileOptions(embeddedFileAssembly: null!);
                    });
                }

                [Fact]
                public void WhenCalled_SetsFileProvider()
                {
                    var options = new UIStaticFileOptions(typeof(TestEmbeddedUI).Assembly);

                    Assert.NotNull(options.FileProvider);
                    Assert.IsType<ManifestEmbeddedFileProvider>(options.FileProvider);
                }

                [Fact]
                public void WhenCalled_SetsOnPrepareResponse()
                {
                    var options = new UIStaticFileOptions(typeof(TestEmbeddedUI).Assembly);

                    Assert.NotNull(options.OnPrepareResponse);
                }
            }

            public class EmbeddedFileAssemblyAndRootOverload
            {
                [Fact]
                public void NullEmbeddedFileAssembly_Throws()
                {
                    Assert.Throws<ArgumentNullException>("embeddedFileAssembly", () =>
                    {
                        new UIStaticFileOptions(embeddedFileAssembly: null!,
                            root: "wwwroot");
                    });
                }

                [Fact]
                public void NullRoot_Throws()
                {
                    Assert.Throws<ArgumentNullException>("root", () =>
                    {
                        new UIStaticFileOptions(
                            embeddedFileAssembly: typeof(TestEmbeddedUI).Assembly,
                            root: null!);
                    });
                }

                [Fact]
                public void WhenCalled_SetsFileProvider()
                {
                    var options = new UIStaticFileOptions(typeof(TestEmbeddedUI).Assembly,
                        root: "wwwroot");

                    Assert.NotNull(options.FileProvider);
                    Assert.IsType<ManifestEmbeddedFileProvider>(options.FileProvider);
                }

                [Fact]
                public void WhenCalled_SetsOnPrepareResponse()
                {
                    var options = new UIStaticFileOptions(typeof(TestEmbeddedUI).Assembly,
                        root: "wwwroot");

                    Assert.NotNull(options.OnPrepareResponse);
                }
            }

            public class FileProviderOverload
            {
                [Fact]
                public void NullFileProvider_Throws()
                {
                    Assert.Throws<ArgumentNullException>("fileProvider", () =>
                    {
                        new UIStaticFileOptions(fileProvider: null!);
                    });
                }

                [Fact]
                public void WhenCalled_SetsFileProvider()
                {
                    var fileProvider = new ManifestEmbeddedFileProvider(
                        typeof(TestEmbeddedUI).Assembly);

                    var options = new UIStaticFileOptions(fileProvider);

                    Assert.Equal(fileProvider, options.FileProvider);
                }

                [Fact]
                public void WhenCalled_SetsOnPrepareResponse()
                {
                    var options = new UIStaticFileOptions(new ManifestEmbeddedFileProvider(
                        typeof(TestEmbeddedUI).Assembly));

                    Assert.NotNull(options.OnPrepareResponse);
                }
            }
        }
    }
}