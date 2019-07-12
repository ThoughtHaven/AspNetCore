using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc.Recaptcha
{
    public class GoogleRecaptchaV2CheckboxTagHelperTests
    {
        public class Type
        {
            [Fact]
            public void InheritsTagHelper()
            {
                Assert.True(typeof(TagHelper).IsAssignableFrom(
                    typeof(GoogleRecaptchaV2CheckboxTagHelper)));
            }

            [Fact]
            public void HasHtmlTargetElementAttribute()
            {
                var attribute = TestHelpers.GetClassAttribute<GoogleRecaptchaV2CheckboxTagHelper, HtmlTargetElementAttribute>();

                Assert.Equal("div", attribute.Tag);
                Assert.Equal("google-recaptcha-v2checkbox", attribute.Attributes);
            }
        }

        public class GoogleRecaptchaV2CheckboxProperty
        {
            [Fact]
            public void HasHtmlAttributeNameAttribute()
            {
                var attribute = TestHelpers.GetPropertyAttribute<GoogleRecaptchaV2CheckboxTagHelper, HtmlAttributeNameAttribute>(
                    "GoogleRecaptchaV2Checkbox");

                Assert.Equal("google-recaptcha-v2checkbox", attribute.Name);
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
                        new GoogleRecaptchaV2CheckboxTagHelper(options: null!);
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
                public void GoogleRecaptchaV2CheckboxFalse_DoesNotClearTagName()
                {
                    var helper = TagHelper();
                    helper.GoogleRecaptchaV2Checkbox = false;
                    var context = Context();
                    var output = Output();
                    output.TagName = "div";

                    helper.Process(context, output);

                    Assert.Equal("div", output.TagName);
                }

                [Fact]
                public void GoogleRecaptchaV2CheckboxTrue_RemovesTagName()
                {
                    var helper = TagHelper();
                    helper.GoogleRecaptchaV2Checkbox = true;
                    var context = Context();
                    var output = Output();
                    output.TagName = "div";

                    helper.Process(context, output);

                    Assert.Null(output.TagName);
                }

                [Fact]
                public void GoogleRecaptchaV2CheckboxTrue_AddsPostContent()
                {
                    var options = Options();
                    var helper = TagHelper(options);
                    helper.GoogleRecaptchaV2Checkbox = true;
                    var context = Context();
                    var output = Output();
                    output.TagName = "div";

                    helper.Process(context, output);

                    var postContent = output.PostContent.GetContent();

                    Assert.Equal(
                        $@"<script class=""test"" src=""https://www.google.com/recaptcha/api.js"" async defer></script><div class=""g-recaptcha"" data-sitekey=""{options.V2CheckboxKeys.SiteKey}""></div>",
                        postContent);
                }
            }
        }

        private static GoogleRecaptchaOptions Options() =>
            new GoogleRecaptchaOptions(
                new GoogleRecaptchaKeys("csite", "csecret"),
                new GoogleRecaptchaKeys("isite", "isecret"),
                new GoogleRecaptchaKeys("3site", "3secret"));
        private static GoogleRecaptchaV2CheckboxTagHelper TagHelper(
            GoogleRecaptchaOptions? options = null) =>
            new GoogleRecaptchaV2CheckboxTagHelper(options ?? Options());
        private static TagHelperContext Context() =>
            new TagHelperContext("div", new TagHelperAttributeList(),
                new Dictionary<object, object>(), Guid.NewGuid().ToString());
        private static TagHelperOutput Output() =>
            new TagHelperOutput("div", new TagHelperAttributeList(),
                (_, __) => null);
    }
}