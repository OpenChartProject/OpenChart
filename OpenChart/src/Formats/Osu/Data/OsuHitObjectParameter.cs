namespace OpenChart.Formats.Osu.Data
{
    /// <summary>
    /// Wrapper for osu! hit object parameter data.
    /// </summary>
    public class OsuHitObjectParameter
    {
        /// <summary>
        /// The purpose of the parameter. 
        /// </summary>
        public string Purpose { get; set; }
        
        /// <summary>
        /// The data for that parameter.
        /// </summary>
        public string Data { get; set; }
    }
}