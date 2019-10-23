using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ThoughtHaven.AspNetCore.Mvc.Recaptcha
{
    [HtmlTargetElement("div", Attributes = AttributeName)]
    public class GoogleRecaptchaV2CheckboxTagHelper : TagHelper
    {
        private const string AttributeName = "google-recaptcha-v2checkbox";

        protected GoogleRecaptchaOptions Options { get; }

        [HtmlAttributeName(AttributeName)]
        public bool GoogleRecaptchaV2Checkbox { get; set; }

        public GoogleRecaptchaV2CheckboxTagHelper(GoogleRecaptchaOptions options)
        {
            this.Options = Guard.Null(nameof(options), options);
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Guard.Null(nameof(context), context);
            Guard.Null(nameof(output), output);

            if (this.GoogleRecaptchaV2Checkbox)
            {
                output.TagName = null;

                output.PostContent.AppendHtml(
                    $@"<script src=""https://www.google.com/recaptcha/api.js"" async defer></script>");
                output.PostContent.AppendHtml(
                    $@"<div class=""g-recaptcha"" data-sitekey=""{this.Options.V2CheckboxKeys.SiteKey}""></div>");
            }
        }
    }
}