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
        public string AppFolder { get; set; }

        /// <summary>
        /// The manager for different file formats.
        /// </summary>
        public FormatManager Formats { get; set; }

        /// <summary>
        /// The location of the noteskins folder.
        /// </summary>
        public string NoteSkinFolder => "noteskins";

        /// <summary>
        /// The noteskins that are loaded into the app.
        /// </summary>
        public NoteSkinManager NoteSkins { get; set; }

        /// <summary>
        /// Initializes the app data.
        /// </summary>
        public void Init()
        {
            Formats = new FormatManager();
            Formats.AddFormat(new OpenChartFormatHandler());

            NoteSkins = new NoteSkinManager();

            Log.Information("Finding noteskins...");
            NoteSkins.LoadAll();

            Log.Information("OpenChart init OK.");
        }
    }
}
