namespace OpenChart.Formats.StepMania.SM.Data
{
    /// <summary>
    /// Represents the value of a field in an SM file.
    /// </summary>
    public class FieldValue
    {
        /// <summary>
        /// The raw string value.
        /// </summary>
        public readonly string Value;

        /// <summary>
        /// Creates a new FieldValue instance.
        /// </summary>
        /// <param name="val">The raw string value of the field.</param>
        public FieldValue(string val)
        {
            Value = val;
        }

        /// <summary>
        /// Interprets the field as a bool.
        /// </summary>
        public bool AsBool()
        {
            return Value == "yes" || Value == "1";
        }

        /// <summary>
        /// Inteprets the field as a double.
        /// </summary>
        public double AsDouble()
        {
            return double.Parse(Value);
        }

        /// <summary>
        /// Inteprets the field as an int.
        /// </summary>
        public int AsInt()
        {
            return int.Parse(Value);
        }

        /// <summary>
        /// Inteprets the field as a string.
        /// </summary>
        public string AsString()
        {
            return Value;
        }
    }
}
