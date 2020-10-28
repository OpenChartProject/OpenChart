using OpenChart.UI.Assets;

namespace OpenChart.NoteSkins
{
    /// <summary>
    /// Contains the image assets for a single key (column).
    /// </summary>
    public class NoteSkinKey
    {
        /// <summary>
        /// The receptor image. The receptor is where a note is hit.
        /// </summary>
        public ImageAsset Receptor;

        /// <summary>
        /// The tap note image. A tap note is a regular note that doesn't have release timing.
        /// </summary>
        public ImageAsset TapNote;

        /// <summary>
        /// The hold note image. A hold note is a note that must be held after the initial hit.
        /// </summary>
        public ImageAsset HoldNote;

        /// <summary>
        /// The hold note body image. The hold note body represents how long a hold note must
        /// be held. The image is tiled and not stretched.
        /// </summary>
        public ImagePattern HoldNoteBody;
    }
}
