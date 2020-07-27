using OpenChart.Formats.StepMania.SM.Data;
using OpenChart.Projects;
using System.IO;
using System.Text;

namespace OpenChart.Formats.StepMania.SM
{
    public class OpenChartFormatHandler : IFormatHandler
    {
        static IProjectConverter<StepFileData> converter = new SMConverter();
        static IFormatSerializer<StepFileData> serializer = new SMSerializer();

        /// <summary>
        /// The name of the format.
        /// </summary>
        public string FormatName => "StepMania";

        /// <summary>
        /// The extension used by the format.
        /// </summary>
        public string FileExtension => ".sm";

        /// <summary>
        /// Reads data from a stepmania file and returns a Project object.
        /// </summary>
        /// <param name="reader">The stream that contains the file data.</param>
        public Project Read(StreamReader reader)
        {
            var data = reader.ReadToEnd();
            var projectData = serializer.Deserialize(Encoding.UTF8.GetBytes(data));

            return converter.ToNative(projectData);
        }

        /// <summary>
        /// Writes the project object to an .sm file.
        /// </summary>
        /// <param name="writer">The stream to write to.</param>
        /// <param name="project">The project to write.</param>
        public void Write(StreamWriter writer, Project project)
        {
            var data = serializer.Serialize(converter.FromNative(project));

            writer.Write(data);
            writer.Flush();
        }
    }
}
