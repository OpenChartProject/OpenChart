using System;

namespace OpenChart.Formats.StepMania.SM.Data
{
    /// <summary>
    /// Represents a single measure in a chart.
    /// </summary>
    public class Measure
    {
        /// <summary>
        /// The beat rows for the measure. Non-empty beat rows are null.
        /// </summary>
        public readonly BeatRow[] BeatRows;

        /// <summary>
        /// The measure number (starting at 0).
        /// </summary>
        public readonly int Number;

        /// <summary>
        /// The number of subdivisions for the measure. This number divides the measure into
        /// smaller segments which correspond to each beat row.
        ///
        /// For quarter notes this is 4, eigth notes it's 8, etc.
        /// </summary>
        public readonly int Subdivisions;

        /// <summary>
        /// Creates a new Measure instance.
        /// </summary>
        public Measure(int number, int subdivisions)
        {
            Number = number;
            Subdivisions = subdivisions;
            BeatRows = new BeatRow[Subdivisions];
        }

        /// <summary>
        /// Adds a row to the measure.
        /// </summary>
        /// <param name="subdivision">The subdivision for the row (starting at 0).</param>
        /// <param name="row">The beat row.</param>
        public void AddRow(int subdivision, BeatRow row)
        {
            if (subdivision < 0 || subdivision >= Subdivisions)
                throw new IndexOutOfRangeException();

            BeatRows[subdivision] = row;
        }
    }
}
