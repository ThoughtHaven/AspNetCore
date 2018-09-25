using Microsoft.AspNetCore.Mvc;

namespace ThoughtHaven.AspNetCore.Mvc
{
    public class DefaultResponseCacheAttribute : ResponseCacheAttribute
    {
        public DefaultResponseCacheAttribute()
        {
            this.Duration = 60 * 5;
            this.Location = ResponseCacheLocation.Any;
        }
    }
}