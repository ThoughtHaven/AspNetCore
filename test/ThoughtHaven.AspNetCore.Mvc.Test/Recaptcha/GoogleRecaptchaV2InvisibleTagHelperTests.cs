using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc.Recaptcha
{
    public class GoogleRecaptchaV2InvisibleTagHelperTests
    {
        public class Type
        {
            [Fact]
            public void InheritsTagHelper()
            {
                Assert.True(typeof(TagHelper).IsAssignableFrom(
                    typeof(GoogleRecaptchaV2InvisibleTagHelper)));
            }

            [Fact]
            public void HasHtmlTargetElementAttribute()
            {
                var attribute = TestHelpers.GetClassAttribute<GoogleRecaptchaV2InvisibleTagHelper, HtmlTargetElementAttribute>();

                Assert.Equal("form", attribute.Tag);
                Assert.Equal("google-recaptcha-v2invisible", attribute.Attributes);
            }
        }

        public class GoogleRecaptchaV2InvisibleProperty
        {
            [Fact]
            public void HasHtmlAttributeNameAttribute()
            {
                var attribute = TestHelpers.GetPropertyAttribute<GoogleRecaptchaV2InvisibleTagHelper, HtmlAttributeNameAttribute>(
                    "GoogleRecaptchaV2Invisible");

                Assert.Equal("google-recaptcha-v2invisible", attribute.Name);
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
                        new GoogleRecaptchaV2InvisibleTagHelper(options: null!);
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
                public void GoogleRecaptchaV2InvisibleFalse_DoesNotAddId()
                {
                    var helper = TagHelper();
                    helper.GoogleRecaptchaV2Invisible = false;
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
                    helper.GoogleRecaptchaV2Invisible = true;
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
                    helper.GoogleRecaptchaV2Invisible = true;
                    var context = Context();
                    var output = Output();

                    helper.Process(context, output);

                    Assert.Equal($"form{context.UniqueId}", output.Attributes["id"].Value);
                }

                [Theory]
                [InlineData("id1")]
                [InlineData("id2")]
                public void WhenCalled_AppendsPostContentHtml(string id)
                {
                    var options = Options();
                    var helper = TagHelper(options);
                    helper.GoogleRecaptchaV2Invisible = true;
                    var context = Context();
                    var output = Output();
                    output.Attributes.SetAttribute("id", id);

                    helper.Process(context, output);

                    Assert.Equal(CaptchaPostContentHtml(options, context, id),
                        output.PostContent.GetContent());
                }
            }
        }

        private static GoogleRecaptchaOptions Options() =>
            new GoogleRecaptchaOptions(
                new GoogleRecaptchaKeys("csite", "csecret"),
                new GoogleRecaptchaKeys("isite", "isecret"),
                new GoogleRecaptchaKeys("3site", "3secret"));
        private static GoogleRecaptchaV2InvisibleTagHelper TagHelper(
            GoogleRecaptchaOptions? options = null) =>
            new GoogleRecaptchaV2InvisibleTagHelper(options ?? Options());
        private static TagHelperContext Context() =>
            new TagHelperContext("div", new TagHelperAttributeList(),
                new Dictionary<object, object>(), Guid.NewGuid().ToString());
        private static TagHelperOutput Output() =>
            new TagHelperOutput("div", new TagHelperAttributeList(),
                (_, __) => null);
        private static string CaptchaPostContentHtml(GoogleRecaptchaOptions options,
            TagHelperContext context, string formId) =>
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
<div class='g-recaptcha' data-sitekey='{options.V2InvisibleKeys.SiteKey}'
        data-callback='__googleRecaptchaCallback{context.UniqueId}' data-size='invisible'></div>
";
    }
}