using Microsoft.AspNetCore.Mvc;

namespace ThoughtHaven.AspNetCore.Mvc
{
    public class NeverResponseCacheAttribute : ResponseCacheAttribute
    {
        public NeverResponseCacheAttribute()
        {
            this.Location = ResponseCacheLocation.None;
            this.NoStore = true;
        }
    }
}