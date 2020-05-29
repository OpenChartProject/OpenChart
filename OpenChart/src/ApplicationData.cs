using OpenChart.Formats;
using OpenChart.Formats.OpenChart.Version0_1;
using OpenChart.NoteSkins;
using Serilog;

namespace OpenChart
{
    /// <summary>
    ///
    /// </summary>
    public class ApplicationData
    {
        /// <summary>
        /// The absolute path to the folder where the OpenChart executable is.
        /// </summary>
        public string AppFolder { get; private set; }

        /// <summary>
        /// The manager for different file formats.
        /// </summary>
        public FormatManager Formats { get; private set; }

        /// <summary>
        /// The location of the noteskins folder.
        /// </summary>
        public string NoteSkinFolder => "noteskins";

        /// <summary>
        /// The noteskins that are loaded into the app.
        /// </summary>
        public NoteSkinManager NoteSkins { get; private set; }

        /// <summary>
        /// Creates a new ApplicationData instance.
        /// </summary>
        /// <param name="appFolder">The path the OpenChart executable is in.</param>
        public ApplicationData(string appFolder)
        {
            AppFolder = appFolder;
            Formats = new FormatManager();
            NoteSkins = new NoteSkinManager();
        }

        /// <summary>
        /// Initializes the application data.
        /// </summary>
        public void Init()
        {
            Log.Debug("Setting up file formats.");
            Formats.AddFormat(new OpenChartFormatHandler());

            Log.Information("Finding noteskins...");
            NoteSkins.LoadAll();
        }
    }
}
