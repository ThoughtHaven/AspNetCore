using System;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace ThoughtHaven.AspNetCore.Mvc.Fakes
{
    public class FakeFileInfo : IFileInfo
    {
        public bool Exists => throw new NotImplementedException();

        public bool IsDirectory => throw new NotImplementedException();

        public DateTimeOffset LastModified => throw new NotImplementedException();

        public long Length => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public string PhysicalPath => throw new NotImplementedException();

        public Stream CreateReadStream() => throw new NotImplementedException();
    }
}