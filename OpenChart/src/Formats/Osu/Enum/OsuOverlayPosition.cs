namespace OpenChart.Formats.Osu.Enum
{
    public class OsuOverlayPosition
    {
        /// <summary>
        /// Assume the skin behavior for drawing hit circle overlays over hit numbers.
        /// </summary>
        public static readonly string UseSkinBehavior = "NoChange";
        
        /// <summary>
        /// Draw the overlays under hit numbers.
        /// </summary>
        public static readonly string Below = "Below";
        
        /// <summary>
        /// Draw the overlays over hit numbers.
        /// </summary>
        public static readonly string Above = "Above";
    }
}