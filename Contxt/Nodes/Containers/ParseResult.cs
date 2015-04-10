using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contxt.Nodes.Containers
{
    public class ParseResult
    {
        public enum ResultType
        {
            Failure,
            Success
        }

        public static readonly ParseResult Success = new ParseResult(ResultType.Success);
        public static readonly ParseResult Failure = new ParseResult(ResultType.Failure);

        public ResultType Result { get; private set; }
        public int LineNumber { get; private set; }
        public string Line { get; private set; }
        public string Message { get; private set; }

        public ParseResult(ResultType result, int lineNumber = -1, string line = null, string message = null)
        {
            Result = result;
            LineNumber = lineNumber;
            Line = line;
            Message = message;
        }

        public ParseResult Derive(int lineNumber = -1, string line = null, string message = null)
        {
            return new ParseResult(Result, lineNumber, line, message);
        }
    }
}
