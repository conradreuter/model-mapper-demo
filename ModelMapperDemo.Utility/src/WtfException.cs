using System;

namespace ModelMapperDemo.Utility
{
    /// <summary>
    /// Can be thrown in places that should be unreachable.
    /// </summary>
    public class WtfException : Exception
    {
        public WtfException() { }
        public WtfException(string message) : base(message) { }
        public WtfException(string message, Exception inner) : base(message, inner) { }
    }
}
