using System.Collections.Generic;

namespace OpenChart.Formats
{
    /// <summary>
    /// A class which manages a collection of file format handlers.
    /// </summary>
    public class FormatManager
    {
        /// <summary>
        /// A collection of file formats. The dictionary key is the format's extension.
        /// </summary>
        Dictionary<string, IFormatHandler> formats;

        /// <summary>
        /// Creates a new FormatManager instance.
        /// </summary>
        public FormatManager()
        {
            formats = new Dictionary<string, IFormatHandler>();
        }

        /// <summary>
        /// Adds a format handler to the dictionary.
        /// </summary>
        /// <param name="handler">The handler to add.</param>
        public void AddFormat(IFormatHandler handler)
        {
            formats.Add(handler.FileExtension, handler);
        }

        /// <summary>
        /// Looks up a format handler by its extension, or returns null if it doesn't exist.
        /// </summary>
        /// <param name="ext">The file extension used by the format. Includes a period.</param>
        public IFormatHandler GetFormatHandler(string ext)
        {
            if (!formats.ContainsKey(ext))
            {
                return null;
            }

            return formats[ext];
        }
    }
}
