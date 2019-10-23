using Microsoft.AspNetCore.Builder;

namespace ThoughtHaven.AspNetCore.Mvc.Fakes
{
    public class FakeBuilderOptions : MvcBuilderOptions
    {
        public bool GetDeveloperExceptionPage_Called;
        public override DeveloperExceptionPageOptions DeveloperExceptionPage
        {
            get
            {
                GetDeveloperExceptionPage_Called = true;

                return base.DeveloperExceptionPage;
            }
        }

        public bool GetExceptionHandler_Called;
        public override ExceptionHandlerOptions ExceptionHandler
        {
            get
            {
                GetExceptionHandler_Called = true;

                return base.ExceptionHandler;
            }
        }

        public bool GetStatusCodePagePathFormat_Called;
        public override string StatusCodePagePathFormat
        {
            get
            {
                GetStatusCodePagePathFormat_Called = true;

                return base.StatusCodePagePathFormat;
            }
            set { base.StatusCodePagePathFormat = value; }
        }
    }
}