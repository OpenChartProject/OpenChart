namespace OpenChart.Formats.StepMania.SM.Enums
{
    /// <summary>
    /// The different note types in a .sm file.
    /// </summary>
    public enum NoteType
    {
        Empty = '0',
        Tap = '1',
        HoldHead = '2',
        HoldRollTail = '3',
        RollHead = '4',
        Mine = 'M',
        KeySound = 'K',
        Lift = 'L',
        Fake = 'F'
    }
}
