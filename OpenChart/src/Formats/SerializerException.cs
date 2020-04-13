namespace OpenChart.Formats
{
    /// <summary>
    /// An exception that occurs while serializing/deserializing a file.
    /// </summary>
    public class SerializerException : FileFormatException
    {
        public SerializerException(string msg) : base(msg) { }
    }
}