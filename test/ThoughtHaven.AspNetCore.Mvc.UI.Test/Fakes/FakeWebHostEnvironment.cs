using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace ThoughtHaven.AspNetCore.Mvc.Fakes
{
    public class FakeWebHostEnvironment : IWebHostEnvironment
    {
        public string EnvironmentName { get; set; } = "Test";
        public string ApplicationName { get; set; } = "App";
        public string WebRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IFileProvider WebRootFileProvider { get; set; } = new FileProvider();
        public string ContentRootPath { get; set; } = "/";
        public IFileProvider ContentRootFileProvider { get; set; } = new FileProvider();

        private class FileProvider : IFileProvider
        {
            public IDirectoryContents GetDirectoryContents(string subpath) => throw new NotImplementedException();
            public IFileInfo GetFileInfo(string subpath) => new FileInfo();
            public IChangeToken Watch(string filter) => new ChangeToken();

            public class FileInfo : IFileInfo
            {
                public bool Exists => throw new NotImplementedException();

                public long Length => throw new NotImplementedException();

                public string PhysicalPath => throw new NotImplementedException();

                public string Name => throw new NotImplementedException();

                public DateTimeOffset LastModified => throw new NotImplementedException();

                public bool IsDirectory => throw new NotImplementedException();

                public Stream CreateReadStream() => throw new NotImplementedException();
            }
            public class ChangeToken : IChangeToken
            {
                public bool HasChanged => throw new NotImplementedException();

                public bool ActiveChangeCallbacks => false;

                public IDisposable RegisterChangeCallback(Action<object> callback, object state) => throw new NotImplementedException();
            }
        }
    }
}