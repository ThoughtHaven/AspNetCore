using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc.Recaptcha
{
    public class GoogleRecaptchaV3TagHelperTests
    {
        public class Type
        {
            [Fact]
            public void InheritsTagHelper()
            {
                Assert.True(typeof(TagHelper).IsAssignableFrom(
                    typeof(GoogleRecaptchaV3TagHelper)));
            }

            [Fact]
            public void HasHtmlTargetElementAttribute()
            {
                var attribute = TestHelpers.GetClassAttribute<GoogleRecaptchaV3TagHelper, HtmlTargetElementAttribute>();

                Assert.Equal("form", attribute.Tag);
                Assert.Equal("google-recaptcha-v3", attribute.Attributes);
            }
        }

        public class GoogleRecaptchaV3TagHelperProperty
        {
            [Fact]
            public void HasHtmlAttributeNameAttribute()
            {
                var attribute = TestHelpers.GetPropertyAttribute<GoogleRecaptchaV3TagHelper, HtmlAttributeNameAttribute>(
                    "GoogleRecaptchaV3");

                Assert.Equal("google-recaptcha-v3", attribute.Name);
            }
        }

        public class RecaptchaActionProperty
        {
            public class GetAccessor
            {
                [Fact]
                public void DefaultValue_Null()
                {
                    Assert.Null(TagHelper().RecaptchaAction);
                }
            }
        }

        public class Constructor
        {
            public class OptionsOverload
            {
                [Fact]
                public void NullOptions_Throws()
                {
                    Assert.Throws<ArgumentNullException>("options", () =>
                    {
                        new GoogleRecaptchaV3TagHelper(options: null!);
                    });
                }
            }
        }

        public class ProcessMethod
        {
            public class ContextAndOutputOverload
            {
                [Fact]
                public void NullContext_Throws()
                {
                    Assert.Throws<ArgumentNullException>("context", () =>
                    {
                        TagHelper().Process(context: null!, Output());
                    });
                }

                [Fact]
                public void NullOutput_Throws()
                {
                    Assert.Throws<ArgumentNullException>("output", () =>
                    {
                        TagHelper().Process(Context(), output: null!);
                    });
                }

                [Fact]
                public void GoogleRecaptchaV3False_DoesNotAddId()
                {
                    var helper = TagHelper();
                    helper.GoogleRecaptchaV3 = false;
                    var context = Context();
                    var output = Output();

                    helper.Process(context, output);

                    Assert.False(output.Attributes.ContainsName("id"));
                }

                [Theory]
                [InlineData("id1")]
                [InlineData("id2")]
                public void OutputHasIdAttribute_DoesNotSetId(string id)
                {
                    var helper = TagHelper();
                    helper.GoogleRecaptchaV3 = true;
                    var context = Context();
                    var output = Output();
                    output.Attributes.SetAttribute("id", id);

                    helper.Process(context, output);

                    Assert.Equal(id, output.Attributes["id"].Value);
                }

                [Fact]
                public void OutputHasIdAttribute_SetsIdToFormUniqueId()
                {
                    var helper = TagHelper();
                    helper.GoogleRecaptchaV3 = true;
                    var context = Context();
                    var output = Output();

                    helper.Process(context, output);

                    Assert.Equal($"form{context.UniqueId}", output.Attributes["id"].Value);
                }

                [Theory]
                [InlineData("id1", null)]
                [InlineData("id2", "")]
                [InlineData("id3", " ")]
                [InlineData("id4", "action1")]
                [InlineData("id5", "action2")]
                public void WhenCalled_AppendsPostContentHtml(string id, string action)
                {
                    var options = Options();
                    var helper = TagHelper(options);
                    helper.GoogleRecaptchaV3 = true;
                    helper.RecaptchaAction = action;
                    var context = Context();
                    var output = Output();
                    output.Attributes.SetAttribute("id", id);

                    helper.Process(context, output);

                    Assert.Equal(CaptchaPostContentHtml(options, id, action),
                        output.PostContent.GetContent());
                }
            }
        }

        private static GoogleRecaptchaOptions Options() =>
            new GoogleRecaptchaOptions(
                new GoogleRecaptchaKeys("csite", "csecret"),
                new GoogleRecaptchaKeys("isite", "isecret"),
                new GoogleRecaptchaKeys("3site", "3secret"));
        private static GoogleRecaptchaV3TagHelper TagHelper(
            GoogleRecaptchaOptions? options = null) =>
            new GoogleRecaptchaV3TagHelper(options ?? Options());
        private static TagHelperContext Context() =>
            new TagHelperContext("div", new TagHelperAttributeList(),
                new Dictionary<object, object>(), Guid.NewGuid().ToString());
        private static TagHelperOutput Output() =>
            new TagHelperOutput("div", new TagHelperAttributeList(),
                (_, __) => null);
        private static string CaptchaPostContentHtml(GoogleRecaptchaOptions options,
            string formId, string? action) =>
$@"
<script src='https://www.google.com/recaptcha/api.js?render={options.V3Keys.SiteKey}'></script>
<script>
grecaptcha.ready(function () {{
    document.getElementById('{formId}').addEventListener('submit', function (event) {{
        event.preventDefault();

        grecaptcha.execute('{options.V3Keys.SiteKey}'{(!string.IsNullOrWhiteSpace(action) ? $@", {{ action: '{action}' }}" : string.Empty)})
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