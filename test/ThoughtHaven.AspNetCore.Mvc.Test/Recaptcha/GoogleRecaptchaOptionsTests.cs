using System;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc.Recaptcha
{
    public class GoogleRecaptchaOptionsTests
    {
        public class Constructor
        {
            public class PrimaryOverload
            {
                [Fact]
                public void NullV2CheckboxKeys_Throws()
                {
                    Assert.Throws<ArgumentNullException>("v2CheckboxKeys", () =>
                    {
                        new GoogleRecaptchaOptions(
                            v2CheckboxKeys: null!,
                            v2InvisibleKeys: Keys(),
                            v3Keys: Keys());
                    });
                }

                [Fact]
                public void NullV2InvisibleKeys_Throws()
                {
                    Assert.Throws<ArgumentNullException>("v2InvisibleKeys", () =>
                    {
                        new GoogleRecaptchaOptions(
                            v2CheckboxKeys: Keys(),
                            v2InvisibleKeys: null!,
                            v3Keys: Keys());
                    });
                }

                [Fact]
                public void NullV3Keys_Throws()
                {
                    Assert.Throws<ArgumentNullException>("v3Keys", () =>
                    {
                        new GoogleRecaptchaOptions(
                            v2CheckboxKeys: Keys(),
                            v2InvisibleKeys: Keys(),
                            v3Keys: null!);
                    });
                }

                [Fact]
                public void WhenCalled_SetsV2CheckboxKeys()
                {
                    var checkbox = Keys();
                    var invisible = Keys();
                    var v3 = Keys();

                    var options = new GoogleRecaptchaOptions(checkbox, invisible, v3);

                    Assert.Equal(checkbox, options.V2CheckboxKeys);
                }

                [Fact]
                public void WhenCalled_SetsV2InvisibleKeys()
                {
                    var checkbox = Keys();
                    var invisible = Keys();
                    var v3 = Keys();

                    var options = new GoogleRecaptchaOptions(checkbox, invisible, v3);

                    Assert.Equal(invisible, options.V2InvisibleKeys);
                }

                [Fact]
                public void WhenCalled_SetsV3Keys()
                {
                    var checkbox = Keys();
                    var invisible = Keys();
                    var v3 = Keys();

                    var options = new GoogleRecaptchaOptions(checkbox, invisible, v3);

                    Assert.Equal(v3, options.V3Keys);
                }
            }
        }

        private static GoogleRecaptchaKeys Keys() =>
            new GoogleRecaptchaKeys("site", "secret");
    }
}