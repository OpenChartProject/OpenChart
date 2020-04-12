using OpenChart.Formats.Osu.Enum;

namespace OpenChart.Formats.Osu.Data
{
    /// <summary>
    /// Information about the difficulty of an osu! beatmap.
    /// </summary>
    public class OsuDifficultyInformation
    {
        /// <summary>
        /// The rate at which the player's HP should drain. (HP setting)
        /// </summary>
        public decimal HPDrainRate { get; set; }
        
        /// <summary>
        /// In <see cref="OsuGameMode.Standard"/>, the size of the hit circles. Not used in other gamemodes. (CS setting)
        /// </summary>
        public decimal CircleSize { get; set; }
        
        /// <summary>
        /// Affects the timing window, which makes it more difficult for the player to achieve high accuracy. (OD setting)
        /// </summary>
        public decimal OverallDifficulty { get; set; }
        
        /// <summary>
        /// In <see cref="OsuGameMode.Standard"/>, the speed at which hit circles appear. Not used in other gamemodes. (CS setting)
        /// </summary>
        public decimal ApproachRate { get; set; }
        
        /// <summary>
        /// Base slider velocity, in hecto-osu!pixels per beat.
        /// An osu!pixel is defined as a single pixel when the game is running at 640x480 resolution,
        /// and scaled over a 4:3 ratio to fit the actual game resolution.
        /// See more: https://osu.ppy.sh/help/wiki/Glossary#osupixel
        /// </summary>
        public decimal SliderMultiplier { get; set; }
        
        /// <summary>
        /// Number of slider ticks per beat.
        /// </summary>
        public decimal SliderTickRate { get; set; }
    }
}