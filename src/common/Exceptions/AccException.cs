using System;

namespace ACC.Common.Exceptions
{
    public class AccException : Exception
    {
        public string Code { get; }

        public AccException()
        {
        }

        public AccException(string code)
        {
            Code = code;
        }

        public AccException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public AccException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {
        }

        public AccException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public AccException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}