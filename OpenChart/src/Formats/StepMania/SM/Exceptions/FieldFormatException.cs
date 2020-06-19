using System;

namespace OpenChart.Formats.StepMania.SM.Exceptions
{
    /// <summary>
    /// An exception thrown when a field is formatted incorrectly.
    /// </summary>
    public class FieldFormatException : FileFormatException
    {
        public FieldFormatException() : base() { }
        public FieldFormatException(string msg) : base(msg) { }
        public FieldFormatException(string msg, Exception inner) : base(msg, inner) { }
    }
}
