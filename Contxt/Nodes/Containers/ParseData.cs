using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contxt.Nodes.Containers
{
    public class ParseData
    {
        public int Index { get; private set; }
        public string Source { get; private set; }
        public string Type { get; private set; }
        public string[] Arguments { get; private set; }
        public string Value { get; private set; }

        public int LineNumber;
        public string Line { get; private set; }

        public ParseData(int index, string source, string type, string[] arguments, string value, int lineNumber, string line)
        {
            Index = index;
            Source = source;
            Type = type;
            Arguments = arguments;
            Value = value;
            LineNumber = lineNumber;
            Line = line;
        }
    }
}
