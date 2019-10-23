using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ThoughtHaven.AspNetCore.Mvc.Fakes;
using Xunit;
using static ThoughtHaven.AspNetCore.Mvc.Recaptcha.GoogleRecaptchaService;

namespace ThoughtHaven.AspNetCore.Mvc.Recaptcha
{
    public class GoogleRecaptchaServiceTests
    {
        public class Constructor
        {
            public class PrimaryOverload
            {
                [Fact]
                public void NullHttpClient_Throws()
                {
                    Assert.Throws<ArgumentNullException>("httpClient", () =>
                    {
                        new GoogleRecaptchaService(
                            httpClient: null!,
                            options: Options());
                    });
                }

                [Fact]
                public void NullOptions_Throws()
                {
                    Assert.Throws<ArgumentNullException>("options", () =>
                    {
                        new GoogleRecaptchaService(
                            httpClient: HttpClient(),
                            options: null!);
                    });
                }
            }
        }

        public class VerifyV2CheckboxMethod
        {
            public class ModelOverload
            {
                [Fact]
                public async Task NullModel_ReturnsFailure()
                {
                    var service = Service();

                    var result = await service.VerifyV2Checkbox(model: null!);

                    Assert.False(result.Success);
                    Assert.Equal(ErrorMessages.V2CheckboxFailed, result.Failure.Message);
                }

                [Theory]
                [InlineData(null)]
                [InlineData("")]
                [InlineData(" ")]
                public async Task NoResponseToken_ReturnsFailure(string responseToken)
                {
                    var service = Service();
                    var model = new GoogleRecaptchaVerifyModel()
                    {
                        ResponseToken = responseToken
                    };

                    var result = await service.VerifyV2Checkbox(model);

                    Assert.False(result.Success);
                    Assert.Equal(ErrorMessages.V2CheckboxFailed, result.Failure.Message);
                }

                [Fact]
                public async Task HttpClientReturnsSuccess_ReturnsSuccess()
                {
                    var httpClient = HttpClient();
                    httpClient.SendAsync_Output = new V2ApiResponseModel()
                    {
                        Success = true
                    }.ToResponseMessage();
                    var options = Options();
                    var service = Service(httpClient, options);
                    var model = new GoogleRecaptchaVerifyModel()
                    {
                        ResponseToken = "token"
                    };

                    var result = await service.VerifyV2Checkbox(model);

                    Assert.True(result.Success);
                }
            }

            public class ResponseTokenOverload
            {
                [Theory]
                [InlineData(null)]
                [InlineData("")]
                [InlineData(" ")]
                public async Task NoResponseToken_ReturnsFailure(string responseToken)
                {
                    var service = Service();

                    var result = await service.VerifyV2Checkbox(responseToken);

                    Assert.False(result.Success);
                    Assert.Equal(ErrorMessages.V2CheckboxFailed, result.Failure.Message);
                }

                [Theory]
                [InlineData("token1")]
                [InlineData("token2")]
                public async Task WhenCalled_CallsPostAsyncOnHttpClient(string responseToken)
                {
                    var httpClient = HttpClient();
                    var options = Options();
                    var service = Service(httpClient, options);

                    await service.VerifyV2Checkbox(responseToken);

                    Assert.Equal(new Uri("https://www.google.com/recaptcha/api/siteverify"),
                        httpClient.SendAsync_InputRequest!.RequestUri);

                    var content = Assert.IsType<FormUrlEncodedContent>(
                        httpClient.SendAsync_InputRequest.Content);
                    var form = ParseForm(await content.ReadAsStringAsync());

                    Assert.Equal(options.V2CheckboxKeys.SecretKey, form["secret"]);
                    Assert.Equal(responseToken, form["response"]);
                }

                [Fact]
                public async Task HttpClientReturnsErrorStatusCode_ReturnsFailure()
                {
                    var httpClient = HttpClient();
                    httpClient.SendAsync_Output = new V2ApiResponseModel()
                        .ToResponseMessage(HttpStatusCode.BadRequest);
                    var options = Options();
                    var service = Service(httpClient, options);

                    var result = await service.VerifyV2Checkbox("token");

                    Assert.False(result.Success);
                    Assert.Equal(ErrorMessages.V2CheckboxFailed, result.Failure.Message);
                }

                [Fact]
                public async Task HttpClientReturnsSuccessEqualsFalse_ReturnsFailure()
                {
                    var httpClient = HttpClient();
                    httpClient.SendAsync_Output = new V2ApiResponseModel()
                    {
                        Success = false
                    }.ToResponseMessage();
                    var options = Options();
                    var service = Service(httpClient, options);

                    var result = await service.VerifyV2Checkbox("token");

                    Assert.False(result.Success);
                    Assert.Equal(ErrorMessages.V2CheckboxFailed, result.Failure.Message);
                }

                [Fact]
                public async Task HttpClientReturnsSuccess_ReturnsSuccess()
                {
                    var httpClient = HttpClient();
                    httpClient.SendAsync_Output = new V2ApiResponseModel()
                    {
                        Success = true
                    }.ToResponseMessage();
                    var options = Options();
                    var service = Service(httpClient, options);

                    var result = await service.VerifyV2Checkbox("token");

                    Assert.True(result.Success);
                }
            }
        }

        public class VerifyV2InvisibleMethod
        {
            public class ModelOverload
            {
                [Fact]
                public async Task NullModel_ReturnsFailure()
                {
                    var service = Service();

                    var result = await service.VerifyV2Invisible(model: null!);

                    Assert.False(result.Success);
                    Assert.Equal(ErrorMessages.V2InvisibleFailed, result.Failure.Message);
                }

                [Theory]
                [InlineData(null)]
                [InlineData("")]
                [InlineData(" ")]
                public async Task NoResponseToken_ReturnsFailure(string responseToken)
                {
                    var service = Service();
                    var model = new GoogleRecaptchaVerifyModel()
                    {
                        ResponseToken = responseToken
                    };

                    var result = await service.VerifyV2Invisible(model);

                    Assert.False(result.Success);
                    Assert.Equal(ErrorMessages.V2InvisibleFailed, result.Failure.Message);
                }

                [Fact]
                public async Task HttpClientReturnsSuccess_ReturnsSuccess()
                {
                    var httpClient = HttpClient();
                    httpClient.SendAsync_Output = new V2ApiResponseModel()
                    {
                        Success = true
                    }.ToResponseMessage();
                    var options = Options();
                    var service = Service(httpClient, options);
                    var model = new GoogleRecaptchaVerifyModel()
                    {
                        ResponseToken = "token"
                    };

                    var result = await service.VerifyV2Invisible(model);

                    Assert.True(result.Success);
                }
            }

            public class ResponseTokenOverload
            {
                [Theory]
                [InlineData(null)]
                [InlineData("")]
                [InlineData(" ")]
                public async Task NoResponseToken_ReturnsFailure(string responseToken)
                {
                    var service = Service();

                    var result = await service.VerifyV2Invisible(responseToken);

                    Assert.False(result.Success);
                    Assert.Equal(ErrorMessages.V2InvisibleFailed, result.Failure.Message);
                }

                [Theory]
                [InlineData("token1")]
                [InlineData("token2")]
                public async Task WhenCalled_CallsPostAsyncOnHttpClient(string responseToken)
                {
                    var httpClient = HttpClient();
                    var options = Options();
                    var service = Service(httpClient, options);

                    await service.VerifyV2Invisible(responseToken);

                    Assert.Equal(new Uri("https://www.google.com/recaptcha/api/siteverify"),
                        httpClient.SendAsync_InputRequest!.RequestUri);

                    var content = Assert.IsType<FormUrlEncodedContent>(
                        httpClient.SendAsync_InputRequest.Content);
                    var form = ParseForm(await content.ReadAsStringAsync());

                    Assert.Equal(options.V2InvisibleKeys.SecretKey, form["secret"]);
                    Assert.Equal(responseToken, form["response"]);
                }

                [Fact]
                public async Task HttpClientReturnsErrorStatusCode_ReturnsFailure()
                {
                    var httpClient = HttpClient();
                    httpClient.SendAsync_Output = new V2ApiResponseModel()
                        .ToResponseMessage(HttpStatusCode.BadRequest);
                    var options = Options();
                    var service = Service(httpClient, options);

                    var result = await service.VerifyV2Invisible("token");

                    Assert.False(result.Success);
                    Assert.Equal(ErrorMessages.V2InvisibleFailed, result.Failure.Message);
                }

                [Fact]
                public async Task HttpClientReturnsSuccessEqualsFalse_ReturnsFailure()
                {
                    var httpClient = HttpClient();
                    httpClient.SendAsync_Output = new V2ApiResponseModel()
                    {
                        Success = false
                    }.ToResponseMessage();
                    var options = Options();
                    var service = Service(httpClient, options);

                    var result = await service.VerifyV2Invisible("token");

                    Assert.False(result.Success);
                    Assert.Equal(ErrorMessages.V2InvisibleFailed, result.Failure.Message);
                }

                [Fact]
                public async Task HttpClientReturnsSuccess_ReturnsSuccess()
                {
                    var httpClient = HttpClient();
                    httpClient.SendAsync_Output = new V2ApiResponseModel()
                    {
                        Success = true
                    }.ToResponseMessage();
                    var options = Options();
                    var service = Service(httpClient, options);

                    var result = await service.VerifyV2Invisible("token");

                    Assert.True(result.Success);
                }
            }
        }

        public class VerifyV3Method
        {
            public class ModelOverload
            {
                [Fact]
                public async Task NullModel_ReturnsFailure()
                {
                    var service = Service();

                    var result = await service.VerifyV3(model: null!);

                    Assert.False(result.Success);
                    Assert.Equal(ErrorMessages.V3Failed, result.Failure.Message);
                }

                [Theory]
                [InlineData(null)]
                [InlineData("")]
                [InlineData(" ")]
                public async Task NoResponseToken_ReturnsFailure(string responseToken)
                {
                    var service = Service();
                    var model = new GoogleRecaptchaVerifyModel()
                    {
                        ResponseToken = responseToken
                    };

                    var result = await service.VerifyV3(model);

                    Assert.False(result.Success);
                    Assert.Equal(ErrorMessages.V3Failed, result.Failure.Message);
                }

                [Fact]
                public async Task HttpClientReturnsSuccess_ReturnsSuccess()
                {
                    var httpClient = HttpClient();
                    httpClient.SendAsync_Output = new V3ApiResponseModel()
                    {
                        Success = true
                    }.ToResponseMessage();
                    var options = Options();
                    var service = Service(httpClient, options);
                    var model = new GoogleRecaptchaVerifyModel()
                    {
                        ResponseToken = "token"
                    };

                    var result = await service.VerifyV3(model);

                    Assert.True(result.Success);
                }
            }

            public class ResponseTokenOverload
            {
                [Theory]
                [InlineData(null)]
                [InlineData("")]
                [InlineData(" ")]
                public async Task NoResponseToken_ReturnsFailure(string responseToken)
                {
                    var service = Service();

                    var result = await service.VerifyV3(responseToken);

                    Assert.False(result.Success);
                    Assert.Equal(ErrorMessages.V3Failed, result.Failure.Message);
                }

                [Theory]
                [InlineData("token1")]
                [InlineData("token2")]
                public async Task WhenCalled_CallsPostAsyncOnHttpClient(string responseToken)
                {
                    var httpClient = HttpClient();
                    var options = Options();
                    var service = Service(httpClient, options);

                    await service.VerifyV3(responseToken);

                    Assert.Equal(new Uri("https://www.google.com/recaptcha/api/siteverify"),
                        httpClient.SendAsync_InputRequest!.RequestUri);

                    var content = Assert.IsType<FormUrlEncodedContent>(
                        httpClient.SendAsync_InputRequest.Content);
                    var form = ParseForm(await content.ReadAsStringAsync());

                    Assert.Equal(options.V3Keys.SecretKey, form["secret"]);
                    Assert.Equal(responseToken, form["response"]);
                }

                [Fact]
                public async Task HttpClientReturnsErrorStatusCode_ReturnsFailure()
                {
                    var httpClient = HttpClient();
                    httpClient.SendAsync_Output = new V2ApiResponseModel()
                        .ToResponseMessage(HttpStatusCode.BadRequest);
                    var options = Options();
                    var service = Service(httpClient, options);

                    var result = await service.VerifyV3("token");

                    Assert.False(result.Success);
                    Assert.Equal(ErrorMessages.V3Failed, result.Failure.Message);
                }

                [Fact]
                public async Task HttpClientReturnsSuccessEqualsFalse_ReturnsFailure()
                {
                    var httpClient = HttpClient();
                    httpClient.SendAsync_Output = new V2ApiResponseModel()
                    {
                        Success = false
                    }.ToResponseMessage();
                    var options = Options();
                    var service = Service(httpClient, options);

                    var result = await service.VerifyV3("token");

                    Assert.False(result.Success);
                    Assert.Equal(ErrorMessages.V3Failed, result.Failure.Message);
                }

                [Fact]
                public async Task HttpClientReturnsSuccess_ReturnsSuccess()
                {
                    var httpClient = HttpClient();
                    httpClient.SendAsync_Output = new V2ApiResponseModel()
                    {
                        Success = true
                    }.ToResponseMessage();
                    var options = Options();
                    var service = Service(httpClient, options);

                    var result = await service.VerifyV3("token");

                    Assert.True(result.Success);
                }
            }
        }

        public class ErrorMessagesTests
        {
            public class V2CheckboxFailedField
            {
                public class GetAccessor
                {
                    [Fact]
                    public void WhenCalled_ReturnsValue()
                    {
                        Assert.Equal("Please tap the CAPTCHA checkbox to prove you are not a robot.",
                            ErrorMessages.V2CheckboxFailed);
                    }
                }
            }

            public class V2InvisibleFailedField
            {
                public class GetAccessor
                {
                    [Fact]
                    public void WhenCalled_ReturnsValue()
                    {
                        Assert.Equal("Please try again so we can prove you are not a robot.",
                            ErrorMessages.V2InvisibleFailed);
                    }
                }
            }

            public class V3FailedField
            {
                public class GetAccessor
                {
                    [Fact]
                    public void WhenCalled_ReturnsValue()
                    {
                        Assert.Equal(ErrorMessages.V2InvisibleFailed,
                            ErrorMessages.V3Failed);
                    }
                }
            }
        }

        private static FakeHttpClient HttpClient()
        {
            var client = new FakeHttpClient
            {
                SendAsync_Output = new V2ApiResponseModel() { Success = true }
                    .ToResponseMessage()
            };

            return client;
        }
        private static GoogleRecaptchaOptions Options() =>
            new GoogleRecaptchaOptions(new GoogleRecaptchaKeys("csite", "csecret"),
                new GoogleRecaptchaKeys("isite", "isecret"),
                new GoogleRecaptchaKeys("3site", "3secret"));
        private static GoogleRecaptchaService Service(FakeHttpClient? httpClient = null,
            GoogleRecaptchaOptions? options = null) =>
            new GoogleRecaptchaService(httpClient ?? HttpClient(), options ?? Options());
        private static Dictionary<string, string> ParseForm(string form)
        {
            var kvps = form.Split("&");

            var result = new Dictionary<string, string>();

            foreach (var kvp in kvps)
            {
                var split = kvp.Split("=");

                result.Add(split[0], split[1]);
            }

            return result;
        }
        private class V2ApiResponseModel
        {
            public bool Success { get; set; }

            public HttpResponseMessage ToResponseMessage(
                HttpStatusCode? statusCode = null) =>
                new HttpResponseMessage(statusCode ?? HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(this),
                        Encoding.UTF8, "application/json"),
                };
        }
        private class V3ApiResponseModel : V2ApiResponseModel
        {
            public double Score { get; set; } = 0.0;

            new public HttpResponseMessage ToResponseMessage(
                HttpStatusCode? statusCode = null) =>
                new HttpResponseMessage(statusCode ?? HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(this),
                        Encoding.UTF8, "application/json"),
                };
        }
    }
}