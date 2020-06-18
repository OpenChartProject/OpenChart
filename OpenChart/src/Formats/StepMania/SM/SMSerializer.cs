using OpenChart.Formats.StepMania.SM.Data;

using System.Text;

namespace OpenChart.Formats.StepMania.SM
{
    /// <summary>
    /// Serializer class for importing .sm files.
    ///
    /// .sm docs: https://github.com/stepmania/stepmania/wiki/sm
    /// </summary>
    public class SMSerializer : IFormatSerializer<StepFileData>
    {
        /// <summary>
        /// Deserializes raw file data into a StepFileData object.
        /// </summary>
        public StepFileData Deserialize(byte[] data)
        {
            var strData = Encoding.UTF8.GetString(data);
            var fields = FieldExtractor.Extract(strData);

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
