using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenChart.Charting
{
    /// <summary>
    /// A chart represents a playable beat mapping for a song. The chart has a key count
    /// which dictates what keymode it's played in.
    /// </summary>
    public class Chart
    {
        LinkedList<BPM> bpmList;
        BPM[] cachedBPMs;

        /// <summary>
        /// Returns a list of BPM changes for the chart.
        ///
        /// **This list is meant to be readonly. Do not modify this list.**
        /// </summary>
        public BPM[] BPMs
        {
            get
            {
                // Update the cache if it's outdated.
                if (cachedBPMs == null)
                {
                    cachedBPMs = bpmList.ToArray();
                }

                return cachedBPMs;
            }
        }

        /// <summary>
        /// The key count for the chart. The key count is the number of unique columns that
        /// ChartObjects appear on.
        /// </summary>
        public readonly int KeyCount;

        /// <summary>
        /// The chart objects that make up the chart. Chart objects include things like tap notes,
        /// holds, mines, etc.
        ///
        /// The objects are stored as an array of linked lists. Each list element represents a key/column.
        /// </summary>
        public LinkedList<ChartObject>[] Objects { get; private set; }

        public Chart(int keyCount)
        {
            if (keyCount < 1)
            {
                throw new ArgumentException("Key count must be greater than zero.");
            }

            KeyCount = keyCount;
            bpmList = new LinkedList<BPM>();
            Objects = new LinkedList<ChartObject>[KeyCount];

            for (var i = 0; i < KeyCount; i++)
            {
                Objects[i] = new LinkedList<ChartObject>();
            }
        }

        /// <summary>
        /// Adds a BPM change to the chart. If the BPM being added occurs on the same beat as
        /// an existing BPM change, the existing change is overwritten with the new one.
        /// </summary>
        /// <param name="bpm">The BPM to add.</param>
        public void AddBPM(BPM bpm)
        {
            addBPM(bpm);

            // Invalidate the cache.
            cachedBPMs = null;
        }

        private void addBPM(BPM bpm)
        {
            if (bpmList.Count == 0)
            {
                if (bpm.Beat != 0)
                {
                    throw new ArgumentException(
                        "The first BPM change must occur at the beginning of the chart (beat zero)."
                    );
                }

                bpmList.AddFirst(bpm);
                return;
            }

            LinkedListNode<BPM> cur = bpmList.First;

            // Search the list to find where we need to insert this new bpm change.
            while (true)
            {
                if (cur.Value.Beat == bpm.Beat)
                {
                    // Overwrite an existing BPM if it occurs on the same beat as the one
                    // we're trying to add.
                    cur.Value = bpm;
                    return;
                }
                else if (cur.Value.Beat < bpm.Beat)
                {
                    // We've hit the end of the list, or we're between two BPM changes.
                    if (cur.Next == null || cur.Next.Value.Beat > bpm.Beat)
                    {
                        bpmList.AddAfter(cur, bpm);
                        return;
                    }
                }

                cur = cur.Next;
            }
        }
    }
}