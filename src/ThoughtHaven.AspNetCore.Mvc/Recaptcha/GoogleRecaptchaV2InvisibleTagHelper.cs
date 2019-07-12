using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ThoughtHaven.AspNetCore.Mvc.Recaptcha
{
    [HtmlTargetElement("form", Attributes = AttributeName)]
    public class GoogleRecaptchaV2InvisibleTagHelper : TagHelper
    {
        private const string AttributeName = "google-recaptcha-v2invisible";

        protected GoogleRecaptchaOptions Options { get; }

        [HtmlAttributeName(AttributeName)]
        public bool GoogleRecaptchaV2Invisible { get; set; }

        public GoogleRecaptchaV2InvisibleTagHelper(GoogleRecaptchaOptions options)
        {
            this.Options = Guard.Null(nameof(options), options);
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Guard.Null(nameof(context), context);
            Guard.Null(nameof(output), output);

            if (this.GoogleRecaptchaV2Invisible)
            {
                var idAttribute = output.Attributes["id"];
                var formId = idAttribute?.Value.ToString();

                if (string.IsNullOrWhiteSpace(formId))
                {
                    formId = $"form{context.UniqueId}";
                    output.Attributes.SetAttribute("id", formId);
                }

                output.PostContent.AppendHtml(this.CaptchaHtml(context, formId));
            }
        }

        private string CaptchaHtml(TagHelperContext context, string formId) =>
$@"
<script>
    function __googleRecaptchaExecute{context.UniqueId}(event) {{
        event.preventDefault();
        grecaptcha.execute();
    }}

    function __googleRecaptchaCallback{context.UniqueId}(token) {{
        document.getElementById('{formId}').submit();
    }}

    document.getElementById('{formId}').onsubmit = __googleRecaptchaExecute{context.UniqueId};
</script>
<script src='https://www.google.com/recaptcha/api.js' async defer></script>
<div class='g-recaptcha' data-sitekey='{this.Options.V2InvisibleKeys.SiteKey}'
        data-callback='__googleRecaptchaCallback{context.UniqueId}' data-size='invisible'></div>
";
    }
}