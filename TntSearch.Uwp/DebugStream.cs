using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TntSearch.Uwp
{
    public class DebugStream : Stream
    {
        public override bool CanRead { get; } = false;

        public override bool CanSeek { get; } = false;

        public override bool CanWrite { get; } = true;

        public override long Length => throw new NotImplementedException();

        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Flush() => Debug.Flush();

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            var text = Encoding.UTF8.GetString(buffer, offset, count);
            Debug.Write(text);
        }
    }
}
