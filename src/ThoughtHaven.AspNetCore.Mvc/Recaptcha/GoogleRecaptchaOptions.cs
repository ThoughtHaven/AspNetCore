namespace ThoughtHaven.AspNetCore.Mvc.Recaptcha
{
    public class GoogleRecaptchaOptions
    {
        public GoogleRecaptchaKeys V2CheckboxKeys { get; }
        public GoogleRecaptchaKeys V2InvisibleKeys { get; }
        public GoogleRecaptchaKeys V3Keys { get; }

        public GoogleRecaptchaOptions(GoogleRecaptchaKeys v2CheckboxKeys,
            GoogleRecaptchaKeys v2InvisibleKeys, GoogleRecaptchaKeys v3Keys)
        {
            this.V2CheckboxKeys = Guard.Null(nameof(v2CheckboxKeys), v2CheckboxKeys);
            this.V2InvisibleKeys = Guard.Null(nameof(v2InvisibleKeys), v2InvisibleKeys);
            this.V3Keys = Guard.Null(nameof(v3Keys), v3Keys);
        }
    }
}