using Gtk;

namespace OpenChart.UI.Widgets
{
    /// <summary>
    /// The base class for any object that appears on the note field.
    /// </summary>
    public abstract class NoteFieldObject : Image
    {
        /// <summary>
        /// The key index this object is for on the note field.
        /// </summary>
        public int Key { get; private set; }

        public NoteFieldObject(int key)
        {
            Key = key;
        }
    }
}