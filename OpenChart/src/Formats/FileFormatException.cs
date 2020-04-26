using System;

namespace OpenChart.Formats
{
    /// <summary>
    /// The base exception type for format-related exceptions.
    /// </summary>
    public abstract class FileFormatException : Exception
    {
        public FileFormatException(string msg) : base(msg) { }
    }
}
