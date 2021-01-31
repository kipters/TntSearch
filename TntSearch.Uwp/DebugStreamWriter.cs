using System.IO;

namespace TntSearch.Uwp
{
    public class DebugStreamWriter : StreamWriter
    {
        public DebugStreamWriter() : base(new DebugStream())
        {

        }
    }
}
