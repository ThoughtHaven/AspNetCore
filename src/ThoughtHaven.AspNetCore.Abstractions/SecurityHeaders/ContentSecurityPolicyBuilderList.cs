using System.Collections.Generic;

namespace ThoughtHaven.AspNetCore.SecurityHeaders
{
    public partial class ContentSecurityPolicyBuilder
    {
        public class List : List<string>
        {
            public void AddSelf() => this.Add("'self'");
        }

        public class ImageList : List
        {
            public void AddData() => this.Add("data:");
        }
    }
}