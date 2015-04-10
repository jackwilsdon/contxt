using System;

namespace Contxt
{
    public class IncorrectTypeException : Exception
    {
        private static string MessageFormat = "expected type {0} but found type {1}";

        public IncorrectTypeException(Type expected, Type actual) : base(GetMessage(expected, actual))
        { }

        private static string GetMessage(Type expected, Type actual) {
            return String.Format(MessageFormat, expected.ToString(), actual.ToString());
        }
    }
}
