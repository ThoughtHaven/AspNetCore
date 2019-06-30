using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThoughtHaven.AspNetCore.Mvc.Fakes
{
    public class FakeCompositeViewEngine : ICompositeViewEngine
    {
        public ActionContext? FindView_InputContext;
        public string? FindView_InputViewName;
        public bool FindView_IsMainPage;
        public ViewEngineResult FindView_Output = ViewEngineResult.Found("View", new FakeView());
        public ViewEngineResult FindView(ActionContext context, string viewName,
            bool isMainPage)
        {
            this.FindView_InputContext = context;
            this.FindView_InputViewName = viewName;
            this.FindView_IsMainPage = isMainPage;

            return this.FindView_Output;
        }

        public IReadOnlyList<IViewEngine> ViewEngines => throw new NotImplementedException();
        public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage) => throw new NotImplementedException();

        public class FakeView : IView
        {
            public string Path => throw new NotImplementedException();

            public Task RenderAsync(ViewContext context) => throw new NotImplementedException();
        }
    }
}