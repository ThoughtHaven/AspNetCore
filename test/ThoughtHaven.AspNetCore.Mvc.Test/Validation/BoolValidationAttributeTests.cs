using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ThoughtHaven.AspNetCore.Mvc.Validation
{
    public class BoolValidationAttributeTests
    {
        public class Constructor
        {
            public class RequiredValueOverload
            {
                [Fact]
                public void RequiredValueTrue_SetsRequiredValueToTrue()
                {
                    var attribute = new BoolValidationAttribute(requiredValue: true);

                    Assert.True(attribute.RequiredValue);
                }

                [Fact]
                public void RequiredValueFalse_SetsRequiredValueToFalse()
                {
                    var attribute = new BoolValidationAttribute(requiredValue: false);

                    Assert.False(attribute.RequiredValue);
                }
            }
        }

        public class IsValidMethod
        {
            public class ValueOverload
            {
                [Fact]
                public void ValueNotBool_ReturnsFalse()
                {
                    var attribute = new BoolValidationAttribute(requiredValue: false);

                    Assert.False(attribute.IsValid(""));
                }

                [Fact]
                public void ValueDoesNotEqualRequiredValue_ReturnsFalse()
                {
                    var attribute = new BoolValidationAttribute(requiredValue: true);

                    Assert.False(attribute.IsValid(false));
                }

                [Fact]
                public void ValueEqualsRequiredValue_ReturnsTrue()
                {
                    var attribute = new BoolValidationAttribute(requiredValue: true);

                    Assert.True(attribute.IsValid(true));
                }
            }
        }
    }
}