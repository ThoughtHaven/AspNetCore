using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ThoughtHaven.AspNetCore.Mvc.Fakes
{
    public class FakeHttpClient : HttpClient
    {
        public HttpRequestMessage? SendAsync_InputRequest;
        public HttpResponseMessage SendAsync_Output = new HttpResponseMessage(HttpStatusCode.OK);
        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            this.SendAsync_InputRequest = request;

            return Task.FromResult(this.SendAsync_Output);
        }
    }
}