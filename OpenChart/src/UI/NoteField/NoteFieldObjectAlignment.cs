namespace OpenChart.UI.NoteField
{
    /// <summary>
    /// The alignment for note field objects with respect to beat lines. This doesn't affect the
    /// actual placement of the objects, it's simply for appearance.
    /// </summary>
    public enum NoteFieldObjectAlignment
    {
        /// <summary>
        /// Use the top of the object for alignment.
        /// </summary>
        Top = 0,

        /// <summary>
        /// Use the center of the object for alignment.
        /// </summary>
        Center,

        /// <summary>
        /// Use the bottom of the object for alignment.
        /// </summary>
        Bottom
    }
}
