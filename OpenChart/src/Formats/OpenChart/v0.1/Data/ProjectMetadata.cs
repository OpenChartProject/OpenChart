using System;

namespace OpenChart.Formats.OpenChart.Version0_1.Data
{
    /// <summary>
    /// Metadata about the chart that is saved.
    /// </summary>
    public class ProjectMetadata : IValidatable
    {
        /// <summary>
        /// The version of the file format.
        /// </summary>
        public string Version { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Version))
                throw new Exception("The 'metadata.version' field is missing or empty.");
        }

        public override bool Equals(object obj)
        {
            if (obj is ProjectMetadata data)
                return Version == data.Version;

            return false;
        }

        public override int GetHashCode()
        {
            return Version.GetHashCode();
        }
    }
}
