using Microsoft.AspNetCore.Mvc;

namespace ThoughtHaven.AspNetCore.Mvc.Recaptcha
{
    public class GoogleRecaptchaVerifyModel
    {
        [FromForm(Name = "g-recaptcha-response")]
        public virtual string? ResponseToken { get; set; }
    }
}