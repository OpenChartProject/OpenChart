using OpenChart.Formats.StepMania.SM.Data;

namespace OpenChart.Formats.StepMania.SM
{
    /// <summary>
    /// Serializer class for importing .sm files.
    /// </summary>
    public class SMSerializer : IFormatSerializer<StepFileData>
    {
        /// <summary>
        /// Deserializes raw file data into a StepFileData object.
        /// </summary>
        public StepFileData Deserialize(byte[] data)
        {
            return null;
        }

        /// <summary>
        /// Serializes a StepFileData object into the raw .sm data.
        /// </summary>
        public byte[] Serialize(StepFileData obj)
        {
            return null;
        }
    }
}
