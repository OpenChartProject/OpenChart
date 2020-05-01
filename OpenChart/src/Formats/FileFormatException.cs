using System;

namespace OpenChart.Formats
{
    /// <summary>
    /// The base exception type for format-related exceptions.
    /// </summary>
    public abstract class FileFormatException : Exception
    {
        public FileFormatException() : base() { }
        public FileFormatException(string msg) : base(msg) { }
        public FileFormatException(string msg, Exception inner) : base(msg, inner) { }
    }
}
