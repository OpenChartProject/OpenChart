using System;

namespace OpenChart.Formats.StepMania.SM.Exceptions
{
    /// <summary>
    /// An exception thrown when a step file has no BPM data.
    /// </summary>
    public class NoBPMException : FileFormatException
    {
        public NoBPMException() : base() { }
        public NoBPMException(string msg) : base(msg) { }
        public NoBPMException(string msg, Exception inner) : base(msg, inner) { }
    }
}
