using System.Collections.Generic;

namespace OpenChart.Chart
{
    public class Chart
    {
        LinkedList<BPM> _bpms;

        public Chart()
        {
            _bpms = new LinkedList<BPM>();
        }

        /// <summary>
        /// Adds a BPM change to the chart. If the BPM being added occurs on the same beat as
        /// an existing BPM change, the existing change is overwritten with the new one.
        /// </summary>
        /// <param name="bpm">The BPM to add.</param>
        public void AddBPMChange(BPM bpm)
        {
            if (_bpms.Count == 0)
            {
                _bpms.AddFirst(bpm);
                return;
            }

            LinkedListNode<BPM> cur = _bpms.First;

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
                        _bpms.AddAfter(cur, bpm);
                        return;
                    }
                }

                cur = cur.Next;
            }
        }
    }
}