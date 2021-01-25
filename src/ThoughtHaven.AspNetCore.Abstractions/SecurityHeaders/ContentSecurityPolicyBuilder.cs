using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThoughtHaven.AspNetCore.SecurityHeaders
{
    public class ContentSecurityPolicyBuilder
    {
        public IList<string> Child { get; } = new List<string>();
        public IList<string> Connect { get; } = new List<string>();
        public IList<string> Default { get; } = new List<string>()
        {
            "'self'",
        };
        public IList<string> Font { get; } = new List<string>();
        public IList<string> Frame { get; } = new List<string>();
        public IList<string> Image { get; } = new List<string>();
        public IList<string> Manifest { get; } = new List<string>();
        public IList<string> Media { get; } = new List<string>();
        public IList<string> Object { get; } = new List<string>()
        {
            "'none'",
        };
        public IList<string> Prefetch { get; } = new List<string>();
        public IList<string> Script { get; } = new List<string>();
        public IList<string> ScriptElem { get; } = new List<string>();
        public IList<string> ScriptAttr { get; } = new List<string>();
        public IList<string> Style { get; } = new List<string>();
        public IList<string> StyleElem { get; } = new List<string>();
        public IList<string> StyleAttr { get; } = new List<string>();
        public IList<string> Worker { get; } = new List<string>();

        public override string ToString()
        {
            var result = new StringBuilder();

            if (this.Default.Any())
            {
                result.Append(WriteList("default-src", this.Default));
            }

            if (this.Child.Any())
            {
                result.Append(WriteList("child-src", this.Child));
            }

            if (this.Connect.Any())
            {
                result.Append(WriteList("connect-src", this.Connect));
            }

            if (this.Font.Any())
            {
                result.Append(WriteList("font-src", this.Font));
            }

            if (this.Frame.Any())
            {
                result.Append(WriteList("frame-src", this.Frame));
            }

            if (this.Image.Any())
            {
                result.Append(WriteList("img-src", this.Image));
            }

            if (this.Manifest.Any())
            {
                result.Append(WriteList("manifest-src", this.Manifest));
            }

            if (this.Media.Any())
            {
                result.Append(WriteList("media-src", this.Media));
            }

            if (this.Object.Any())
            {
                result.Append(WriteList("object-src", this.Object));
            }

            if (this.Prefetch.Any())
            {
                result.Append(WriteList("prefetch-src", this.Prefetch));
            }

            if (this.Script.Any())
            {
                result.Append(WriteList("script-src", this.Script));
            }

            if (this.ScriptElem.Any())
            {
                result.Append(WriteList("script-src-elem", this.ScriptElem));
            }

            if (this.ScriptAttr.Any())
            {
                result.Append(WriteList("script-src-attr", this.ScriptAttr));
            }

            if (this.Style.Any())
            {
                result.Append(WriteList("style-src", this.Style));
            }

            if (this.StyleElem.Any())
            {
                result.Append(WriteList("style-src-elem", this.StyleElem));
            }

            if (this.StyleAttr.Any())
            {
                result.Append(WriteList("style-src-attr", this.StyleAttr));
            }

            if (this.Worker.Any())
            {
                result.Append(WriteList("worker-src", this.Worker));
            }

            return result.ToString().Trim(';').Trim();
        }

        private static string WriteList(string src, IList<string> list)
        {
            var result = new StringBuilder($"; {src}");

            foreach (var item in list)
            {
                result.Append($" {item}");
            }

            return result.ToString();
        }
    }
}