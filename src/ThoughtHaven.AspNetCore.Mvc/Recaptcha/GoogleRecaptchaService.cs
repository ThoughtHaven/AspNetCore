using System.Text.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ThoughtHaven.AspNetCore.Mvc.Recaptcha
{
    public class GoogleRecaptchaService
    {
        protected HttpClient HttpClient { get; }
        protected GoogleRecaptchaOptions Options { get; }

        public GoogleRecaptchaService(HttpClient httpClient, GoogleRecaptchaOptions options)
        {
            this.HttpClient = Guard.Null(nameof(httpClient), httpClient);
            this.Options = Guard.Null(nameof(options), options);
        }

        public virtual Task<Result<UiMessage>> VerifyV2Checkbox(
            GoogleRecaptchaVerifyModel model) =>
            this.VerifyV2Checkbox(model?.ResponseToken!);

        public virtual Task<Result<UiMessage>> VerifyV2Checkbox(string responseToken) =>
            VerifyV2Core(this.Options.V2CheckboxKeys, responseToken,
                ErrorMessages.V2CheckboxFailed);

        public virtual Task<Result<UiMessage>> VerifyV2Invisible(
            GoogleRecaptchaVerifyModel model) =>
            this.VerifyV2Invisible(model?.ResponseToken!);

        public virtual Task<Result<UiMessage>> VerifyV2Invisible(string responseToken) =>
            VerifyV2Core(this.Options.V2InvisibleKeys, responseToken,
                ErrorMessages.V2InvisibleFailed);

        private async Task<Result<UiMessage>> VerifyV2Core(GoogleRecaptchaKeys keys,
            string responseToken, string errorMessage)
        {
            var result = await this.VerifyCore<V2ApiResponseModel>(keys, responseToken,
                errorMessage).ConfigureAwait(false);

            if (!result.Success) { return result.Failure; }

            return result.Value.Success ?
                new Result<UiMessage>() :
                new UiMessage(errorMessage);
        }

        public virtual Task<Result<GoogleRecaptchaV3Score, UiMessage>> VerifyV3(
            GoogleRecaptchaVerifyModel model) => this.VerifyV3(model?.ResponseToken!);

        public virtual async Task<Result<GoogleRecaptchaV3Score, UiMessage>> VerifyV3(
            string responseToken)
        {
            var result = await VerifyCore<V3ApiResponseModel>(this.Options.V3Keys, responseToken,
                ErrorMessages.V3Failed).ConfigureAwait(false);

            if (!result.Success)
            {
                return result.Failure;
            }

            return result.Value.Success ?
                new Result<GoogleRecaptchaV3Score, UiMessage>(
                    new GoogleRecaptchaV3Score(result.Value.Score)) :
                new UiMessage(ErrorMessages.V3Failed);
        }

        private async Task<Result<TResponse, UiMessage>> VerifyCore<TResponse>(
            GoogleRecaptchaKeys keys, string responseToken, string errorMessage)
            where TResponse : V2ApiResponseModel
        {
            Guard.Null(nameof(keys), keys);

            if (string.IsNullOrWhiteSpace(responseToken))
            {
                return new UiMessage(errorMessage);
            }

            var form = new Dictionary<string, string>()
            {
                { "secret", keys.SecretKey },
                { "response", responseToken },
            };

            var response = await this.HttpClient.PostAsync(
                "https://www.google.com/recaptcha/api/siteverify",
                new FormUrlEncodedContent(form!)).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return new UiMessage(errorMessage);
            }

            using var json = await response.Content.ReadAsStreamAsync()
                .ConfigureAwait(false);
            
            return (await JsonSerializer.DeserializeAsync<TResponse>(json))!;
        }

        protected class V2ApiResponseModel
        {
            public bool Success { get; set; }
        }

        protected class V3ApiResponseModel : V2ApiResponseModel
        {
            public double Score { get; set; } = 0.0;
        }

        public static class ErrorMessages
        {
            public const string V2CheckboxFailed = "Please tap the CAPTCHA checkbox to prove you are not a robot.";
            public const string V2InvisibleFailed = "Please try again so we can prove you are not a robot.";
            public const string V3Failed = V2InvisibleFailed;
        }
    }
}