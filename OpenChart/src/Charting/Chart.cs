using OpenChart.Charting.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenChart.Charting
{
    /// <summary>
    /// A chart represents a playable beat mapping for a song. The chart has a key count
    /// which dictates what keymode it's played in.
    ///
    /// In the context of file formats, this class is referred to the "native" chart class.
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
        public LinkedList<BaseObject>[] Objects { get; private set; }

        /// <summary>
        /// Creates a new chart instance.
        /// </summary>
        /// <param name="keyCount">The keycount of the chart.</param>
        public Chart(int keyCount)
        {
            if (keyCount < 1)
            {
                throw new ArgumentOutOfRangeException("Key count must be greater than zero.");
            }

            KeyCount = keyCount;
            bpmList = new LinkedList<BPM>();
            Objects = new LinkedList<BaseObject>[KeyCount];

            for (var i = 0; i < KeyCount; i++)
            {
                Objects[i] = new LinkedList<BaseObject>();
            }
        }

        /// <summary>
        /// Checks if both charts have the same (by value): keycount, BPM changes, and objects.
        /// </summary>
        public override bool Equals(object obj)
        {
            var chart = obj as Chart;

            if (chart == null)
            {
                return false;
            }
            else if (KeyCount != chart.KeyCount || BPMs != chart.BPMs)
            {
                return false;
            }
            else if (Objects == chart.Objects)
            {
                return true;
            }
            else
            {
                // Compare the object counts for each column.
                for (var i = 0; i < KeyCount; i++)
                {
                    if (Objects[i].Count != chart.Objects[i].Count)
                    {
                        return false;
                    }
                }

                // Compare each object individually.
                for (var i = 0; i < KeyCount; i++)
                {
                    var curA = Objects[i].First;
                    var curB = chart.Objects[i].First;

                    while (curA != null)
                    {
                        if (curA.Value != curB.Value)
                        {
                            return false;
                        }

                        curA = curA.Next;
                        curB = curB.Next;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Returns the object's hash code.
        /// </summary>
        public override int GetHashCode()
        {
            return Tuple.Create(KeyCount, BPMs, Objects).GetHashCode();
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

        /// <summary>
        /// Adds a chart object to the chart. When adding several objects at once,
        /// use `AddObjects()` instead as it's much faster.
        /// </summary>
        /// <param name="obj">The chart object to add.</param>
        public void AddObject(BaseObject obj)
        {
            addObject(obj, null);
        }

        /// <summary>
        /// Adds an array of chart objects to the chart. The objects don't have to be on the
        /// same key, nor do they need to be sorted.
        ///
        /// TODO?: We could store cursors for each column on the Chart object itself, since it's
        /// likely that objects will be getting added near each other. That would make this method
        /// no longer needed.
        /// </summary>
        /// <param name="obj">An array of chart objects to add.</param>
        public void AddObjects(BaseObject[] objs)
        {
            // Inserting into linked lists is usually pretty slow since it's O(n), but we can
            // speed things up by taking advantage of the fact that our list is sorted by beats.
            // We can sort the object array by beats and then reuse the LinkedListNode cursor
            // so we don't need to start at the beginning of the list every time.
            var query = from obj in objs
                        orderby obj.Beat
                        select obj;

            // Create a cursor for each key
            var cursors = new LinkedListNode<BaseObject>[KeyCount];

            foreach (var obj in query)
            {
                // This logic is only necessary to prevent an out of range exception when we
                // try and access the list. If the object's key index is out of range, the
                // `addObject()` method will take care of it by throwing an exception.
                var cur = obj.Key < KeyCount ? cursors[obj.Key] : null;

                // Use the cursor as the starting point for inserting the object, then save it
                // to be reused later.
                cur = addObject(obj, cur);
                cursors[obj.Key] = cur;
            }
        }

        private void addBPM(BPM bpm)
        {
            if (bpm == null)
            {
                throw new ArgumentNullException("BPM cannot be null.");
            }
            else if (bpmList.Count == 0)
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

        /// <summary>
        /// Adds an object to the chart using the given cursor as the starting point (or the
        /// start of the list if it's null). Returns the node the object was added to.
        /// </summary>
        private LinkedListNode<BaseObject> addObject(BaseObject obj, LinkedListNode<BaseObject> cur)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Chart object cannot be null.");
            }
            else if (obj.Key >= KeyCount)
            {
                throw new ArgumentOutOfRangeException("ChartObject's key is out of range.");
            }

            var col = Objects[obj.Key];
            cur = cur ?? col.First;

            // Traverse down the list.
            while (cur != null)
            {
                if (cur.Value.Beat > obj.Beat)
                {
                    // Check if the object can be inserted here.
                    obj.CanBeInserted(
                        cur.Previous != null ? cur.Previous.Value : null,
                        cur.Value
                    );

                    // Insert the object as soon as we find an existing object which occurs after it.
                    return col.AddBefore(cur, obj);
                }
                else if (cur.Value.Beat == obj.Beat)
                {
                    if (cur.Value == obj)
                    {
                        throw new ChartException("Chart object already exists in chart.");
                    }
                    else
                    {
                        throw new ChartException("Cannot add chart object (an object already exists at this beat).");
                    }
                }

                cur = cur.Next;
            }

            // Check if the object can be inserted here.
            obj.CanBeInserted(
                col.Last != null ? col.Last.Value : null,
                null
            );

            // Add the object to the end if it hasn't been inserted already.
            return col.AddLast(obj);
        }
    }
}
