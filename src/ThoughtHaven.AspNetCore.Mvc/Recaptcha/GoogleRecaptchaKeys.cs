namespace ThoughtHaven.AspNetCore.Mvc.Recaptcha
{
    public class GoogleRecaptchaKeys
    {
        public string SiteKey { get; }
        public string SecretKey { get; }

        public GoogleRecaptchaKeys(string siteKey, string secretKey)
        {
            this.SiteKey = Guard.NullOrWhiteSpace(nameof(siteKey), siteKey);
            this.SecretKey = Guard.NullOrWhiteSpace(nameof(secretKey), secretKey);
        }
    }
}