using OpenChart.Formats.OpenChart.Version0_1.Data;
using OpenChart.Projects;
using System.IO;
using System.Text;

namespace OpenChart.Formats.OpenChart.Version0_1
{
    public class OpenChartFormatHandler : IFormatHandler
    {
        static IProjectConverter<ProjectData> converter = new OpenChartConverter();
        static IFormatSerializer<ProjectData> serializer = new OpenChartSerializer();

        /// <summary>
        /// The name of the OpenChart format.
        /// </summary>
        public string FormatName => "OpenChart";

        /// <summary>
        /// The extension used by the OpenChart format.
        /// </summary>
        public string FileExtension => ".oc";

        /// <summary>
        /// The file format version this handler supports.
        /// </summary>
        public static string Version => "0.1";

        /// <summary>
        /// Reads data from an OpenChart file and returns a Project object.
        /// </summary>
        /// <param name="reader">The stream that contains the file data.</param>
        public Project Read(StreamReader reader)
        {
            var data = reader.ReadToEnd();
            var projectData = serializer.Deserialize(Encoding.UTF8.GetBytes(data));

            return converter.ToNative(projectData);
        }

        /// <summary>
        /// Writes the project object to an OpenChart file.
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