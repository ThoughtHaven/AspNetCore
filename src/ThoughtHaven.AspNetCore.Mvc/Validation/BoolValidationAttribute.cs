namespace System.ComponentModel.DataAnnotations
{
    public class BoolValidationAttribute : ValidationAttribute
    {
        public bool RequiredValue { get; set; }

        public BoolValidationAttribute(bool requiredValue)
        {
            this.RequiredValue = requiredValue;
        }

        public override bool IsValid(object? value) =>
            value is bool result && result == this.RequiredValue;
    }
}