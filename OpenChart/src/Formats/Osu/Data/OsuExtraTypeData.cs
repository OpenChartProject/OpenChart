namespace OpenChart.Formats.Osu.Data
{
    public class OsuExtraTypeData
    {
        /// <summary>
        /// Denotes whether this note starts a new combo.
        /// </summary>
        public bool IsNewCombo { get; set; }
        
        /// <summary>
        /// How many combo colours to skip, if this object starts a new combo.
        /// """color hax"""
        /// </summary>
        public short SkipComboColors { get; set; }
    }
}