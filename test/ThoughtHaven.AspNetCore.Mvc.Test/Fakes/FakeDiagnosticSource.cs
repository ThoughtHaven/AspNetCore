using System.Diagnostics;

namespace ThoughtHaven.AspNetCore.Mvc.Fakes
{
    public class FakeDiagnosticSource : DiagnosticSource
    {
        public FakeDiagnosticSource() : base() { }

        public override bool IsEnabled(string name) => throw new System.NotImplementedException();
        public override void Write(string name, object value) => throw new System.NotImplementedException();
    }
}