using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ThoughtHaven.AspNetCore.Mvc.Recaptcha
{
    [HtmlTargetElement("form", Attributes = AttributeName)]
    public class GoogleRecaptchaV3TagHelper : TagHelper
    {
        private const string AttributeName = "google-recaptcha-v3";

        protected GoogleRecaptchaOptions Options { get; }

        [HtmlAttributeName(AttributeName)]
        public bool GoogleRecaptchaV3 { get; set; }

        public string? RecaptchaAction { get; set; } = null;

        public GoogleRecaptchaV3TagHelper(GoogleRecaptchaOptions options)
        {
            this.Options = Guard.Null(nameof(options), options);
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Guard.Null(nameof(context), context);
            Guard.Null(nameof(output), output);

            if (this.GoogleRecaptchaV3)
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
<script src='https://www.google.com/recaptcha/api.js?render={this.Options.V3Keys.SiteKey}'></script>
<script>
grecaptcha.ready(function () {{
    document.getElementById('{formId}').addEventListener('submit', function (event) {{
        event.preventDefault();

        grecaptcha.execute('{Options.V3Keys.SiteKey}'{(!string.IsNullOrWhiteSpace(this.RecaptchaAction) ? $@", {{ action: '{this.RecaptchaAction}' }}" : string.Empty)})
            .then(function (token) {{
                var input = document.createElement('input');
                input.setAttribute('type', 'hidden');
                input.setAttribute('name', 'g-recaptcha-response');
                input.setAttribute('value', token);

                var form = document.getElementById('{formId}');
                form.appendChild(input);
                form.submit();
            }});
    }});
}});
</script>
";
    }
}