using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThoughtHaven.AspNetCore.SecurityHeaders
{
    public partial class ContentSecurityPolicyBuilder
    {
        public List Child { get; } = new List();
        public List Connect { get; } = new List();
        public List Default { get; } = new List()
        {
            "'self'",
        };
        public List Font { get; } = new List();
        public List Frame { get; } = new List();
        public ImageList Image { get; } = new ImageList();
        public List Manifest { get; } = new List();
        public List Media { get; } = new List();
        public List Object { get; } = new List()
        {
            "'none'",
        };
        public List Prefetch { get; } = new List();
        public List Script { get; } = new List();
        public List ScriptElem { get; } = new List();
        public List ScriptAttr { get; } = new List();
        public List Style { get; } = new List();
        public List StyleElem { get; } = new List();
        public List StyleAttr { get; } = new List();
        public List Worker { get; } = new List();

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