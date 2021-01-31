using System.Linq;
using Xunit;

namespace ThoughtHaven.AspNetCore.SecurityHeaders
{
    public partial class ContentSecurityPolicyBuilderTests
    {
        public class ChildProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_Empty()
                {
                    var builder = new ContentSecurityPolicyBuilder();

                    Assert.Empty(builder.Child);
                }
            }
        }

        public class ConnectProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_Empty()
                {
                    var builder = new ContentSecurityPolicyBuilder();

                    Assert.Empty(builder.Connect);
                }
            }
        }

        public class DefaultProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_ReturnsSelf()
                {
                    var builder = new ContentSecurityPolicyBuilder();

                    Assert.Single(builder.Default);
                    Assert.Equal("'self'", builder.Default.First());
                }
            }
        }

        public class FontProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_Empty()
                {
                    var builder = new ContentSecurityPolicyBuilder();

                    Assert.Empty(builder.Font);
                }
            }
        }

        public class FrameProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_Empty()
                {
                    var builder = new ContentSecurityPolicyBuilder();

                    Assert.Empty(builder.Frame);
                }
            }
        }

        public class ImageProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_Empty()
                {
                    var builder = new ContentSecurityPolicyBuilder();

                    Assert.Empty(builder.Image);
                }
            }
        }

        public class ManifestProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_Empty()
                {
                    var builder = new ContentSecurityPolicyBuilder();

                    Assert.Empty(builder.Manifest);
                }
            }
        }

        public class MediaProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_Empty()
                {
                    var builder = new ContentSecurityPolicyBuilder();

                    Assert.Empty(builder.Media);
                }
            }
        }

        public class ObjectProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_Empty()
                {
                    var builder = new ContentSecurityPolicyBuilder();

                    Assert.Single(builder.Object);
                    Assert.Equal("'none'", builder.Object.First());
                }
            }
        }

        public class PrefetchProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_Empty()
                {
                    var builder = new ContentSecurityPolicyBuilder();

                    Assert.Empty(builder.Prefetch);
                }
            }
        }

        public class ScriptProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_Empty()
                {
                    var builder = new ContentSecurityPolicyBuilder();

                    Assert.Empty(builder.Script);
                }
            }
        }

        public class ScriptElemProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_Empty()
                {
                    var builder = new ContentSecurityPolicyBuilder();

                    Assert.Empty(builder.ScriptElem);
                }
            }
        }

        public class ScriptAttrProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_Empty()
                {
                    var builder = new ContentSecurityPolicyBuilder();

                    Assert.Empty(builder.ScriptAttr);
                }
            }
        }

        public class StyleProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_Empty()
                {
                    var builder = new ContentSecurityPolicyBuilder();

                    Assert.Empty(builder.Style);
                }
            }
        }

        public class StyleElemProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_Empty()
                {
                    var builder = new ContentSecurityPolicyBuilder();

                    Assert.Empty(builder.StyleElem);
                }
            }
        }

        public class StyleAttrProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_Empty()
                {
                    var builder = new ContentSecurityPolicyBuilder();

                    Assert.Empty(builder.StyleAttr);
                }
            }
        }

        public class WorkerProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_Empty()
                {
                    var builder = new ContentSecurityPolicyBuilder();

                    Assert.Empty(builder.Worker);
                }
            }
        }

        public class ToStringMethod
        {
            public class EmptyOverload
            {
                [Fact]
                public void DefaultValue_ReturnsValue()
                {
                    var value = new ContentSecurityPolicyBuilder().ToString();

                    Assert.Equal("default-src 'self'; object-src 'none'", value);
                }

                [Fact]
                public void DefaultSrcOnly_ReturnsValue()
                {
                    var builder = Builder();
                    builder.Default.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("default-src https://example.com", value);
                }

                [Fact]
                public void DefaultSrcMultipleValues_ReturnsValue()
                {
                    var builder = Builder();
                    builder.Default.Add("https://1.example.com");
                    builder.Default.Add("https://2.example.com");

                    var value = builder.ToString();

                    Assert.Equal("default-src https://1.example.com https://2.example.com", value);
                }

                [Fact]
                public void ChildSrcOnly_ReturnsValue()
                {
                    var builder = Builder();
                    builder.Child.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("child-src https://example.com", value);
                }

                [Fact]
                public void ConnectSrcOnly_ReturnsValue()
                {
                    var builder = Builder();
                    builder.Connect.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("connect-src https://example.com", value);
                }

                [Fact]
                public void FontSrcOnly_ReturnsValue()
                {
                    var builder = Builder();
                    builder.Font.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("font-src https://example.com", value);
                }

                [Fact]
                public void FrameSrcOnly_ReturnsValue()
                {
                    var builder = Builder();
                    builder.Frame.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("frame-src https://example.com", value);
                }

                [Fact]
                public void ImageSrcOnly_ReturnsValue()
                {
                    var builder = Builder();
                    builder.Image.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("img-src https://example.com", value);
                }

                [Fact]
                public void ManifestSrcOnly_ReturnsValue()
                {
                    var builder = Builder();
                    builder.Manifest.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("manifest-src https://example.com", value);
                }

                [Fact]
                public void MediaSrcOnly_ReturnsValue()
                {
                    var builder = Builder();
                    builder.Media.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("media-src https://example.com", value);
                }

                [Fact]
                public void ObjectSrcOnly_ReturnsValue()
                {
                    var builder = Builder();
                    builder.Object.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("object-src https://example.com", value);
                }

                [Fact]
                public void PrefetchSrcOnly_ReturnsValue()
                {
                    var builder = Builder();
                    builder.Prefetch.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("prefetch-src https://example.com", value);
                }

                [Fact]
                public void ScriptSrcOnly_ReturnsValue()
                {
                    var builder = Builder();
                    builder.Script.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("script-src https://example.com", value);
                }

                [Fact]
                public void ScriptSrcElemOnly_ReturnsValue()
                {
                    var builder = Builder();
                    builder.ScriptElem.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("script-src-elem https://example.com", value);
                }

                [Fact]
                public void ScriptSrcAttrOnly_ReturnsValue()
                {
                    var builder = Builder();
                    builder.ScriptAttr.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("script-src-attr https://example.com", value);
                }

                [Fact]
                public void StyleSrcOnly_ReturnsValue()
                {
                    var builder = Builder();
                    builder.Style.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("style-src https://example.com", value);
                }

                [Fact]
                public void StyleSrcElemOnly_ReturnsValue()
                {
                    var builder = Builder();
                    builder.StyleElem.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("style-src-elem https://example.com", value);
                }

                [Fact]
                public void StyleSrcAttrOnly_ReturnsValue()
                {
                    var builder = Builder();
                    builder.StyleAttr.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("style-src-attr https://example.com", value);
                }

                [Fact]
                public void WorkerSrcOnly_ReturnsValue()
                {
                    var builder = Builder();
                    builder.Worker.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("worker-src https://example.com", value);
                }

                [Fact]
                public void AllSrcs_ReturnsValue()
                {
                    var builder = Builder();
                    builder.Default.Add("https://1.example.com");
                    builder.Default.Add("https://2.example.com");
                    builder.Child.Add("https://example.com");
                    builder.Connect.Add("https://example.com");
                    builder.Font.Add("https://example.com");
                    builder.Frame.Add("https://example.com");
                    builder.Image.Add("https://example.com");
                    builder.Manifest.Add("https://example.com");
                    builder.Media.Add("https://example.com");
                    builder.Object.Add("https://example.com");
                    builder.Prefetch.Add("https://example.com");
                    builder.Script.Add("https://example.com");
                    builder.ScriptElem.Add("https://example.com");
                    builder.ScriptAttr.Add("https://example.com");
                    builder.Style.Add("https://example.com");
                    builder.StyleElem.Add("https://example.com");
                    builder.StyleAttr.Add("https://example.com");
                    builder.Worker.Add("https://example.com");

                    var value = builder.ToString();

                    Assert.Equal("default-src https://1.example.com https://2.example.com; child-src https://example.com; connect-src https://example.com; font-src https://example.com; frame-src https://example.com; img-src https://example.com; manifest-src https://example.com; media-src https://example.com; object-src https://example.com; prefetch-src https://example.com; script-src https://example.com; script-src-elem https://example.com; script-src-attr https://example.com; style-src https://example.com; style-src-elem https://example.com; style-src-attr https://example.com; worker-src https://example.com",
                        value);
                }
            }
        }

        private static ContentSecurityPolicyBuilder Builder(bool clear = true)
        {
            var builder = new ContentSecurityPolicyBuilder();

            if (clear)
            {
                builder.Default.Clear();
                builder.Object.Clear();
            }

            return builder;
        }
    }
}