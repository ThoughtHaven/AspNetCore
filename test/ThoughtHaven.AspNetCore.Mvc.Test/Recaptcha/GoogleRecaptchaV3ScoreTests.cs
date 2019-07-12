using System;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc.Recaptcha
{
    public class GoogleRecaptchaV3ScoreTests
    {
        public class Constructor
        {
            public class ValueOverload
            {
                [Fact]
                public void ValueBelow0_Throws()
                {
                    Assert.Throws<ArgumentOutOfRangeException>("value", () =>
                    {
                        new GoogleRecaptchaV3Score(value: -0.1);
                    });
                }

                [Fact]
                public void ValueAbove1_Throws()
                {
                    Assert.Throws<ArgumentOutOfRangeException>("value", () =>
                    {
                        new GoogleRecaptchaV3Score(value: 1.1);
                    });
                }

                [Theory]
                [InlineData(0.0)]
                [InlineData(0.5)]
                [InlineData(1.0)]
                public void WhenCalled_SetsValue(double value)
                {
                    var score = new GoogleRecaptchaV3Score(value);

                    Assert.Equal(value, score.Value);
                }
            }
        }
    }
}