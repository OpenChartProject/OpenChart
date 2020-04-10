namespace OpenChart.Formats
{
    /// <summary>
    /// The interface for a class which can serialize and unserialize a FFO (file-format object).
    /// </summary>
    /// <typeparam name="T">The FFO's class.</typeparam>
    public interface IFormatSerializer<T>
    {
        /// <summary>
        /// Serializes the FFO into a byte array.
        /// </summary>
        byte[] Serialize(T obj);

        /// <summary>
        /// Unserializes the byte array into a FFO and returns it.
        /// </summary>
        T Unserialize(byte[] data);
    }
}