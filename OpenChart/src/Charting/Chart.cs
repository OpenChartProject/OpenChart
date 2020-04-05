using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenChart.Charting
{
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

        public Chart()
        {
            bpmList = new LinkedList<BPM>();
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