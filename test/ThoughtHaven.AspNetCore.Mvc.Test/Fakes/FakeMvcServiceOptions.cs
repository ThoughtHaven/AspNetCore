using Microsoft.Extensions.DependencyInjection;

namespace ThoughtHaven.AspNetCore.Mvc.Fakes
{
    public class FakeMvcServiceOptions : MvcServiceOptions
    {
        public bool Mvc_Called;
        public bool Json_Called;
        public bool Razor_Called;
        public bool Views_Called;
        //public bool Antiforgery_Called;
        public bool Routing_Called;
        //public bool CookiePolicy_Called;

        public FakeMvcServiceOptions()
        {
            this.Mvc = o => { this.Mvc_Called = true; };
            this.Json = o => { this.Json_Called = true; };
            this.Razor = o => { this.Razor_Called = true; };
            this.Views = o => { this.Views_Called = true; };
            this.Routing = o => { this.Routing_Called = true; };
        }
    }
}