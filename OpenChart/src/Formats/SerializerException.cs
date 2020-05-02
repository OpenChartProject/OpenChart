using System;

namespace OpenChart.Formats
{
    /// <summary>
    /// An exception that occurs while serializing/deserializing a file.
    /// </summary>
    public class SerializerException : FileFormatException
    {
        public SerializerException() : base() { }
        public SerializerException(string msg) : base(msg) { }
        public SerializerException(string msg, Exception inner) : base(msg, inner) { }
    }
}
