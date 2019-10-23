namespace ThoughtHaven.AspNetCore.Mvc.Recaptcha
{
    public class GoogleRecaptchaV3Score : ValueObject<double>
    {
        public GoogleRecaptchaV3Score(double value)
            : base(value)
        {
            Guard.OutOfRange(nameof(value), value, minimum: 0.0, maximum: 1.0);
        }
    }
}